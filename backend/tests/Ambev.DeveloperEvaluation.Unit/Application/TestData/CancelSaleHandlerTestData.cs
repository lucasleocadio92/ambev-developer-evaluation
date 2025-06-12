using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Sales
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library.
    /// This class centralizes all test data generation for CancelSaleHandler tests
    /// to ensure consistency across test cases and provide both valid and invalid data scenarios.
    /// </summary>
    public static class CancelSaleHandlerTestData
    {
        /// <summary>
        /// Configures the Faker to generate valid CancelSaleCommand entities.
        /// </summary>
        private static readonly Faker<CancelSaleCommand> cancelSaleCommandFaker = new Faker<CancelSaleCommand>()
            .RuleFor(c => c.Id, f => f.Random.Guid());

        /// <summary>
        /// Generates a valid CancelSaleCommand entity with a valid sale ID.
        /// </summary>
        /// <returns>A valid CancelSaleCommand entity.</returns>
        public static CancelSaleCommand GenerateValidCommand()
        {
            return cancelSaleCommandFaker.Generate();
        }

        /// <summary>
        /// Generates an invalid CancelSaleCommand entity where the sale ID is null.
        /// </summary>
        /// <returns>An invalid CancelSaleCommand entity.</returns>
        public static CancelSaleCommand GenerateInvalidCommand()
        {
            return cancelSaleCommandFaker
                .RuleFor(c => c.Id, Guid.Empty)  // Invalid ID
                .Generate();
        }
    }
}
