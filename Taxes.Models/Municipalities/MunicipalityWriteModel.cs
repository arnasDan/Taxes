
using System.ComponentModel.DataAnnotations;

namespace Taxes.Models.Municipalities
{
    public class MunicipalityWriteModel
    {
        [Required]
        public string Name { get; set; }
    }
}