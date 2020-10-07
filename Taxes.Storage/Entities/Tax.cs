using System;

namespace Taxes.Storage.Entities
{
    public class Tax : BaseEntity
    {
        public decimal Value { get; set; }
        
        public DateTime PeriodStartDate { get; set; }
        public DateTime PeriodEndDate { get; set; }

        public Guid MunicipalityId { get; set; }
        public virtual Municipality Municipality { get; set; }
    }
}