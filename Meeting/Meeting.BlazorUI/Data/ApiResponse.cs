namespace Meeting.BlazorUI.Data
{
    public class ApiResponse<T> where T : class
    {
        public int Code { get; set; }
        public string Error { get; set; }
        public T Result { get; set; }
    }
}
