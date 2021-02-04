using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface ICsvRepository : IBaseRepository<Csv, int>
    {
        public bool IsAny(int id);
    }
}
