using System.Threading.Tasks;
using AutoMapper;
using Taxes.Models.Municipalities;
using Taxes.Storage;
using Taxes.Storage.Entities;

namespace Taxes.Core.Repositories.Municipalities
{
    public class MunicipalityRepository : BaseRepository<MunicipalityReadModel, MunicipalityWriteModel, Municipality>, IMunicipalityRepository
    {
        public MunicipalityRepository(IDatabaseContext databaseContext, IMapper mapper) : base(databaseContext, mapper)
        {
        }

        protected override Municipality UpdateEntity(Municipality entity, MunicipalityWriteModel model)
        {
            entity.Name = model.Name;

            return entity;
        }
    }
}