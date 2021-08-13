using Kernel.Domain.Model.ValueObjects;

namespace Empresa.Churras.Domain.Model.ValueObjects
{
    public class EventoColegaConfirmado : ValueObject
    {
        public long ColegaKey { get; set; }
        public string ColegaNome { get; set; }
        public string OQueVaiLevar { get; set; }
    }
}
