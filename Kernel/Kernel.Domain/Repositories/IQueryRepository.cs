using Kernel.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kernel.Domain.Repositories
{
    public interface IQueryRepository<T> where T : class, IEntity
    {
        IQueryable<T> Query { get; }
        Task<T> Get(object id);
        Task<T> Get(Expression<Func<T, bool>> @where);
        Task<IList<T>> List();
        Task<IList<T>> List(Expression<Func<T, bool>> @where);
    }
}
