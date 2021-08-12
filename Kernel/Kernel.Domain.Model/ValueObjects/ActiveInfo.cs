using System;

namespace Kernel.Domain.Model.ValueObjects
{
    public class ActiveInfo : ValueObject<ActiveInfo>
    {
        public bool Active { get; set; }
        public DateTime? InactivityDate { get; set; }

        public ActiveInfo()
        {
            Active = true;
        }

        public void Deactivate()
        {
            Active = false;
            InactivityDate = DateTime.Now;
        }

        public void Activate()
        {
            Active = true;
            InactivityDate = null;
        }

        public override string ToString() => !Active
            ? $"Essa entidade foi inativada em: {InactivityDate}"
            : "Essa entidade está ativa";
    }
}
