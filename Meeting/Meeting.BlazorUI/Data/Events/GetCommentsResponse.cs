namespace Meeting.BlazorUI.Data
{
    using System;

    public class GetCommentsResponse
    {
        public string CommentatorId { get; set; }
        public string CommentatorName { get; set; }
        public string Comment { get; set; }
        public DateTimeOffset CommentedDate { get; set; }
    }
}
