using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    /// <summary>
    /// Represents the response returned after successfully cancelling a sale.
    /// </summary>
    /// <remarks>
    /// This response contains the unique identifier of the cancelled sale,
    /// as well as updated details such as the new status, total amount, and 
    /// the original sale date.
    /// </remarks>
    public class CancelSaleResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the cancelled sale.
        /// </summary>
        /// <value>A GUID that uniquely identifies the sale in the system.</value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the status of the sale after cancellation.
        /// </summary>
        /// <value>The current status of the sale (e.g., Cancelled).</value>
        public SaleStatus Status { get; set; }
    }
}