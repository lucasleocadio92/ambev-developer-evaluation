using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// API response model for the UpdateSale operation.
    /// </summary>
    public class UpdateSaleResponse
    {
        /// <summary>
        /// The unique identifier of the updated sale.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The total amount of the sale after the update.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// The date when the sale was created or updated.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// The current status of the sale.
        /// </summary>
        public SaleStatus Status { get; set; }
    }
}
