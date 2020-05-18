namespace Meeting.Events.Infrastructure
{
    using Couchbase;
    using Couchbase.Core;
    using Couchbase.N1QL;
    using Couchbase.Search;
    using Couchbase.Search.Queries.Compound;
    using Couchbase.Search.Queries.Geo;
    using Couchbase.Search.Queries.Simple;
    using Meeting.Events.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EventRepository : IEventRepository
    {
        private readonly IBucket _eventsBucket;

        public EventRepository(IEventsBucketProvider eventsBucketProvider)
        {
            _eventsBucket = eventsBucketProvider.GetBucket();
        }

        public async Task AddComment(AddCommentCommand addCommentCommand)
        {
            var queryRequest = new QueryRequest()
                     .Statement($"UPDATE `{_eventsBucket.Name}` USE KEYS $eventId SET comments = ARRAY_APPEND(IFMISSINGORNULL(comments,[]), $comment);")
                     .AddNamedParameter("$eventId", addCommentCommand.EventId)
                     .AddNamedParameter("$comment", new EventCommentDocument { Comment = addCommentCommand.Comment, CommentatorId = addCommentCommand.CommentatorId, CommentedDate = addCommentCommand.CommentedDate });

            var queryResult = await _eventsBucket.QueryAsync<dynamic>(queryRequest);

            if (!queryResult.Success)
                throw queryResult.Exception;
        }

        public async Task CancelEvent(CancelEventCommand cancelEventCommand)
        {
            var documentResult = await _eventsBucket.MutateIn<EventDocument>(cancelEventCommand.EventId.ToString())
                   .Replace("status", EventStatuses.CANCELLED)
                   .ExecuteAsync();

            if (!documentResult.Success)
                throw documentResult.Exception;
        }

        public async Task CompleteEvent(CompleteEventCommand completeEventCommand)
        {
            var documentResult = await _eventsBucket.MutateIn<EventDocument>(completeEventCommand.EventId.ToString())
                   .Replace("status", EventStatuses.COMPLETED)
                   .ExecuteAsync();

            if (!documentResult.Success)
                throw documentResult.Exception;
        }

        public async Task CreateEvent(Guid eventId, CreateEventCommand createEventCommand)
        {
            await CreateUrlKey(eventId, createEventCommand.UrlKey);

            var document = new Document<EventDocument>
            {
                Id = eventId.ToString(),
                Content = new EventDocument
                {
                    Subject = createEventCommand.Subject,
                    UrlKey = createEventCommand.UrlKey,
                    Description = createEventCommand.Description,
                    Address = createEventCommand.Address,
                    Date = createEventCommand.Date,
                    EndDate = createEventCommand.EndDate,
                    Capacity = createEventCommand.Capacity,
                    GroupId = createEventCommand.GroupId,
                    Status = EventStatuses.ACTIVE,
                    Location = new EventLocationDocument
                    {
                        Lat = createEventCommand.Latitude,
                        Lon = createEventCommand.Longitude
                    }
                }
            };

            var documentResult = await _eventsBucket.InsertAsync(document);

            if (!documentResult.Success)
            {
                await DeleteUrlKey(createEventCommand.UrlKey);

                throw documentResult.Exception;
            }
        }

        public async Task<GetEventByIdQueryResult> GetEventById(GetEventByIdQuery getEventByIdQuery)
        {
            var queryRequest = new QueryRequest()
                  .Statement(@"SELECT e.*, META(e).id, ARRAY_COUNT(e.comments) as commentsCount, OBJECT_CONCAT({'latitude':e.location.lat},{'longitude':e.location.lon}) AS location, OBJECT_CONCAT({'name':r_groups.groupName},{'id':e.groupId},{'organizerId':r_groups.organizerId}) AS `group`
                               FROM `events` e 
                               INNER JOIN `events-relations` r_groups ON KEYS 'groups|' || e.groupId
                               WHERE META(e).id = $eventId
                               LIMIT 1;")
                  .AddNamedParameter("$eventId", getEventByIdQuery.EventId.ToString());

            var queryResult = await _eventsBucket.QueryAsync<GetEventByIdQueryResult>(queryRequest);

            if (!queryResult.Success)
                throw queryResult.Exception;

            var result = queryResult.Rows.FirstOrDefault();

            return result;
        }

        public async Task<GetEventByIdQueryResult> GetEventByUrlKey(GetEventByUrlKeyQuery getEventByUrlKeyQuery)
        {
            var documentResult = _eventsBucket.GetDocument<EventUrlKeyDocument>($"urlkey::{getEventByUrlKeyQuery.UrlKey}");

            if (!documentResult.Success && documentResult.Status == Couchbase.IO.ResponseStatus.KeyNotFound)
                throw new GroupNotFoundException();
            else if (!documentResult.Success)
                throw documentResult.Exception;

            return await GetEventById(new GetEventByIdQuery(documentResult.Document.Content.EventId));
        }

        public async Task<IEnumerable<GetUpcomingEventsQueryResult>> GetUpcomingEvents(GetUpcomingEventsQuery getUpcomingEventsQuery)
        {
            var geoDistanceQuery = new GeoDistanceQuery();
            geoDistanceQuery.Field("location");
            geoDistanceQuery.Latitude(getUpcomingEventsQuery.Latitude);
            geoDistanceQuery.Longitude(getUpcomingEventsQuery.Longitude);
            geoDistanceQuery.Distance($"{getUpcomingEventsQuery.Radius}km");

            var statusMatchQuery = new MatchQuery(EventStatuses.ACTIVE);
            statusMatchQuery.Field("status");

            var conjunctionQuery = new ConjunctionQuery(geoDistanceQuery, statusMatchQuery);

            if (!string.IsNullOrEmpty(getUpcomingEventsQuery.Keywords))
            {
                var subjectMatchQuery = new MatchQuery(getUpcomingEventsQuery.Keywords).Fuzziness(1);

                conjunctionQuery.And(subjectMatchQuery);
            }

            var searchParams = new SearchParams()
                .Fields("*")
                .Limit(10)
                .Timeout(TimeSpan.FromMilliseconds(10000));

            var searchQuery = new SearchQuery
            {
                Query = conjunctionQuery,
                Index = "idx_geo_events",
                SearchParams = searchParams
            };

            var queryResult = await _eventsBucket.QueryAsync(searchQuery);

            var result = new List<GetUpcomingEventsQueryResult>();

            foreach (var hit in queryResult.Hits)
            {
                result.Add(new GetUpcomingEventsQueryResult
                {
                    EventId = Guid.Parse(hit.Id),
                    Subject = hit.Fields["subject"],
                    UrlKey = hit.Fields["urlKey"],
                    Description = hit.Fields["description"],
                    Date = DateTimeOffset.Parse(hit.Fields["date"].ToString())
                });
            }

            return result;
        }

        public async Task JoinEvent(JoinEventCommand joinEventCommand)
        {
            var attempts = 0;

            Exception exception = null;

            do
            {
                var @event = await _eventsBucket.GetAsync<EventDocument>(joinEventCommand.EventId.ToString());

                if (@event.Value.Capacity == @event.Value.AttendeeCount)
                    throw new CapacityLimitExceededException();

                if (@event.Value.Attendees.Any((x) => x.AttendeeId == joinEventCommand.AttendeeId))
                    throw new AttendeeIdAlreadyExistsException();

                var original = @event.Value;

                var documentResult = await _eventsBucket.MutateIn<EventDocument>(joinEventCommand.EventId.ToString())
                    .Replace("attendeeCount", ++original.AttendeeCount)
                    .ArrayAppend("attendees", new EventAttendeeDocument { AttendeeId = joinEventCommand.AttendeeId, JoinedDate = joinEventCommand.JoinedDate })
                    .WithCas(@event.Cas)
                    .ExecuteAsync();

                if (documentResult.Success)
                    break;

                exception = documentResult.Exception;
            } while (attempts++ < 10);

            if (exception != null)
                throw exception;
        }

        public async Task LeaveEvent(LeaveEventCommand leaveEventCommand)
        {
            var queryRequest = new QueryRequest()
                .Statement("UPDATE events SET attendeeCount = attendeeCount - 1, attendees = ARRAY a FOR a IN attendees WHEN a.attendeeId != $attendeeId END WHERE META().id = $eventId;")
                .AddNamedParameter("$eventId", leaveEventCommand.EventId)
                .AddNamedParameter("$attendeeId", leaveEventCommand.AttendeeId);

            var queryResult = await _eventsBucket.QueryAsync<dynamic>(queryRequest);

            if (!queryResult.Success)
                throw queryResult.Exception;
        }

        public async Task UpdateEvent(UpdateEventCommand updateEventCommand)
        {
            var documentResult = await _eventsBucket.MutateIn<EventDocument>(updateEventCommand.EventId.ToString())
                .Replace("subject", updateEventCommand.Subject)
                .Replace("description", updateEventCommand.Description)
                .Replace("date", updateEventCommand.Date)
                .Replace("endDate", updateEventCommand.EndDate)
                .Replace("capacity", updateEventCommand.Capacity)
                .Replace("address", updateEventCommand.Address)
                .Replace("location.lat", updateEventCommand.Latitude)
                .Replace("location.lon", updateEventCommand.Longitude)
                .ExecuteAsync();

            if (!documentResult.Success)
                throw documentResult.Exception;
        }

        public async Task<IEnumerable<GetGroupEventsQueryResult>> GetGroupEvents(GetGroupEventsQuery getGroupEventsQuery)
        {
            var queryRequest = new QueryRequest()
                  .Statement(@"SELECT META().id AS eventId, subject, urlKey, description, status, date, attendeeCount FROM events 
                               WHERE groupId = $groupId AND status = $status")
                  .AddNamedParameter("$groupId", getGroupEventsQuery.GroupId.ToString())
                  .AddNamedParameter("$status", getGroupEventsQuery.Status.ToString());

            var queryResult = await _eventsBucket.QueryAsync<GetGroupEventsQueryResult>(queryRequest);

            if (!queryResult.Success)
                throw queryResult.Exception;

            return queryResult.Rows;
        }

        public async Task<bool> ExistsEvent(Guid eventId)
        {
            var isExists = await _eventsBucket.ExistsAsync(eventId.ToString());

            return isExists;
        }

        public async Task<IEnumerable<GetCommentsQueryResult>> GetComments(GetCommentsQuery getCommentsQuery)
        {
            var queryRequest = new QueryRequest()
                        .Statement(@"SELECT comments FROM `events` e 
                                     LEFT NEST `events-relations` r_commentators ON KEYS ARRAY 'users|' || x.commentatorId FOR x IN e.comments END
                                     LET comments = ARRAY {
                                       'commentatorId' : c.commentatorId,
                                       'commentatorName' : IFNULL(FIRST x FOR x IN r_commentators WHEN META(x).id  = 'users|' || c.commentatorId END, MISSING).userName,
                                       'comment' : c.comment,
                                       'commentedDate' : c.commentedDate
                                     } FOR c IN e.comments END
                                     WHERE META(e).id = $eventId
                                     LIMIT 1;")
                        .AddNamedParameter("$eventId", getCommentsQuery.EventId.ToString());

            var queryResult = await _eventsBucket.QueryAsync<dynamic>(queryRequest);

            if (!queryResult.Success)
                throw queryResult.Exception;

            var queryResultItem = queryResult.Rows.FirstOrDefault();
            var result = ((IEnumerable<dynamic>)queryResultItem.comments).Select((x) => new GetCommentsQueryResult
            {
                CommentatorId = x.commentatorId,
                CommentatorName = x.commentatorName,
                Comment = x.comment,
                CommentedDate = x.commentedDate
            });

            return result;
        }

        public async Task<IEnumerable<GetAttendeesQueryResult>> GetAttendees(GetAttendeesQuery getAttendeesQuery)
        {
            var queryRequest = new QueryRequest()
                        .Statement(@"SELECT attendeeList FROM `events` e 
                                     LEFT NEST `events-relations` r_attendees ON KEYS ARRAY 'users|' || x.attendeeId FOR x IN e.attendees END
                                     LET attendeeList = ARRAY {
                                       'attendeeId' : a.attendeeId,
                                       'attendeeName' : IFNULL(FIRST x FOR x IN r_attendees WHEN META(x).id  = 'users|' || a.attendeeId END, MISSING).userName,
                                       'joinedDate' : a.joinedDate
                                     } FOR a IN e.attendees END
                                     WHERE META(e).id = $eventId
                                     LIMIT 1;")
                        .AddNamedParameter("$eventId", getAttendeesQuery.EventId.ToString());

            var queryResult = await _eventsBucket.QueryAsync<dynamic>(queryRequest);

            if (!queryResult.Success)
                throw queryResult.Exception;

            var queryResultItem = queryResult.Rows.FirstOrDefault();
            var result = ((IEnumerable<dynamic>)queryResultItem.attendeeList).Select((x) => new GetAttendeesQueryResult
            {
                AttendeeId = x.attendeeId,
                AttendeeName = x.attendeeName,
                JoinedDate = x.joinedDate
            });

            return result;
        }

        public async Task<GetAttendeeInfoQueryResult> GetAttendeeInfo(GetAttendeeInfoQuery getAttendeeInfoQuery)
        {
            var queryRequest = new QueryRequest()
                  .Statement(@"SELECT a.joinedDate FROM `events` e 
                               UNNEST attendees AS a
                               WHERE META(e).id = $eventId AND a.attendeeId = $attendeeId
                               LIMIT 1;")
                  .AddNamedParameter("$eventId", getAttendeeInfoQuery.EventId.ToString())
                  .AddNamedParameter("$attendeeId", getAttendeeInfoQuery.AttendeeId);

            var queryResult = await _eventsBucket.QueryAsync<GetAttendeeInfoQueryResult>(queryRequest);

            return new GetAttendeeInfoQueryResult
            {
                JoinedDate = queryResult.Rows.Count > 0 ? queryResult.Rows.FirstOrDefault().JoinedDate : null
            };
        }

        public async Task<IEnumerable<GetEventsQueryResult>> GetEvents(GetEventsQuery getEventsQuery)
        {
            var queryRequest = new QueryRequest()
                     .Statement(@"SELECT META().id, subject, date, endDate, capacity, status, attendeeCount FROM events 
                                  WHERE groupId = $groupId")
                     .AddNamedParameter("$groupId", getEventsQuery.GroupId.ToString());

            var queryResult = await _eventsBucket.QueryAsync<GetEventsQueryResult>(queryRequest);

            if (!queryResult.Success)
                throw queryResult.Exception;

            return queryResult.Rows;
        }

        #region Private Functions

        private async Task CreateUrlKey(Guid eventId, string urlKey)
        {
            var document = new Document<EventUrlKeyDocument>
            {
                Id = $"urlkey::{urlKey}",
                Content = new EventUrlKeyDocument
                {
                    UrlKey = urlKey,
                    EventId = eventId
                }
            };

            var documentResult = await _eventsBucket.InsertAsync(document);

            if (!documentResult.Success && documentResult.Status == Couchbase.IO.ResponseStatus.KeyExists)
                throw new UrlKeyAlreadyExistsException();
            else if (!documentResult.Success)
                throw documentResult.Exception;
        }

        private async Task DeleteUrlKey(string urlKey)
        {
            var documentResult = await _eventsBucket.RemoveAsync($"urlkey::{urlKey}");

            if (!documentResult.Success)
                throw documentResult.Exception;
        }

        #endregion
    }
}
