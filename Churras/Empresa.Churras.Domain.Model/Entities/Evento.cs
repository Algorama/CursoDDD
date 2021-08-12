using Kernel.Domain.Model.Entities;
using System;

namespace Empresa.Churras.Domain.Model.Entities
{
    public class Evento : EntityKeySeq
    {
        public string DonoDaCasa { get; set; }
        public string TipoEvento { get; set; }
        public DateTime Dia { get; set; }
        public string Periodo { get; set; }
        public string ColegasQueConfirmaramPresenca { get; set; }
        public string OQueCadaColegaVaiLevar { get; set; }
    }
}
