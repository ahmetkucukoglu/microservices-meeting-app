namespace Meeting.Events.Core
{
    using System;

    public class GetCommentsQueryResult
    {
        public string CommentatorId { get; set; }
        public string CommentatorName { get; set; }
        public string Comment { get; set; }
        public DateTimeOffset CommentedDate { get; set; }
    }
}
