using AplicatieFotbal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AplicatieFotbal.DataAccess.Data.Repository.IRepository
{
    public interface IMatchRepository : IRepository<Match>
    {
        Match Get(string name);
        bool CheckIfExist(string name);
    }
}
