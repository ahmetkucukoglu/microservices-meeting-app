namespace Meeting.Cities.Core
{
    using System;

    public partial class V1
    {
        public class CityCreated : Event
        {
            public CityCreated(Guid cityId, string cityName)
            {
                CityId = cityId;
                CityName = cityName;
            }

            public Guid CityId { get; }
            public string CityName { get; }
        }
    }
}
