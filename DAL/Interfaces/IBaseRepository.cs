using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IBaseRepository<TEntity, TId> where TEntity : class
    {
        public IQueryable<TEntity> GetAll();

        public Task<TEntity> GetById(TId Id);

        public Task Insert(TEntity Entity);

        public TEntity Update(TEntity Entity);

        public Task Delete(TId Id);

        public void Save();

        public Task SaveAsync();
    }
}
