namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Represents an item within a sale that is being updated.
    /// </summary>
    /// <remarks>
    /// This command is used to update individual items in a sale,
    /// including quantity, unit price, and discount. These fields
    /// will be used to recalculate the total amount for the item.
    /// </remarks>
    public class UpdateSaleItemCommand
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product in the sale.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount applied to the item.
        /// </summary>
        public decimal Discount { get; set; }
    }
}
