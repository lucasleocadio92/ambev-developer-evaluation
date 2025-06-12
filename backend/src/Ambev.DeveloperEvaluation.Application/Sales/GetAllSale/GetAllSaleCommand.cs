using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSale
{
    /// <summary>
    /// Command for retrieving a paginated list of sales with optional sorting.
    /// </summary>
    /// <remarks>
    /// This command is used to fetch a list of sales with pagination, allowing you to specify the page number, page size, 
    /// and optional sorting order for the results. Sorting can be done on multiple fields using "asc" or "desc" for each field.
    /// </remarks>
    /// <param name="PageNumber">The page number to fetch (defaults to 1).</param>
    /// <param name="PageSize">The number of sales per page (defaults to 10).</param>
    /// <param name="Order">An optional string specifying the sorting order (e.g., "SaleDate asc, TotalAmount desc").</param>
    public record GetAllSaleCommand(int PageNumber, int PageSize, string? Order = null) : IRequest<GetAllSaleResult>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="GetAllSaleCommand"/> with the specified parameters.
        /// </summary>
        /// <param name="pageNumber">The page number to fetch (defaults to 1).</param>
        /// <param name="pageSize">The number of sales per page (defaults to 10).</param>
        /// <param name="order">An optional string specifying the sorting order (e.g., "SaleDate asc, TotalAmount desc").</param>
        /// <returns>A new instance of <see cref="GetAllSaleCommand"/>.</returns>
        public static GetAllSaleCommand Create(int pageNumber, int pageSize, string? order = null)
        {
            return new GetAllSaleCommand(pageNumber, pageSize, order);
        }
    }

}
