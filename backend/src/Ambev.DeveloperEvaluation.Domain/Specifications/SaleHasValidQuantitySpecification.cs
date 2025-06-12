using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Specifications
{
    public class SaleHasValidQuantitySpecification : ISpecification<Sale>
    {
        public bool IsSatisfiedBy(Sale sale)
        {
            foreach (var item in sale.Items)
            {
                if (item.Quantity > 20)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
