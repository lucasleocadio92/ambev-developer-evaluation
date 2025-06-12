using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// API response model for CreateSale operation.
/// </summary>
public class CreateSaleResponse
{
    /// <summary>
    /// The unique identifier of the created sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The total value of the sale including discounts.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// The current status of the sale.
    /// </summary>
    public SaleStatus Status { get; set; }
}
