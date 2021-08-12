using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Kernel.Domain.Model.Entities
{
    public class Entity<TKey> : IEquatable<Entity<TKey>>, IEntity
    {
        [Key]
        [DisplayName("Key")]
        public TKey Key { get; set; }

        public string id { get; set; }
        
        public string Discriminator { get; set; }

        public bool Equals(Entity<TKey> other)
        {
            if (other == null) return false;

            if (other.Key.Equals(default(TKey)) && Key.Equals(default(TKey)))
                return ReferenceEquals(this, other);

            return GetType() == other.GetType() && Key.Equals(other.Key);
        }

        public override bool Equals(object obj) => Equals(obj as Entity<TKey>);

        public override int GetHashCode() => Key.GetHashCode();

        public Entity<TKey> Clone() => (Entity<TKey>)MemberwiseClone();

        public override string ToString() => Key.ToString();
    }
}