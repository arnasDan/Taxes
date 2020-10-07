using System.Collections;
using System.Collections.Generic;

namespace Taxes.Storage.Entities
{
    public class Municipality : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Tax> Taxes { get; set; }
    }
}