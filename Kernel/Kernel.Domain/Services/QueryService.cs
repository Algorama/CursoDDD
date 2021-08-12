using Kernel.Domain.Model.Entities;
using Kernel.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kernel.Domain.Services
{
    public class QueryService<T> where T : class, IEntity, new()
    {
        protected ISessionFactory SessionFactory { get; set; }

        public QueryService(ISessionFactory sessionFactory)
        {
            SessionFactory = sessionFactory;
        }

        public virtual async Task<T> Get(object key)
        {
            if (key == null)
                return new T();

            using var session = SessionFactory.OpenSession();
            var repo = session.GetQueryRepository<T>();
            return await repo.Get(key);
        }

        public virtual async Task<T> Get(Expression<Func<T, bool>> @where)
        {
            using var session = SessionFactory.OpenSession();
            var repo = session.GetQueryRepository<T>();
            return await repo.Get(where);
        }

        public virtual async Task<IList<T>> List()
        {
            using var session = SessionFactory.OpenSession();
            var repo = session.GetQueryRepository<T>();
            return await repo.List();
        }

        public virtual async Task<IList<T>> List(Expression<Func<T, bool>> @where)
        {
            using var session = SessionFactory.OpenSession();
            var repo = session.GetQueryRepository<T>();
            return await repo.List(where);
        }
    }
}
