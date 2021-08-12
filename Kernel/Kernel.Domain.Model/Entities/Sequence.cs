namespace Kernel.Domain.Model.Entities
{
    public class Sequence : Entity<string>, IAggregateRoot
    {
        public long NextId { get; set; }
    }
}