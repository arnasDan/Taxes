using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Taxes.Core.Exceptions;
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

        protected override async Task Validate(Municipality entity, CancellationToken cancellationToken)
        {
            if (await DatabaseContext.Municipalities.AnyAsync(m => m.Id != entity.Id && m.Name == entity.Name, cancellationToken))
                throw new DomainException(DomainExceptionType.AlreadyExists, "Municipality with the same name already exists");
        }
    }
}