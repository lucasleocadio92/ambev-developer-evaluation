using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Implementation of ISaleRepository using Entity Framework Core
    /// </summary>
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of SaleRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new sale in the database
        /// </summary>
        /// <param name="sale">The sale entity to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created sale</returns>
        public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _context.Sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        /// <summary>
        /// Retrieves a sale by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the sale</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale if found, null otherwise</returns>
        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Include(s => s.Items)   // Include items if necessary
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        /// <summary>
        /// Retrieves a list of sales filtered by customer or status
        /// </summary>
        /// <param name="customer">Customer filter (optional)</param>
        /// <param name="status">Sale status filter (optional)</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A list of sales matching the criteria</returns>
        public async Task<IEnumerable<Sale>> GetByCustomerOrStatusAsync(string? customer = null, string? status = null, CancellationToken cancellationToken = default)
        {
            IQueryable<Sale> query = _context.Sales;

            if (!string.IsNullOrEmpty(customer))
            {
                query = query.Where(s => s.Customer.Name.Contains(customer));
            }

            if (!string.IsNullOrEmpty(status) && Enum.TryParse<SaleStatus>(status, out var saleStatus))
            {
                query = query.Where(s => s.Status == saleStatus);
            }

            return await query.Include(s => s.Items)   // Include items if necessary
                               .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Retrieves all sales with pagination and optional sorting from the repository.
        /// </summary>
        /// <param name="pageNumber">The page number for pagination. Default is 1.</param>
        /// <param name="pageSize">The number of items per page. Default is 10.</param>
        /// <param name="order">Optional sorting criteria (e.g., "SaleDate ASC", "TotalAmount DESC").</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A paginated result containing a list of sales and metadata such as total count and total pages.</returns>
        public async Task<PagedResult<Sale?>> GetAllAsync(int pageNumber = 1, int pageSize = 10, string? order = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Set<Sale>().AsNoTracking();

            if (!string.IsNullOrEmpty(order))
            {
                query = ApplyOrdering(query, order);
            }

            var totalCount = await query!.CountAsync(cancellationToken);
            var items = await query!
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return PagedResult<Sale?>.Create(items, totalCount, pageSize, pageNumber);
        }

        /// <summary>
        /// Updates a sale in the database
        /// </summary>
        /// <param name="sale">The sale entity to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated sale</returns>
        public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        private IQueryable<Sale> ApplyOrdering(IQueryable<Sale> query, string order)
        {
            var orderByFields = order.Split(',', StringSplitOptions.RemoveEmptyEntries);
            IOrderedQueryable<Sale>? orderedQueryable = null;

            foreach (var field in orderByFields)
            {
                var trimmedField = field.Trim();
                var isDescending = trimmedField.EndsWith(" desc", StringComparison.OrdinalIgnoreCase);
                trimmedField = isDescending ? trimmedField.Substring(0, trimmedField.Length - 5).Trim() : trimmedField.Substring(0, trimmedField.Length - 4).Trim();
                var propertyName = char.ToUpper(trimmedField[0]) + trimmedField.Substring(1);  // Capitaliza o primeiro caractere

                var orderExpression = EF.Property<object>(Expression.Parameter(typeof(Sale), "p"), propertyName);

                if (orderedQueryable == null)
                {
                    orderedQueryable = isDescending
                        ? query.OrderByDescending(x => EF.Property<object>(x, propertyName))
                        : query.OrderBy(x => EF.Property<object>(x, propertyName));
                }
                else
                {
                    orderedQueryable = isDescending
                        ? orderedQueryable.ThenByDescending(x => EF.Property<object>(x, propertyName))
                        : orderedQueryable.ThenBy(x => EF.Property<object>(x, propertyName));
                }
            }

            return orderedQueryable ?? query;  // Caso não tenha ordenação, retorna o query sem alteração
        }
    }
}
