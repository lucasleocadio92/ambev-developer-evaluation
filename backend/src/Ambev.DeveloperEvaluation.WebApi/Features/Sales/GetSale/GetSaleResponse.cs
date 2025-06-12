using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    /// <summary>
    /// API response model for GetSale operation
    /// </summary>
    public class GetSaleResponse
    {
        /// <summary>
        /// The unique identifier of the sale
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The date when the sale was created
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// The total amount of the sale
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// The status of the sale
        /// </summary>
        public SaleStatus Status { get; set; }

        /// <summary>
        /// The branch where the sale was processed
        /// </summary>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// The customer associated with the sale
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// The list of items in the sale
        /// </summary>
        public List<GetSaleItemResponse> Items { get; set; } = [];
    }
}
