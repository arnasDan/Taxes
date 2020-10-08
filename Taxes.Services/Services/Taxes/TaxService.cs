using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Taxes.Core.Exceptions;
using Taxes.Core.Extensions;
using Taxes.Core.Repositories.Taxes;
using Taxes.Models.Taxes;

namespace Taxes.Core.Services.Taxes
{
    public class TaxService : ITaxService
    {
        private readonly ITaxRepository _taxRepository;

        public TaxService(ITaxRepository taxRepository)
        {
            _taxRepository = taxRepository;
        }

        public async Task<TaxReadModel> Create(TaxWriteModel model, CancellationToken cancellationToken)
        {
            ValidatePeriod(model.PeriodStartDate, model.PeriodLength());

            return await _taxRepository.Create(model, cancellationToken);
        }

        public async Task<TaxReadModel> Update(Guid id, TaxWriteModel model, CancellationToken cancellationToken)
        {
            ValidatePeriod(model.PeriodStartDate, model.PeriodLength());

            return await _taxRepository.Update(id, model, cancellationToken);
        }

        public Task<IEnumerable<TaxReadModel>> List(CancellationToken cancellationToken) =>
            _taxRepository.GetAll(cancellationToken);

        public async Task<TaxReadModel> GetTaxForDate(Guid municipalityId, DateTime date, CancellationToken cancellationToken)
        {
            var taxes = (await _taxRepository.GetByMunicipalityId(municipalityId, cancellationToken))
                .Where(t => t.PeriodStartDate <= date && date <= t.PeriodEndDate)
                .ToList();

            if (!taxes.Any())
                throw new DomainException(DomainExceptionType.NotFound, $"No taxes exist for municipality {municipalityId} for date {date}");

            return taxes
                .OrderBy(t => t.PeriodLength())
                .First();
        }

        public Task<TaxReadModel> GetById(Guid taxId, CancellationToken cancellationToken) =>
            _taxRepository.GetById(taxId, cancellationToken);

        private static void ValidatePeriod(DateTime startDate, int periodLength)
        {
            if (periodLength == 1)
                return;

            const int daysInWeek = 7;
            var daysInMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
            var daysInYear = DateTime.IsLeapYear(startDate.Year) ? 366 : 365;

            if (startDate.DayOfYear == 1 && periodLength == daysInYear)
                return;

            if (startDate.Month == 1 && periodLength == daysInMonth)
                return;

            if (startDate.DayOfWeek == DayOfWeek.Monday && periodLength == daysInWeek)
                return;

            throw new DomainException(DomainExceptionType.InvalidDate, "Incorrect or unsupported period range");
        }
    }
}