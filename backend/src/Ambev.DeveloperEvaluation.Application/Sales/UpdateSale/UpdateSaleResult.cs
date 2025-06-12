using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Represents the response returned after successfully updating a sale.
    /// </summary>
    /// <remarks>
    /// This response contains the unique identifier of the updated sale,
    /// as well as the updated sale date, total amount, and status.
    /// </remarks>
    public class UpdateSaleResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the updated sale.
        /// </summary>
        /// <value>A GUID that uniquely identifies the sale in the system.</value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the total amount of the sale after applying any discounts.
        /// </summary>
        /// <value>The recalculated total amount for the sale.</value>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the date when the sale was last updated.
        /// </summary>
        /// <value>The last modification date of the sale.</value>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Gets or sets the status of the sale.
        /// </summary>
        /// <value>The current status of the sale (e.g., Pending, Completed, Canceled).</value>
        public SaleStatus Status { get; set; }

    }
}
