using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// Validator for UpdateSaleRequest that defines validation rules for updating an existing sale.
    /// </summary>
    public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
    {
        /// <summary>
        /// Initializes a new instance of the UpdateSaleRequestValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Id: Cannot be an empty GUID
        /// - Customer: Cannot be null
        /// - Status: Cannot be Invalid
        /// - Branch: Required, length between 2 and 100 characters
        /// - Items: Must contain at least one item
        /// - Sale item fields: ProductId not be empty, Quantity > 0, UnitPrice ≥ 0, Discount ≥ 0
        /// </remarks>
        public UpdateSaleRequestValidator()
        {
            RuleFor(sale => sale.Id)
                .NotEqual(Guid.Empty).WithMessage("Id must be a valid GUID.");

            RuleFor(sale => sale.Customer)
                .NotNull().WithMessage("Customer must be provided.");

            RuleFor(sale => sale.Status)
           .IsInEnum().WithMessage("Invalid sale status.");

            RuleFor(sale => sale.Branch)
                .NotEmpty().WithMessage("Branch is required.")
                .Length(2, 100).WithMessage("Branch must be between 2 and 100 characters.");

            RuleFor(sale => sale.Items)
                .NotEmpty().WithMessage("At least one sale item is required.");

            RuleForEach(sale => sale.Items).ChildRules(items =>
            {
                items.RuleFor(i => i.ProductId)
                    .NotEmpty().WithMessage("ProductId must not be empty.");

                items.RuleFor(i => i.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

                items.RuleFor(i => i.UnitPrice)
                    .GreaterThanOrEqualTo(0).WithMessage("Unit price must be 0 or greater.");

                items.RuleFor(i => i.Discount)
                    .GreaterThanOrEqualTo(0).WithMessage("Discount must be 0 or greater.");
            });
        }
    }
}
