using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Command for updating an existing sale.
    /// </summary>
    /// <remarks>
    /// This command is used to capture the updated data for a sale,
    /// including customer information, branch, status, and items.
    /// It implements <see cref="IRequest{TResponse}"/> to initiate
    /// the request that returns an <see cref="UpdateSaleResult"/>.
    /// 
    /// The data provided in this command is validated using the
    /// <see cref="UpdateSaleCommandValidator"/> which extends
    /// <see cref="AbstractValidator{T}"/> to ensure that the fields
    /// are correctly populated and follow the required rules.
    /// </remarks>
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale to update.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the updated customer information (name, email, phone).
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets the updated branch or location where the sale was made.
        /// </summary>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the updated sale status.
        /// </summary>
        public SaleStatus Status { get; set; } = SaleStatus.Pending;

        /// <summary>
        /// Gets or sets the updated list of sale items.
        /// </summary>
        public List<UpdateSaleItemCommand> Items { get; set; } = [];

        /// <summary>
        /// Validates the update sale command.
        /// </summary>
        /// <returns>A <see cref="ValidationResultDetail"/> with validation errors, if any.</returns>
        public ValidationResultDetail Validate()
        {
            var validator = new UpdateSaleCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
