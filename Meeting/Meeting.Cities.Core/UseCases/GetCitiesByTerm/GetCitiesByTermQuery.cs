namespace Meeting.Cities.Core
{
    using MediatR;
    using System.Collections.Generic;

    public class GetCitiesByTermQuery : IRequest<IEnumerable<GetCitiesByTermQueryResult>>
    {
        public GetCitiesByTermQuery(string term)
        {
            if (string.IsNullOrEmpty(term) || term.Length < 3)
            {
                throw new InvalidTermException();
            }

            Term = term;
        }

        public string Term { get;  }
    }
}
