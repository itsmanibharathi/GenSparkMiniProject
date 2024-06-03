using API.Context;
using API.Exceptions;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace API.Repositories
{
    /// <summary>
    /// Generic repository implementation for CRUD operations.
    /// </summary>
    /// <typeparam name="Tkey">The type of the entity's primary key.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class Repository<Tkey, TEntity> : IRepository<Tkey, TEntity> where TEntity : class
    {
        protected readonly DBGenSparkMinirojectContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{Tkey, TEntity}"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public Repository(DBGenSparkMinirojectContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds an entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The added entity.</returns>
        /// <exception cref="EntityAlreadyExistsException{Enetity}">Throw when Enetity Already Exists</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to add the entity.</exception>
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                if (await IsDuplicate(entity))
                {
                    throw new EntityAlreadyExistsException<TEntity>(entity);
                }
                _context.Set<TEntity>().Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (EntityAlreadyExistsException<TEntity>)
            {
                throw;
            }

            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to add entity", e);
            }
        }
        [ExcludeFromCodeCoverage]
        public virtual async Task<bool> IsDuplicate(TEntity entity)
        {
            return await Task.FromResult(false);
        }

        /// <summary>
        /// Deletes an entity from the repository.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>True if the delete operation was successful, false otherwise.</returns>
        /// <exception cref="EntityNotFoundException{TEntity}">Thrown when No Entity found.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to delete the entity.</exception>

        public virtual async Task<bool> DeleteAsync(Tkey id)
        {
            try
            {
                var entity = await GetAsync(id);
                _context.Set<TEntity>().Remove(entity);
                return await _context.SaveChangesAsync() > 0;

            }
            catch (EntityNotFoundException<TEntity>)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to delete entity", e);
            }
        }

        /// <summary>
        /// Retrieves an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>The entity if found; otherwise, throws an <see cref="EntityNotFoundException"/>.</returns>
        /// <exception cref="EntityNotFoundException{TEntity}">Thrown when No Entity found.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to retrieve the entity.</exception>
        /// 
        public virtual async Task<TEntity> GetAsync(Tkey id)
        {
            try
            {
                return await _context.Set<TEntity>().FindAsync(id) ?? throw new EntityNotFoundException<TEntity>();
            }
            catch (EntityNotFoundException<TEntity>)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to get entity", e);
            }
        }

        /// <summary>
        /// Retrieves all entities of type <typeparamref name="TEntity"/>.
        /// </summary>
        /// <returns>A collection of entities.</returns>
        /// <exception cref="UnableToDoActionException">Thrown when unable to retrieve entities.</exception>
        public virtual async Task<IEnumerable<TEntity>> GetAsync()
        {
            try
            {
                return await _context.Set<TEntity>().ToListAsync();
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to get entities", e);
            }
        }

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>The updated entity.</returns>
        /// <exception cref="EntityNotFoundException{TEntity}">Thrown when No Entity found.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to update the entity.</exception>
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                return await _context.SaveChangesAsync() > 0 ? entity : throw new UnableToDoActionException("Unable to update entity");
            }
            catch (UnableToDoActionException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to update entity", e);
            }
        }
    }
}
