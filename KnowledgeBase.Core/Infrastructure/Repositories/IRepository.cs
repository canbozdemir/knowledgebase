using System;
using System.Linq;
using System.Linq.Expressions;

namespace KnowledgeBase.Core.Infrastructure.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);
        TEntity GetBy(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> All();
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}