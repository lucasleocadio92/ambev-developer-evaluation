using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for <see cref="CreateSaleCommand"/> that defines validation rules for sale creation.
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Customer: Must not be null, as it is a required field for sale creation.
    /// - Branch: Required, must not be empty.
    /// - Status: Must be a valid <see cref="SaleStatus"/> value (e.g., Pending, Completed, etc.).
    /// - Sale Items:
    ///   - ProductId: Must not be empty.
    ///   - Quantity: Must be greater than 0.
    ///   - UnitPrice: Must be greater than 0.
    ///   - Discount: Cannot be negative.
    /// </remarks>
    public CreateSaleCommandValidator()
    {
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
                .NotEmpty().WithMessage("Product ID must not be empty.");

                item.RuleFor(i => i.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

                item.RuleFor(i => i.UnitPrice)
                    .GreaterThan(0).WithMessage("Unit price must be greater than 0.");

                item.RuleFor(i => i.Discount)
                    .GreaterThanOrEqualTo(0).WithMessage("Discount cannot be negative.");
            });
    }
}