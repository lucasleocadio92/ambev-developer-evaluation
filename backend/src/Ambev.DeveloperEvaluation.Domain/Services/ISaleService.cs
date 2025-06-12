using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface ISaleService
    {
        Task ValidateBeforeUpdateAsync(Sale sale);
    }
}
