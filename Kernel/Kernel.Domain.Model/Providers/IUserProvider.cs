using Kernel.Domain.Model.Dtos;
using System.Threading.Tasks;

namespace Kernel.Domain.Model.Providers
{
    public interface IUserProvider
    {
        Task SaveTokenString(string tokenString);
        Task<Token> GetToken();
        Task RemoveToken();
    }
}