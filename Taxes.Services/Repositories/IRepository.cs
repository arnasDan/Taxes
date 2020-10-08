using System;
using System.Threading;
using System.Threading.Tasks;
using Taxes.Models;
using Taxes.Storage.Entities;

namespace Taxes.Core.Repositories
{
    public interface IRepository<TReadModel, TWriteModel, TKey>
        where TReadModel : IReadModel<TKey>
    {
        Task<TReadModel> GetById(TKey id, CancellationToken cancellationToken);
        Task<TReadModel> Create(TWriteModel model, CancellationToken cancellationToken);
        Task<TReadModel> Update(TKey id, TWriteModel model, CancellationToken cancellationToken);
    }
}