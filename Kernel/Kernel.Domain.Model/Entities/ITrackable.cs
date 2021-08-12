using Kernel.Domain.Model.ValueObjects;

namespace Kernel.Domain.Model.Entities
{
    public interface ITrackable : IAggregateRoot
    {
        TrackableInfo TrackableInfo { get; set; }
    }
}
