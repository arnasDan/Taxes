using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Taxes.Core.Exceptions;
using Taxes.Models.Taxes;
using Taxes.Storage;
using Taxes.Storage.Entities;

namespace Taxes.Core.Repositories.Taxes
{
    public class TaxRepository : BaseRepository<TaxReadModel, TaxWriteModel, Tax>, ITaxRepository
    {
        public TaxRepository(IDatabaseContext databaseContext, IMapper mapper) : base(databaseContext, mapper)
        {
        }

        protected override Tax UpdateEntity(Tax entity, TaxWriteModel model)
        {
            entity.Value = model.Value;
            entity.PeriodStartDate = model.PeriodStartDate;
            entity.PeriodEndDate = model.PeriodEndDate;
            entity.MunicipalityId = model.MunicipalityId;

            return entity;
        }

        protected override async Task Validate(Tax entity)
        {
            if (!await DatabaseContext.Taxes.AnyAsync(t => t.MunicipalityId == entity.MunicipalityId))
                throw new DomainException(DomainExceptionType.NotFound,
                    $"Municipality {entity.MunicipalityId} does not exist");
        }

        public async Task<IEnumerable<TaxReadModel>> GetByMunicipalityId(Guid municipalityId)
        {
            return (await DatabaseContext.Taxes
                .Where(t => t.MunicipalityId == municipalityId)
                .ToListAsync())
                .Select(ConvertToReadModel);
        }
    }
}