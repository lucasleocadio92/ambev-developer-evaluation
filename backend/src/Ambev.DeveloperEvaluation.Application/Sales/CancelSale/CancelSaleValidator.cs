using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    /// <summary>
    /// Validator for <see cref="CancelSaleCommand"/> that defines validation rules for sale cancellation.
    /// </summary>
    public class CancelSaleCommandValidator : AbstractValidator<CancelSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CancelSaleCommandValidator"/> with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Id: Must not be empty.
        /// </remarks>
        public CancelSaleCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty()
                .WithMessage("ID must not be empty.");
        }
    }
}
