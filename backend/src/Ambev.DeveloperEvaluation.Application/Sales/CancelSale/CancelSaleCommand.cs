using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    /// <summary>
    /// Command for cancelling a sale.
    /// </summary>
    public record CancelSaleCommand : IRequest<CancelSaleResult>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale to be cancelled.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Initializes a new instance of CancelSaleCommand
        /// </summary>
        /// <param name="id">The ID of the sale to cancel</param>
        public CancelSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}
