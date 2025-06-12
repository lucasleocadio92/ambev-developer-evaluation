using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISpecification<Sale> _saleNotCancelledSpec;
        private readonly ISpecification<Sale> _saleHasValidQuantitySpec;
        private readonly ISpecification<Sale> _saleIsWithinDiscountRulesSpec;

        public SaleService()
        {
            _saleNotCancelledSpec = new SaleNotCancelledSpecification();
            _saleHasValidQuantitySpec = new SaleHasValidQuantitySpecification();
            _saleIsWithinDiscountRulesSpec = new SaleIsWithinDiscountRulesSpecification();
        }

        public Task ValidateBeforeUpdateAsync(Sale sale)
        {
            if (!_saleNotCancelledSpec.IsSatisfiedBy(sale))
                throw new InvalidOperationException("Cannot update a cancelled sale.");

            if (!_saleHasValidQuantitySpec.IsSatisfiedBy(sale))
                throw new InvalidOperationException("Invalid item quantity.");

            if (!_saleIsWithinDiscountRulesSpec.IsSatisfiedBy(sale))
                throw new InvalidOperationException("Discount rules not satisfied.");

            var validation = sale.Validate();
            if (!validation.IsValid)
                throw new ValidationException(string.Join(";", validation.Errors.Select(e => e.Error)));

            return Task.CompletedTask;
        }
    }
}
