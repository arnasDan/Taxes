using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Taxes.Models.Taxes;

namespace Taxes.Core.Services.Taxes
{
    public interface ITaxService
    {
        Task<TaxReadModel> Create(TaxWriteModel model, CancellationToken cancellationToken);
        Task<TaxReadModel> Update(Guid id, TaxWriteModel model, CancellationToken cancellationToken);
        Task<IEnumerable<TaxReadModel>> List(CancellationToken cancellationToken);
        Task<TaxReadModel> GetTaxForDate(Guid municipalityId, DateTime date, CancellationToken cancellationToken);
        Task<TaxReadModel> GetById(Guid taxId, CancellationToken cancellationToken);
    }
}