namespace Meeting.Events.Core
{
    using MediatR;

    public class GetEventByUrlKeyQuery : IRequest<GetEventByIdQueryResult>
    {
        public GetEventByUrlKeyQuery(string urlKey)
        {
            if (string.IsNullOrEmpty(urlKey))
            {
                throw new InvalidUrlKeyException();
            }

            UrlKey = urlKey;
        }

        public string UrlKey { get; }
    }
}
