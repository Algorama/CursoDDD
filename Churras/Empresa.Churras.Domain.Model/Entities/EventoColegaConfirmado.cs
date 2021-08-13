using Kernel.Domain.Model.ValueObjects;

namespace Empresa.Churras.Domain.Model.Entities
{
    public class EventoColegaConfirmado : ValueObject
    {
        public long ColegaKey { get; set; }
        public string ColegaNome { get; set; }
        public string OQueVaiLevar { get; set; }
    }
}
