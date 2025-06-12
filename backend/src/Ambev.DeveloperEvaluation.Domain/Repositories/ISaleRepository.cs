using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Repository interface for Sale entity operations
    /// </summary>
    public interface ISaleRepository
    {
        /// <summary>
        /// Creates a new sale in the repository
        /// </summary>
        /// <param name="sale">The sale entity to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created sale</returns>
        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a sale by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the sale</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale if found, null otherwise</returns>
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all sales with pagination and optional sorting from the repository.
        /// </summary>
        /// <param name="pageNumber">The page number for pagination. Default is 1.</param>
        /// <param name="pageSize">The number of items per page. Default is 10.</param>
        /// <param name="order">Optional sorting criteria (e.g., "SaleDate ASC", "TotalAmount DESC").</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A paginated result containing a list of sales and metadata such as total count and total pages.</returns>
        Task<PagedResult<Sale?>> GetAllAsync(int pageNumber = 1, int pageSize = 10, string? order = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates a sale in the repository
        /// </summary>
        /// <param name="sale">The sale entity to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated sale</returns>
        Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);
    }
}