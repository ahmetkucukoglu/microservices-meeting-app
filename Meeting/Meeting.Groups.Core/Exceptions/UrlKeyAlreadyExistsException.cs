namespace Meeting.Groups.Core
{
    public class UrlKeyAlreadyExistsException : AggregateException
    {
        public UrlKeyAlreadyExistsException() : base("Url key already exists.") { }
    }
}