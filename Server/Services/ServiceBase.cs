using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.Services
{
    public class ServiceBase<T> : IServiceBase<T> where T : class
    {
        private readonly ApplicationDBContext _context;
        public ServiceBase(ApplicationDBContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> condition)
        {
            return _context.Set<T>().Where(condition).AsNoTracking();
        }

        public async Task SaveDatabaseAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}