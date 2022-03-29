using AplicatieFotbal.Models;

namespace AplicatieFotbal.DataAccess.Data.Repository.IRepository
{
    public interface ILeagueRepository : IRepository<League>
    {
        League Get(string name);
        bool CheckIfExist(string name);
        League GetAllData(int id);
    }
}
