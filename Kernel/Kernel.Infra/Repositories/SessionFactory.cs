using Kernel.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kernel.Infra.Repositories
{
    public class SessionFactory : ISessionFactory
    {
        public ISessionRepository OpenSession() => 
            new SessionRepository(IoC.Get<DbContext>());
    }
}
