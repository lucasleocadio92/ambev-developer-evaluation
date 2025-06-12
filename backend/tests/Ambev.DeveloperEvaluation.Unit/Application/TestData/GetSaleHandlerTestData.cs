using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Sales
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library.
    /// This class centralizes all test data generation for GetSaleHandler tests
    /// to ensure consistency across test cases and provide both valid and invalid data scenarios.
    /// </summary>
    public static class GetSaleHandlerTestData
    {
        /// <summary>
        /// Configures the Faker to generate valid GetSaleCommand entities.
        /// </summary>
        private static readonly Faker<GetSaleCommand> getSaleCommandFaker = new Faker<GetSaleCommand>()
            .RuleFor(c => c.Id, f => f.Random.Guid());

        /// <summary>
        /// Generates a valid GetSaleCommand entity.
        /// </summary>
        /// <returns>A valid GetSaleCommand entity.</returns>
        public static GetSaleCommand GenerateValidCommand()
        {
            return getSaleCommandFaker.Generate();
        }

        /// <summary>
        /// Generates an invalid GetSaleCommand entity where the sale ID is invalid.
        /// </summary>
        /// <returns>An invalid GetSaleCommand entity.</returns>
        public static GetSaleCommand GenerateInvalidCommand()
        {
            return getSaleCommandFaker
                .RuleFor(c => c.Id, Guid.Empty)  // Invalid ID
                .Generate();
        }
    }
}
