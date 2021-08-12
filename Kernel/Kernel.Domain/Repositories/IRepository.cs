using Kernel.Domain.Model.Entities;
using System.Threading.Tasks;

namespace Kernel.Domain.Repositories
{
    public interface IRepository<T> : IQueryRepository<T> 
        where T : class, IAggregateRoot
    {
        Task Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
