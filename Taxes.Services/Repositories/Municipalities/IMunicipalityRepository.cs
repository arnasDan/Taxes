using System;
using Taxes.Models.Municipalities;
using Taxes.Storage.Entities;

namespace Taxes.Core.Repositories.Municipalities
{
    public interface IMunicipalityRepository : IRepository<MunicipalityReadModel, MunicipalityWriteModel, Guid>
    {
        
    }
}