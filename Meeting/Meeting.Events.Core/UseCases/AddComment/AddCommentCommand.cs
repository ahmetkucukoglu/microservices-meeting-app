namespace Meeting.Events.Core
{
    using MediatR;
    using System;

    public class AddCommentCommand : IRequest
    {
        public AddCommentCommand(Guid eventId, string commentatorId, string comment, DateTimeOffset commentedDate)
        {
            if (eventId == null)
            {
                throw new InvalidEventIdException();
            }

            if (string.IsNullOrEmpty(commentatorId))
            {
                throw new InvalidCommentatorIdException();
            }

            if (string.IsNullOrEmpty(comment))
            {
                throw new InvalidCommentException();
            }

            EventId = eventId;
            CommentatorId = commentatorId;
            Comment = comment;
            CommentedDate = commentedDate;
        }

        public Guid EventId { get; set; }
        public string CommentatorId { get; }
        public string Comment { get; }
        public DateTimeOffset CommentedDate { get; }
    }
}
