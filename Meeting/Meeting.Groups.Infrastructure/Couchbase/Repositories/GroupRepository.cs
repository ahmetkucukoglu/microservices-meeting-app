namespace Meeting.Groups.Infrastructure
{
    using Couchbase;
    using Couchbase.Core;
    using Couchbase.N1QL;
    using Meeting.Groups.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class GroupRepository : IGroupRepository
    {
        private readonly IBucket _groupsBucket;

        public GroupRepository(IGroupsBucketProvider groupsBucketProvider)
        {
            _groupsBucket = groupsBucketProvider.GetBucket();
        }

        public async Task CreateGroup(Guid groupId, CreateGroupCommand createGroupCommand)
        {
            await CreateUrlKey(groupId, createGroupCommand.UrlKey);

            var document = new Document<GroupDocument>
            {
                Id = groupId.ToString(),
                Content = new GroupDocument
                {
                    Name = createGroupCommand.Name,
                    Description = createGroupCommand.Description,
                    UrlKey = createGroupCommand.UrlKey,
                    OrganizerId = createGroupCommand.OrganizerId,
                    CityId = createGroupCommand.CityId,
                    Topics = createGroupCommand.TopicIds.Distinct().Select((x) => new GroupTopicDocument { TopicId = x }).ToList()
                }
            };

            var documentResult = await _groupsBucket.InsertAsync(document);

            if (!documentResult.Success)
            {
                await DeleteUrlKey(createGroupCommand.UrlKey);

                throw documentResult.Exception;
            }
        }

        public async Task<bool> ExistsGroup(Guid groupId)
        {
            var isExists = await _groupsBucket.ExistsAsync(groupId.ToString());

            return isExists;
        }

        public async Task<List<FindGroupsQueryResult>> FindGroups(FindGroupsQuery findGroupsQuery)
        {
            var queryRequest = new QueryRequest()
                .Statement($"SELECT META().id, g.name, g.urlKey, ARRAY_COUNT(g.members) as membersCount FROM `{_groupsBucket.Name}` AS g WHERE g.cityId = $cityId AND ANY t IN g.topics SATISFIES t.topicId IN $topicId END")
                .AddNamedParameter("$cityId", findGroupsQuery.CityId)
                .AddNamedParameter("$topicId", findGroupsQuery.TopicIds.ToArray());

            var queryResult = await _groupsBucket.QueryAsync<FindGroupsQueryResult>(queryRequest);

            if (!queryResult.Success)
                throw queryResult.Exception;

            return queryResult.Rows;
        }

        public async Task JoinGroup(JoinGroupCommand joinGroupCommand)
        {
            //var documentResult = await _groupsBucket.MutateIn<GroupDocument>(joinGroupCommand.GroupId.ToString())
            //    .ArrayAppend("members", new GroupMemberDocument { MemberId = joinGroupCommand.MemberId })
            //    .ExecuteAsync();

            //var documentResult = await _groupsBucket.MutateIn<GroupDocument>(joinGroupCommand.GroupId.ToString())
            //    .ArrayAppend("members", new GroupMemberDocument { MemberId = joinGroupCommand.MemberId })
            //    .ExecuteAsync();

            //if (!documentResult.Success)
            //    throw documentResult.Exception;

            var queryRequest = new QueryRequest()
                .Statement($"UPDATE `{_groupsBucket.Name}` USE KEYS $groupId SET members = ARRAY_DISTINCT(ARRAY_APPEND(IFMISSINGORNULL(members,[]), $memberId));")
                .AddNamedParameter("$groupId", joinGroupCommand.GroupId)
                .AddNamedParameter("$memberId", new GroupMemberDocument { MemberId = joinGroupCommand.MemberId });

            var queryResult = await _groupsBucket.QueryAsync<dynamic>(queryRequest);

            if (!queryResult.Success)
                throw queryResult.Exception;
        }

        public async Task LeaveGroup(LeaveGroupCommand leaveGroupCommand)
        {
            //var queryRequest = new QueryRequest()
            //    .Statement("UPDATE groups SET members = ARRAY m FOR m IN members WHEN m.memberId != $memberId END WHERE META().id = $groupId;")
            //    .AddNamedParameter("$groupId", leaveGroupCommand.GroupId)
            //    .AddNamedParameter("$memberId", leaveGroupCommand.MemberId);

            //var queryResult = await _groupsBucket.QueryAsync<dynamic>(queryRequest);

            //if (!queryResult.Success)
            //    throw queryResult.Exception;

            var queryRequest = new QueryRequest()
               .Statement($"UPDATE `{_groupsBucket.Name}` USE KEYS $groupId SET members = ARRAY_REMOVE(members, $memberId);")
               .AddNamedParameter("$groupId", leaveGroupCommand.GroupId)
               .AddNamedParameter("$memberId", new GroupMemberDocument { MemberId = leaveGroupCommand.MemberId });

            var queryResult = await _groupsBucket.QueryAsync<dynamic>(queryRequest);

            if (!queryResult.Success)
                throw queryResult.Exception;
        }

        public async Task<GetGroupByIdQueryResult> GetGroupById(GetGroupByIdQuery getGroupByIdQuery)
        {
            var queryRequest = new QueryRequest()
                  .Statement(@"SELECT META(g).id, g.name, g.description, g.cityId, g.urlKey, ARRAY_COUNT(g.members) as membersCount, topics, OBJECT_CONCAT({'name':r_cities.cityName},{'id':g.cityId}) AS `city`, OBJECT_CONCAT({'name':r_organizers.userName},{'id':g.organizerId}) AS `organizer`
                               FROM `groups` g 
                               INNER JOIN `groups-relations` r_cities ON KEYS 'cities|' || g.cityId
                               INNER JOIN `groups-relations` r_organizers ON KEYS 'users|' || g.organizerId
                               LEFT NEST `groups-relations` r_topics ON KEYS ARRAY 'topics|' || x.topicId FOR x IN g.topics END
                               LET topics = ARRAY {
                                 'id' : t.topicId,
                                 'name' : IFNULL(FIRST x FOR x IN r_topics WHEN META(x).id  = 'topics|' || t.topicId END, MISSING).topicName
                               } FOR t IN g.topics END
                               WHERE META(g).id = $groupId
                               LIMIT 1;")
                  .AddNamedParameter("$groupId", getGroupByIdQuery.GroupId.ToString());

            var queryResult = await _groupsBucket.QueryAsync<GetGroupByIdQueryResult>(queryRequest);

            if (!queryResult.Success)
                throw queryResult.Exception;

            var result = queryResult.Rows.FirstOrDefault();

            return result;
        }

        public async Task<GetGroupByIdQueryResult> GetGroupByUrlKey(GetGroupByUrlKeyQuery getGroupByUrlKeyQuery)
        {
            var documentResult = _groupsBucket.GetDocument<GroupUrlKeyDocument>($"urlkey::{getGroupByUrlKeyQuery.UrlKey}");

            if (!documentResult.Success && documentResult.Status == Couchbase.IO.ResponseStatus.KeyNotFound)
                throw new GroupNotFoundException();
            else if (!documentResult.Success)
                throw documentResult.Exception;

            return await GetGroupById(new GetGroupByIdQuery(documentResult.Document.Content.GroupId));
        }

        public async Task<IEnumerable<GetMembersQueryResult>> GetMembers(GetMembersQuery getMembersQuery)
        {
            var queryRequest = new QueryRequest()
                     .Statement(@"SELECT members FROM `groups` g 
                                  LEFT NEST `groups-relations` r_members ON KEYS ARRAY 'users|' || x.memberId FOR x IN g.members END
                                  LET members = ARRAY {
                                    'memberId' : m.memberId,
                                    'memberName' : IFNULL(FIRST x FOR x IN r_members WHEN META(x).id  = 'users|' || m.memberId END, MISSING).userName
                                  } FOR m IN g.members END
                                  WHERE META(g).id = $groupId
                                  LIMIT 1;")
                     .AddNamedParameter("$groupId", getMembersQuery.GroupId.ToString());

            var queryResult = await _groupsBucket.QueryAsync<dynamic>(queryRequest);

            if (!queryResult.Success)
                throw queryResult.Exception;

            var queryResultItem = queryResult.Rows.FirstOrDefault();
            var result = ((IEnumerable<dynamic>)queryResultItem.members).Select((x) => new GetMembersQueryResult
            {
                MemberId = x.memberId,
                MemberName = x.memberName
            });

            return result;
        }

        public async Task<GetMemberInfoQueryResult> GetMemberInfo(GetMemberInfoQuery getMemberInfoQuery)
        {
            var queryRequest = new QueryRequest()
                     .Statement(@"SELECT m.memberId FROM `groups` e 
                                  UNNEST members AS m
                                  WHERE META(e).id = $groupId AND m.memberId = $memberId
                                  LIMIT 1;")
                     .AddNamedParameter("$groupId", getMemberInfoQuery.GroupId.ToString())
                     .AddNamedParameter("$memberId", getMemberInfoQuery.MemberId);

            var queryResult = await _groupsBucket.QueryAsync<GetMemberInfoQueryResult>(queryRequest);

            return new GetMemberInfoQueryResult
            {
                MemberedIn = queryResult.Rows.Count > 0
            };
        }

        public async Task<IEnumerable<GetGroupsQueryResult>> GetGroups(GetGroupsQuery getGroupsQuery)
        {
            var queryRequest = new QueryRequest()
                     .Statement(@"SELECT META(g).id, g.name, ARRAY_COUNT(g.members) as membersCount
                                  FROM `groups` g 
                                  WHERE g.organizerId = $organizerId")
                     .AddNamedParameter("$organizerId", getGroupsQuery.OrganizerId);

            var queryResult = await _groupsBucket.QueryAsync<GetGroupsQueryResult>(queryRequest);

            if (!queryResult.Success)
                throw queryResult.Exception;

            return queryResult.Rows;
        }

        #region Private Functions

        private async Task CreateUrlKey(Guid groupId, string urlKey)
        {
            var document = new Document<GroupUrlKeyDocument>
            {
                Id = $"urlkey::{urlKey}",
                Content = new GroupUrlKeyDocument
                {
                    UrlKey = urlKey,
                    GroupId = groupId
                }
            };

            var documentResult = await _groupsBucket.InsertAsync(document);

            if (!documentResult.Success && documentResult.Status == Couchbase.IO.ResponseStatus.KeyExists)
                throw new UrlKeyAlreadyExistsException();
            else if (!documentResult.Success)
                throw documentResult.Exception;
        }

        private async Task DeleteUrlKey(string urlKey)
        {
            var documentResult = await _groupsBucket.RemoveAsync($"urlkey::{urlKey}");

            if (!documentResult.Success)
                throw documentResult.Exception;
        }

        #endregion
    }
}
