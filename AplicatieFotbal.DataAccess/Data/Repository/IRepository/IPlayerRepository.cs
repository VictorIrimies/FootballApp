using AplicatieFotbal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AplicatieFotbal.DataAccess.Data.Repository.IRepository
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Player Get(string name);
        bool CheckIfExist(string name);
    }
}

