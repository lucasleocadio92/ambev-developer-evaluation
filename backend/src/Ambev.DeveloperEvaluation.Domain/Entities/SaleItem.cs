namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a product item in the sale.
    /// </summary>
    public class SaleItem
    {
        /// <summary>
        /// Gets or sets the product identifier.
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
        /// Gets or sets the discount applied to this sale item.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the total amount of this sale item (after discount).
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Applies a discount to the sale item based on the quantity.
        /// </summary>
        public void ApplyDiscount()
        {
            if (Quantity >= 10 && Quantity <= 20)
            {
                Discount = UnitPrice * Quantity * 0.20m; // 20% discount
            }
            else if (Quantity >= 4)
            {
                Discount = UnitPrice * Quantity * 0.10m; // 10% discount
            }
            else
            {
                Discount = 0;
            }

            TotalAmount = (Quantity * UnitPrice) - Discount;
        }
    }
}
