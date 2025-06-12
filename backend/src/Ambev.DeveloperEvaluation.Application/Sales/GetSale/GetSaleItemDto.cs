namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Represents an item included in the sale.
    /// </summary>
    public class GetSaleItemDto
    {
        /// <summary>
        /// Gets or sets the identifier of the product sold.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product sold.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount applied to this item.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the total amount for this item after discount.
        /// </summary>
        public decimal TotalAmount { get; set; }
    }
}
