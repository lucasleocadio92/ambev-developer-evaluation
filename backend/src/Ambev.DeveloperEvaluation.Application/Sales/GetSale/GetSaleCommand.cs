using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Command for retrieving a sale by their ID
    /// </summary>
    /// <remarks>
    public class GetSaleCommand : IRequest<GetSaleResult>
    {
        /// <summary>
        /// The unique identifier of the sale to retrieve
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Initializes a new instance of GetSaleCommand
        /// </summary>
        /// <param name="id">The ID of the user to retrieve</param>
        public GetSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}
