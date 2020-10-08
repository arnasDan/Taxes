
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Taxes.Core.Exceptions;
using Taxes.Models;
using Taxes.Storage;
using Taxes.Storage.Entities;

namespace Taxes.Core.Repositories
{
    public abstract class BaseRepository<TReadModel, TWriteModel, TEntity> : IRepository<TReadModel, TWriteModel, Guid>
        where TReadModel : IReadModel<Guid>
        where TEntity : BaseEntity
    {
        protected readonly IDatabaseContext DatabaseContext;
        protected readonly IMapper Mapper;

        protected BaseRepository(IDatabaseContext databaseContext, IMapper mapper)
        {
            DatabaseContext = databaseContext;
            Mapper = mapper;
        }

        public virtual async Task<TReadModel> GetById(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetEntityById(id, cancellationToken);

            return ConvertToReadModel(entity);
        }

        public virtual async Task<TReadModel> Create(TWriteModel model, CancellationToken cancellationToken)
        {
            var entity = await ConvertToEntity(model);

            await DatabaseContext.Set<TEntity>().AddAsync(entity, cancellationToken);
            await DatabaseContext.SaveChangesAsync(cancellationToken);

            return ConvertToReadModel(entity);
        }

        public virtual async Task CreateRange(IEnumerable<TWriteModel> models,
            CancellationToken cancellationToken)
        {
            var entities = await Task.WhenAll(models.Select(async m => await ConvertToEntity(m)));

            await DatabaseContext.Set<TEntity>().AddRangeAsync(entities);
            await DatabaseContext.SaveChangesAsync(cancellationToken);


        }

        public virtual async Task<TReadModel> Update(Guid id, TWriteModel model, CancellationToken cancellationToken)
        {
            var entityToUpdate = await GetEntityById(id, cancellationToken);
            var updatedEntity = UpdateEntity(entityToUpdate, model);
            await Validate(updatedEntity);

            await DatabaseContext.SaveChangesAsync(cancellationToken);

            return ConvertToReadModel(updatedEntity);
        }

        protected abstract TEntity UpdateEntity(TEntity entity, TWriteModel model);

        protected async Task<TEntity> GetEntityById(Guid id, CancellationToken cancellationToken)
        {
            var entity = await DatabaseContext.Set<TEntity>().FindAsync(id, cancellationToken);

            if (entity == null)
                throw new DomainException(DomainExceptionType.NotFound, $"{typeof(TEntity).Name} with id {id} not found");

            return entity;
        }

        protected virtual Task Validate(TEntity entity)
        {
            return Task.CompletedTask;
        }

        protected TReadModel ConvertToReadModel(TEntity entity) => Mapper.Map<TReadModel>(entity);

        protected async Task<TEntity> ConvertToEntity(TWriteModel model)
        {
            var entity = Mapper.Map<TEntity>(model);
            await Validate(entity);
            return entity;
        }
    }
}