using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Validator for <see cref="UpdateSaleCommand"/> that defines validation rules for sale update.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must not be empty.
    /// - Customer: Must not be null.
    /// - Branch: Must not be empty.
    /// - Status: Must be a valid <see cref="SaleStatus"/>.
    /// - Sale Items:
    ///   - ProductId: Must not be empty GUID.
    ///   - Quantity: Must be greater than 0.
    ///   - UnitPrice: Must be greater than 0.
    ///   - Discount: Must be zero or positive.
    /// </remarks>
    public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSaleCommandValidator"/> class.
        /// </summary>
        public UpdateSaleCommandValidator()
        {
            RuleFor(sale => sale.Id)
                .NotEmpty()
                .WithMessage("ID must not be empty.");

            RuleFor(sale => sale.Customer)
                .NotNull().WithMessage("Customer information is required.");

            RuleFor(sale => sale.Branch)
                .NotEmpty().WithMessage("Branch is required.");

            RuleFor(sale => sale.Status)
                .IsInEnum().WithMessage("Invalid sale status.");

            RuleForEach(sale => sale.Items)
                .NotNull().WithMessage("Each sale item must be provided.")
                .ChildRules(item =>
                {
                    item.RuleFor(i => i.ProductId)
                        .NotEqual(Guid.Empty).WithMessage("Product ID must not be empty.");

                    item.RuleFor(i => i.Quantity)
                        .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

                    item.RuleFor(i => i.UnitPrice)
                        .GreaterThan(0).WithMessage("Unit price must be greater than 0.");

                    item.RuleFor(i => i.Discount)
                        .GreaterThanOrEqualTo(0).WithMessage("Discount cannot be negative.");
                });
        }
    }
}
