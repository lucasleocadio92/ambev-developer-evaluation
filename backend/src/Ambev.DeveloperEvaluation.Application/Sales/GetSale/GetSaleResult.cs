using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Represents the response returned after retrieving a sale.
    /// </summary>
    /// <remarks>
    /// This response includes the sale's identifier, date, total amount, status,
    /// branch information, customer details, and the list of items in the sale.
    /// </remarks>
    public class GetSaleResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the date when the sale was made.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Gets or sets the total amount of the sale after discounts.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the status of the sale.
        /// </summary>
        public SaleStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the branch or location where the sale was made.
        /// </summary>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the customer information related to the sale.
        /// </summary>
        public GetCustomerDto Customer { get; set; } = new();

        /// <summary>
        /// Gets or sets the list of items included in the sale.
        /// </summary>
        public List<GetSaleItemDto> Items { get; set; } = [];
    }
}
