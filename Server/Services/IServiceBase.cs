using System.Linq.Expressions;

namespace Server.Services
{
    public interface IServiceBase<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> condition);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveDatabaseAsync();
    }
}