using System.ComponentModel.DataAnnotations;

namespace Kernel.Domain.Model.Dtos
{
    public class LoginRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato do E-Mail inválido!")]
        public string Email { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Senha inválida")]
        public string Password { get; set; }
    }
}