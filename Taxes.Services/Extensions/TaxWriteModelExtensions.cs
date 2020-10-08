using Taxes.Models.Taxes;

namespace Taxes.Core.Extensions
{
    public static class TaxWriteModelExtensions
    {
        public static int PeriodLength(this TaxWriteModel model) => (model.PeriodEndDate - model.PeriodStartDate).Days + 1;
    }
}