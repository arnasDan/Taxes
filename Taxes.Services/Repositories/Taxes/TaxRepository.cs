using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        protected override async Task Validate(Tax entity, CancellationToken cancellationToken)
        {
            if (!await DatabaseContext.Municipalities.AnyAsync(t => t.Id == entity.MunicipalityId, cancellationToken))
                throw new DomainException(DomainExceptionType.ValidationError,  $"Municipality {entity.MunicipalityId} does not exist");

            if (await DatabaseContext.Taxes.AnyAsync(t => t.Id != entity.Id &&
                                                          t.PeriodStartDate == entity.PeriodStartDate &&
                                                          t.PeriodEndDate == entity.PeriodEndDate &&
                                                          t.MunicipalityId == entity.MunicipalityId, cancellationToken))
            {
                throw new DomainException(DomainExceptionType.AlreadyExists, "Tax with same period already exists for this municipality");
            }
    }

        public async Task<IEnumerable<TaxReadModel>> GetByMunicipalityId(Guid municipalityId, CancellationToken cancellationToken)
        {
            return (await DatabaseContext.Taxes
                .Where(t => t.MunicipalityId == municipalityId)
                .ToListAsync(cancellationToken))
                .Select(ConvertToReadModel);
        }
    }
}