using System;

namespace Taxes.Storage.Entities
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }

        DateTime CreatedOn { get; set; }
    }
}