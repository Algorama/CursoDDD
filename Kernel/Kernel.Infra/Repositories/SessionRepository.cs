using Kernel.Domain.Model.Entities;
using Kernel.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kernel.Infra.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly DbContext _context;

        public SessionRepository(DbContext context)
        {
            _context = context;
        }

        public IQueryRepository<T> GetQueryRepository<T>() where T : class, IEntity => new QueryRepository<T>(_context.Set<T>());
        public IRepository<T> GetRepository<T>() where T : class, IAggregateRoot => new Repository<T>(_context.Set<T>());

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
