namespace Kernel.Domain.Repositories
{
    public interface ISessionFactory
    {
        ISessionRepository OpenSession();
    }
}
