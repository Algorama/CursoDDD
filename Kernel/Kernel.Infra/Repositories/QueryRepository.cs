using Kernel.Domain.Model.Entities;
using Kernel.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kernel.Infra.Repositories
{
    public class QueryRepository<T> : IQueryRepository<T> where T : class, IEntity
    {
        protected readonly DbSet<T> DbSet;

        public QueryRepository(DbSet<T> set)
        {
            DbSet = set;
        }

        public IQueryable<T> Query => DbSet.AsQueryable<T>();
        public async Task<T> Get(object id) => await DbSet.FindAsync(id);
        public async Task<T> Get(Expression<Func<T, bool>> where) => await DbSet.Where(where).FirstOrDefaultAsync();
        public async Task<IList<T>> List() => await DbSet.ToListAsync();
        public async Task<IList<T>> List(Expression<Func<T, bool>> where) => await DbSet.Where(where).ToListAsync();
    }
}
