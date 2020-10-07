using System;

namespace Taxes.Storage.Entities
{
    public abstract class BaseEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        protected BaseEntity()
        {
            CreatedOn = DateTime.UtcNow;
        }
    }
}