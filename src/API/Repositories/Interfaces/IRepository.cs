namespace API.Repositories.Interfaces
{
    /// <summary>
    /// Interface for generic repository operations.
    /// </summary>
    /// <typeparam name="Tkey">The type of the entity's primary key.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepository<Tkey, TEntity> where TEntity : class
    {
        /// <summary>
        /// Adds an entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The added entity.</returns>
        /// <exception cref="EntityAlreadyExistsException{Enetity}">Throw when Enetity Already Exists</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to add the entity.</exception>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Deletes an entity from the repository.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>True if the delete operation was successful, false otherwise.</returns>
        /// <exception cref="EntityNotFoundException{TEntity}">Thrown when No Entity found.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to delete the entity.</exception>

        Task<bool> DeleteAsync(Tkey id);

        /// <summary>
        /// Retrieves an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>The entity if found; otherwise, throws an <see cref="EntityNotFoundException"/>.</returns>
        /// <exception cref="EntityNotFoundException{TEntity}">Thrown when No Entity found.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to delete the entity.</exception>
        Task<TEntity> GetAsync(Tkey id);

        /// <summary>
        /// Retrieves all entities of type <typeparamref name="TEntity"/>.
        /// </summary>
        /// <returns>A collection of entities.</returns>
        /// <exception cref="UnableToDoActionException">Thrown when unable to delete the entity.</exception>

        Task<IEnumerable<TEntity>> GetAsync();

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>The updated entity.</returns>
        /// <exception cref="UnableToDoActionException">Thrown when unable to update the entity.</exception>
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}