using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Taxes.Models.Taxes
{
    public class TaxWriteModel
    {
        [JsonRequired, Range(0.0, 99.99)]
        public decimal Value { get; set; }
        
        [JsonRequired]
        public DateTime PeriodStartDate { get; set; }

        [JsonRequired]
        public DateTime PeriodEndDate { get; set; }

        [JsonRequired]
        public Guid MunicipalityId { get; set; }
    }
}
