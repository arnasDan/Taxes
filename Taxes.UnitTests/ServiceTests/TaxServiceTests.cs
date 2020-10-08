using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Taxes.Core.Exceptions;
using Taxes.Core.Repositories.Taxes;
using Taxes.Core.Services.Taxes;
using Taxes.Models.Taxes;
using Xunit;

namespace Taxes.UnitTests.ServiceTests
{
    public class TaxServiceTests
    {
        private readonly ITaxService _service;
        private readonly Mock<ITaxRepository> _repository;

        public TaxServiceTests()
        {
            _repository = new Mock<ITaxRepository>();
            _service = new TaxService(_repository.Object);
        }

        [Fact]
        public async Task CanGetTaxByDate()
        {
            var taxes = GetTaxes();

            _repository
                .Setup(r => r.GetByMunicipalityId(Guid.Empty, CancellationToken.None))
                .Returns(Task.FromResult(taxes));

            var expectedTax = taxes.Last();

            var actualTax = await _service.GetTaxForDate(Guid.Empty, new DateTime(2020, 1, 30), CancellationToken.None);

            Assert.Equal(expectedTax.Value, actualTax.Value);
        }

        [Theory]
        [InlineData(2020, 1, 1, 2020, 12, 31)]
        [InlineData(2020, 1, 1, 2020, 1, 31)]
        [InlineData(2020, 1, 1, 2020, 1, 1)]
        [InlineData(2020, 10, 5, 2020, 10, 11)]
        public async Task CanSaveTaxesWithCorrectPeriods(int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay)
        {
            var tax = GetTax(new DateTime(startYear, startMonth, startDay), new DateTime(endYear, endMonth, endDay));

            await _service.Create(tax, CancellationToken.None);
        }

        [Theory]
        [InlineData(2020, 1, 1, 2020, 1, 2)]
        public async Task CannotSaveTaxesWithIncorrectPeriod(int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay)
        {
            var tax = GetTax(new DateTime(startYear, startMonth, startDay), new DateTime(endYear, endMonth, endDay));

            var exception = await Assert.ThrowsAsync<DomainException>(() => _service.Create(tax, CancellationToken.None));
            Assert.Equal(DomainExceptionType.InvalidDate, exception.Type);
        }

        public TaxWriteModel GetTax(DateTime periodStartDate, DateTime periodEndDate)
        {
            return new TaxReadModel
            {
                Id = Guid.NewGuid(),
                MunicipalityId = Guid.Empty,
                PeriodStartDate = periodStartDate,
                PeriodEndDate = periodEndDate,
                Value = 0.1m
            };
        }

        public IEnumerable<TaxReadModel> GetTaxes()
        {
            return new []
            {
                new TaxReadModel
                {
                    Id = Guid.NewGuid(),
                    MunicipalityId = Guid.Empty,
                    PeriodStartDate = new DateTime(2020, 1, 1),
                    PeriodEndDate = new DateTime(2020, 12, 31),
                    Value = 0.1m
                },
                new TaxReadModel
                {
                    Id = Guid.NewGuid(),
                    MunicipalityId = Guid.Empty,
                    PeriodStartDate = new DateTime(2020, 1, 1),
                    PeriodEndDate = new DateTime(2020, 1, 31),
                    Value = 0.5m
                },
            };
        }
    }
}
