namespace Meeting.Cities.Core
{
    using System;

    public partial class V1
    {
        public class CityCreated
        {
            public Guid CityId { get; set; }
            public string CityName { get; set; }
        }
    }
}
