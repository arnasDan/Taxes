using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taxes.Models.Taxes;

namespace Taxes.Core.Repositories.Taxes
{
    public interface ITaxRepository : IRepository<TaxReadModel, TaxWriteModel, Guid>
    {
        Task<IEnumerable<TaxReadModel>> GetByMunicipalityId(Guid municipalityId);
    }
}