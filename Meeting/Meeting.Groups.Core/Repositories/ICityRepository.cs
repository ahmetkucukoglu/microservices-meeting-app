namespace Meeting.Groups.Core
{
    using System;
    using System.Threading.Tasks;

    public interface ICityRepository
    {
        Task<bool> ExistsCity(Guid cityId);
    }
}
