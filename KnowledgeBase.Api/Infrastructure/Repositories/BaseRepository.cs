using KnowledgeBase.Core.Data;
using KnowledgeBase.Core.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace KnowledgeBase.Api.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(KnowledgeBaseDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }
        
        public TEntity GetBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).SingleOrDefault();
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<TEntity> All()
        {
            return _dbSet;
        }

        public void Create(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(TEntity entity)
        {

        }
    }
}
