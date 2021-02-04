using DAL.Models;
using DAL.Interfaces;
using System.Linq;

namespace DAL.Repositories
{
    public class CsvRepository : BaseRepository<Csv, int>, ICsvRepository
    {
        public CsvRepository(csvdbContext context) : base(context)
        {
        }

        public bool IsAny(int id)
        {
            return _dbContext.Csvs.Any(e => e.Id == id);
        }
    }
}
