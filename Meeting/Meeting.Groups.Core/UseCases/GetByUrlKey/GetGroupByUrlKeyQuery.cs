namespace Meeting.Groups.Core
{
    using MediatR;

    public class GetGroupByUrlKeyQuery : IRequest<GetGroupByIdQueryResult>
    {
        public GetGroupByUrlKeyQuery(string urlKey)
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
