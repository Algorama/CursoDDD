using Kernel.Domain.Model.Dtos;

namespace Kernel.Domain.Model.Helpers
{
    public interface ITokenHelper
    {
        string GenerateNewSecret();
        string GetTokenString(Token token);
        Token GetToken(string tokenString);
    }
}