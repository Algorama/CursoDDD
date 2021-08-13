using Empresa.Churras.Domain.Model.ValueObjects;
using Kernel.Domain.Model.Entities;

namespace Empresa.Churras.Domain.Model.Entities
{
    public class Colega : EntityKeySeq
    {
        public string Nome { get; set; }
        public Endereco Endereco { get; set; }
    }
}
