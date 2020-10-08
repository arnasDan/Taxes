using System;

namespace Taxes.Models.Taxes
{
    public class TaxReadModel : TaxWriteModel, IReadModel<Guid>
    {
        public Guid Id { get; set; }
    }
}