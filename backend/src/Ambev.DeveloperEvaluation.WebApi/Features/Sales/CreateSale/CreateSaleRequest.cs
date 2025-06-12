using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Represents a request to create a new sale in the system.
    /// </summary>
    public class CreateSaleRequest
    {
        /// <summary>
        /// Gets or sets the customer who is making the purchase.
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets the branch where the sale is taking place.
        /// </summary>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of items included in the sale.
        /// </summary>
        public List<CreateSaleItemRequest> Items { get; set; } = [];

        /// <summary>
        /// Gets or sets the current status of the sale.
        /// </summary>
        public SaleStatus Status { get; set; }
    }
}
