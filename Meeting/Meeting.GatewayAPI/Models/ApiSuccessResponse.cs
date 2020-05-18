namespace Meeting.GatewayAPI
{
    public class ApiSuccessResponse<T> where T : class
    {
        public int Code { get; set; }
        public T Result { get; set; }
    }
}
