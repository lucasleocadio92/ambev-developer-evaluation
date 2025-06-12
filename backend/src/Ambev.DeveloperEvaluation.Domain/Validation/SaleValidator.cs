using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleForEach(sale => sale.Items)
                .ChildRules(items =>
                {
                    items.RuleFor(item => item.ProductId)
                         .NotEqual(Guid.Empty)
                         .WithMessage("ProductId must be a valid non-empty GUID.");

                    items.RuleFor(item => item.Quantity)
                         .GreaterThan(0)
                         .WithMessage("Quantity must be greater than 0.");

                    items.RuleFor(item => item.UnitPrice)
                         .GreaterThan(0)
                         .WithMessage("Unit price must be greater than 0.");

                    items.RuleFor(item => item.TotalAmount)
                         .GreaterThanOrEqualTo(0)
                         .WithMessage("Total amount must be zero or greater.");
                });

            RuleFor(sale => sale.TotalAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Total sale amount must be zero or greater.");

            RuleFor(sale => sale.Customer)
                .NotNull()
                .WithMessage("Customer is required.")
                .ChildRules(customer =>
                {
                    customer.RuleFor(c => c.Name)
                            .NotEmpty().WithMessage("Customer name is required.")
                            .MaximumLength(100).WithMessage("Customer name cannot be longer than 100 characters.");

                    customer.RuleFor(c => c.Email)
                            .NotEmpty().WithMessage("Customer email is required.")
                            .MaximumLength(100).WithMessage("Customer email cannot be longer than 100 characters.")
                            .EmailAddress().WithMessage("Customer email must be a valid email address.");

                    customer.RuleFor(c => c.Phone)
                            .NotEmpty().WithMessage("Customer phone is required.")
                            .MaximumLength(20).WithMessage("Customer phone cannot be longer than 20 characters.");
                });

            RuleFor(sale => sale.Branch)
                .NotEmpty()
                .WithMessage("Branch is required.")
                .MaximumLength(50)
                .WithMessage("Branch name cannot be longer than 50 characters.");

            RuleFor(sale => sale.SaleDate)
                .NotNull()
                .WithMessage("Sale date is required.")
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Sale date cannot be in the future.");
        }
    }
}
