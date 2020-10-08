using System;

namespace Taxes.Models.Municipalities
{
    public class MunicipalityReadModel : MunicipalityWriteModel, IReadModel<Guid>
    {
        public Guid Id { get; set; }
    }
}