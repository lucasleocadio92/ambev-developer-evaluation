using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using System;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// Request model to update an existing sale.
    /// </summary>
    public class UpdateSaleRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale to be updated.
        /// </summary>
        /// <value>The GUID that uniquely identifies the sale to be updated.</value>
        [JsonIgnore]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the customer associated with the sale.
        /// </summary>
        /// <value>The customer associated with the sale.</value>
        public Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets the updated status of the sale.
        /// </summary>
        /// <value>The updated status of the sale (e.g., Pending, Completed, Canceled, etc.).</value>
        public SaleStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the branch where the sale was made.
        /// </summary>
        /// <value>The branch where the sale was processed.</value>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of items in the sale, allowing updates to item quantities, prices, or discounts.
        /// </summary>
        /// <value>A list of sale items to be updated.</value>
        public List<UpdateSaleItemRequest> Items { get; set; } = [];
    }
}
