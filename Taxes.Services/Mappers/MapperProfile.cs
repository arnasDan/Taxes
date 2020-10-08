using AutoMapper;
using Taxes.Models.Municipalities;
using Taxes.Models.Taxes;
using Taxes.Storage.Entities;

namespace Taxes.Core.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<TaxWriteModel, Tax>();
            CreateMap<Tax, TaxReadModel>();

            CreateMap<MunicipalityWriteModel, Municipality>();
            CreateMap<Municipality, MunicipalityReadModel>();
        }
    }
}