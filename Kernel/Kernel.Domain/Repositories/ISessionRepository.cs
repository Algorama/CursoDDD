using Kernel.Domain.Model.Entities;
using System;

namespace Kernel.Domain.Repositories
{
    public interface ISessionRepository : IDisposable
    {
        IQueryRepository<T> GetQueryRepository<T>() where T : class, IEntity;
        IRepository<T> GetRepository<T>() where T : class, IAggregateRoot;

        void SaveChanges();
    }
}
