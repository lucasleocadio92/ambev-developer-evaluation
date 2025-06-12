namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    /// <summary>
    /// Represents an item in the sale
    /// </summary>
    public class GetSaleItemResponse
    {
        /// <summary>
        /// The unique identifier of the product in the sale
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// The quantity of the product in the sale
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The unit price of the product in the sale
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// The discount applied to the product in the sale
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// The total amount for this item after discount.
        /// </summary>
        public decimal TotalAmount { get; set; }
    }
}
