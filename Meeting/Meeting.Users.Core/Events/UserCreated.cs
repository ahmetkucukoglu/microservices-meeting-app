namespace Meeting.Users.Core
{
    public partial class V1
    {
        public class UserCreated : Event
        {
            public UserCreated(string userId, string userName)
            {
                UserId = userId;
                UserName = userName;
            }

            public string UserId { get; }
            public string UserName { get; }
        }
    }
}
