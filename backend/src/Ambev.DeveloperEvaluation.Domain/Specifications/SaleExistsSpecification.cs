using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Specifications
{
    public class SaleExistsSpecification : ISpecification<Sale>
    {
        public bool IsSatisfiedBy(Sale sale)
        {
            return sale != null;
        }
    }
}