using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Specifications
{
    public class SaleIsWithinDiscountRulesSpecification : ISpecification<Sale>
    {
        public bool IsSatisfiedBy(Sale sale)
        {
            foreach (var item in sale.Items)
            {
                if (item.Quantity >= 4 && item.Quantity < 10)
                {
                    if (item.Discount != 0.10m) return false;
                }
                else if (item.Quantity >= 10 && item.Quantity <= 20)
                {
                    if (item.Discount != 0.20m) return false;
                }
                else if (item.Quantity < 4)
                {
                    if (item.Discount != 0) return false;
                }
            }
            return true;
        }
    }
}