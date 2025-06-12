using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Command for creating a new sale.
    /// </summary>
    /// <remarks>
    /// This command is used to capture the required data for creating a sale, 
    /// including customer information, branch, items, and status. 
    /// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
    /// that returns a <see cref="CreateSaleResult"/>.
    /// 
    /// The data provided in this command is validated using the 
    /// <see cref="CreateSaleCommandValidator"/> which extends 
    /// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
    /// populated and follow the required rules.
    /// </remarks>
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        /// <summary>
        /// Gets or sets the customer information for the sale (name, email, phone).
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets the branch or location where the sale was made.
        /// </summary>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of items in the sale.
        /// </summary>
        public List<CreateSaleItemCommand> Items { get; set; } = [];

        /// <summary>
        /// Gets or sets the sale's status.
        /// </summary>
        public SaleStatus Status { get; set; } = SaleStatus.Pending;

        /// <summary>
        /// Validates the sale creation command.
        /// </summary>
        public ValidationResultDetail Validate()
        {
            var validator = new CreateSaleCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
