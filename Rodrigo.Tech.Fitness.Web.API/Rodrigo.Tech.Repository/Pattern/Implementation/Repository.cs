using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Rodrigo.Tech.Respository.Context;
using Rodrigo.Tech.Repository.Pattern.Interface;
using System.Linq.Expressions;

namespace Rodrigo.Tech.Repository.Pattern.Implementation
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly FitnessDatabase _dbContext;
        private readonly DbSet<T> _entities;

        public Repository(FitnessDatabase dbContext)
        {
            _dbContext = dbContext;
            _entities = dbContext.Set<T>();
        }

        /// <inheritdoc/>
        public async Task<T> Add(T entity)
        {
            T doesExit = await _entities.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (doesExit != null)
            {
                return default;
            }

            await _entities.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        /// <inheritdoc/>
        public async Task<bool> Delete(Guid id)
        {
            T entity = await _entities.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return false;
            }

            _entities.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc/>
        public async Task<T> Get(Guid id)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc/>
        public async Task<List<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<T> GetWithExpression(Expression<Func<T, bool>> predicate)
        {
            return await _entities.Where(predicate).FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<IList<T>> GetAllWithExpression(Expression<Func<T, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<T> Update(T entity)
        {
            T doesExit = await _entities.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (doesExit == null)
            {
                return default;
            }

            _entities.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}