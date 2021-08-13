using System.ComponentModel;

namespace Empresa.Churras.Domain.Model.Enums
{
    public enum TipoEvento
    {
        [Description("Churrasco")]
        Churras = 0,

        Pizza = 1,

        Lanche = 2,

        Outros = 999
    }
}
