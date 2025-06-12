namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// Represents the item in the sale that can be updated (e.g., quantity, price).
    /// </summary>
    public class UpdateSaleItemRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product.
        /// </summary>
        /// <value>The unique identifier of the product to be updated in the sale.</value>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the updated quantity of the product.
        /// </summary>
        /// <value>The updated quantity for the product in the sale.</value>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the updated unit price of the product.
        /// </summary>
        /// <value>The updated unit price of the product.</value>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the updated discount for the product.
        /// </summary>
        /// <value>The updated discount for the product in the sale.</value>
        public decimal Discount { get; set; }
    }
}
