using System;

namespace Taxes.Models.Taxes
{
    public class TaxWriteModel
    {
        public decimal Value { get; set; }
        
        public DateTime PeriodStartDate { get; set; }
        public DateTime PeriodEndDate { get; set; }

        public Guid MunicipalityId { get; set; }
    }
}
