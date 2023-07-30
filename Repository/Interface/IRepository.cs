using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
        TEntity Read(int id);
        IEnumerable<TEntity> Read();
    }
}
