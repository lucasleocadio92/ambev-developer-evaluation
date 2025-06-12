using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Represents the response returned after successfully creating a new sale.
    /// </summary>
    /// <remarks>
    /// This response contains the unique identifier of the newly created sale,
    /// as well as other details such as the sale date, total amount, and status.
    /// </remarks>
    public class CreateSaleResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the newly created sale.
        /// </summary>
        /// <value>A GUID that uniquely identifies the created sale in the system.</value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the total amount of the sale after applying any discounts.
        /// </summary>
        /// <value>The total amount calculated for the sale.</value>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the date when the sale was created.
        /// </summary>
        /// <value>The date when the sale was made.</value>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Gets or sets the status of the sale.
        /// </summary>
        /// <value>The current status of the sale (e.g., Pending, Completed, etc.).</value>
        public SaleStatus Status { get; set; }
    }
}
