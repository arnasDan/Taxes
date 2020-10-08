using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Taxes.Models.Municipalities;

namespace Taxes.Core.Services.Municipalities
{
    public interface IMunicipalityService
    {
        Task<MunicipalityReadModel> Create(MunicipalityWriteModel model, CancellationToken cancellationToken);
        Task<MunicipalityReadModel> Update(Guid id, MunicipalityWriteModel model, CancellationToken cancellationToken);
        Task<MunicipalityReadModel> GetById(Guid municipalityId, CancellationToken cancellationToken);
        Task<IEnumerable<MunicipalityReadModel>> List(CancellationToken cancellationToken);
    }
}