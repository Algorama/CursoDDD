using Kernel.Domain.Model.Entities;
using Kernel.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Kernel.Infra.Repositories
{
    public class Repository<T> : QueryRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public Repository(DbSet<T> set) : base(set) { }
        public async Task Insert(T entity) => await DbSet.AddAsync(entity);
        public void Update(T entity) => DbSet.Update(entity);
        public void Delete(T entity) => DbSet.Remove(entity);
    }
}