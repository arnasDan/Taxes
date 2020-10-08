using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Taxes.Core.Repositories.Municipalities;
using Taxes.Models.Municipalities;

namespace Taxes.Core.Services.Municipalities
{
    public class MunicipalityService : IMunicipalityService
    {
        private readonly IMunicipalityRepository _municipalityRepository;

        public MunicipalityService(IMunicipalityRepository municipalityRepository)
        {
            _municipalityRepository = municipalityRepository;
        }
        
        public Task<MunicipalityReadModel> Create(MunicipalityWriteModel model, CancellationToken cancellationToken) =>
             _municipalityRepository.Create(model, cancellationToken);

        public Task<MunicipalityReadModel> Update(Guid id, MunicipalityWriteModel model, CancellationToken cancellationToken) =>
            _municipalityRepository.Update(id, model, cancellationToken);

        public Task<MunicipalityReadModel> GetById(Guid municipalityId, CancellationToken cancellationToken) =>
            _municipalityRepository.GetById(municipalityId, cancellationToken);

        public Task<IEnumerable<MunicipalityReadModel>> List(CancellationToken cancellationToken) =>
            _municipalityRepository.GetAll(cancellationToken);
    }
}