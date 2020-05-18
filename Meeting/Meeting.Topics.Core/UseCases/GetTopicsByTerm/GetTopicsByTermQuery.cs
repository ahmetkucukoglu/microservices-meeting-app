namespace Meeting.Topics.Core
{
    using MediatR;
    using System.Collections.Generic;

    public class GetTopicsByTermQuery : IRequest<IEnumerable<GetTopicsByTermQueryResult>>
    {
        public GetTopicsByTermQuery(string term)
        {
            if (string.IsNullOrEmpty(term) || term.Length < 3)
            {
                throw new InvalidTermException();
            }

            Term = term;
        }

        public string Term { get; }
    }
}
