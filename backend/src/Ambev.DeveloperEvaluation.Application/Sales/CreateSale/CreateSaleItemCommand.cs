using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Command for creating a sale item.
    /// </summary>
    public class CreateSaleItemCommand
    {
        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product.
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

        public CreateSaleItemCommand(Guid productId, int quantity, decimal unitPrice, decimal discount = 0)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
        }
    }
}
