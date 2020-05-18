namespace Meeting.Topics.Core
{
    using System;

    public class AggregateException : Exception
    {
        protected AggregateException(string message) : base(message) { }

        public Guid Id { get; } = Guid.NewGuid();
    }
}
