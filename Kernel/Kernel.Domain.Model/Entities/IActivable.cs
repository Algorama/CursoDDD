using Kernel.Domain.Model.ValueObjects;

namespace Kernel.Domain.Model.Entities
{
    public interface IActivable : IAggregateRoot
    {
        ActiveInfo ActiveInfo { get; set; }
    }
}
