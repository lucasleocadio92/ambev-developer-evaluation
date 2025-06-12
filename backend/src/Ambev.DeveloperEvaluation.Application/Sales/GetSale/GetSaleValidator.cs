using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Validator for <see cref="GetSaleCommand"/> that defines rules for validating a sale retrieval command.
    /// </summary>
    public class GetSaleCommandValidator : AbstractValidator<GetSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSaleCommandValidator"/> class with validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Id: Must not be empty.
        /// </remarks>
        public GetSaleCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty()
                .WithMessage("ID must not be empty.");
        }
    }
}
