using Ambev.DeveloperEvaluation.Application.Sales.GetAllSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Sales
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library.
    /// This class centralizes all test data generation for GetAllSaleHandler tests
    /// to ensure consistency across test cases and provide both valid and invalid data scenarios.
    /// </summary>
    public static class GetAllSaleHandlerTestData
    {
        /// <summary>
        /// Configures the Faker to generate valid GetAllSaleCommand entities.
        /// </summary>
        private static readonly Faker<GetAllSaleCommand> getAllSaleCommandFaker = new Faker<GetAllSaleCommand>()
            .RuleFor(c => c.PageNumber, f => f.Random.Number(1, 10))
            .RuleFor(c => c.PageSize, f => f.Random.Number(10, 100))
            .RuleFor(c => c.Order, f => f.PickRandom("Date", "Amount"));

        /// <summary>
        /// Generates a valid GetAllSaleCommand entity.
        /// </summary>
        /// <returns>A valid GetAllSaleCommand entity.</returns>
        public static List<GetAllSaleCommand> GenerateValidCommand(int count = 1)
        {
            return getAllSaleCommandFaker.Generate(count);
        }

        /// <summary>
        /// Generates an invalid GetAllSaleCommand entity where the page number is invalid.
        /// </summary>
        /// <returns>An invalid GetAllSaleCommand entity.</returns>
        public static List<GetAllSaleCommand> GenerateInvalidCommand(int count = 1)
        {
            return getAllSaleCommandFaker
                .RuleFor(c => c.PageNumber, 0)  // Invalid page number
                .Generate(count);
        }
    }
}
