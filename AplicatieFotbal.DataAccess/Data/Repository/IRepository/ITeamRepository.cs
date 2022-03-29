using AplicatieFotbal.Models;

namespace AplicatieFotbal.DataAccess.Data.Repository.IRepository
{
    public interface ITeamRepository : IRepository<Team>
    {
        Team Get(string name);
        bool CheckIfExist(string name);
    }
}
