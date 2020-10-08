using System;

namespace Taxes.Models.Taxes
{
    public class TaxReadModel : IReadModel<Guid>
    {
        public Guid Id { get; set; }
    }
}