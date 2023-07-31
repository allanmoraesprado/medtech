using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public abstract class Repository<TEntity> : DbContext, IRepository<TEntity> where TEntity : EntityBase, new()
    {
        public DbContext _dbContext;
        public DbSet<TEntity> _dbSet;
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }
        public void Create(TEntity entity)
        {
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = new TEntity { Id = id };
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

        public TEntity Read(int id)
        {          
            return _dbSet.Where(p => p.Id == id).FirstOrDefault();
        }

        public IEnumerable<TEntity> Read()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public void Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
