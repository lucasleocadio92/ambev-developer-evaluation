using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Sales
{
    /// <summary>
    /// Provides methods for generating test data for the <see cref="UpdateSaleHandler"/>.
    /// This class generates both valid and invalid sale update commands for unit testing.
    /// </summary>
    public static class UpdateSaleHandlerTestData
    {
        /// <summary>
        /// Configures the Faker to generate valid <see cref="UpdateSaleCommand"/> objects.
        /// The generated commands will have:
        /// - Id: A randomly generated GUID.
        /// - Status: A random sale status (Pending, Completed, or Canceled).
        /// - TotalPrice: A randomly generated total price between 100 and 1000.
        /// - Items: A list of randomly generated sale items with product ID, quantity, unit price, and discount.
        /// </summary>
        private static readonly Faker<UpdateSaleCommand> faker = new Faker<UpdateSaleCommand>()
            .RuleFor(c => c.Id, f => f.Random.Guid())
            .RuleFor(c => c.Status, f => f.PickRandom(SaleStatus.Pending, SaleStatus.Completed, SaleStatus.Canceled))
            .RuleFor(c => c.Items, f => f.Make(3, () => new UpdateSaleItemCommand
            {
                ProductId = f.Random.Guid(),
                Quantity = f.Random.Number(1, 15),
                UnitPrice = f.Random.Decimal(10, 100),
                Discount = f.Random.Decimal(0, 0.2m)
            }));

        /// <summary>
        /// Generates a valid <see cref="UpdateSaleCommand"/> with random data for testing.
        /// The generated command is expected to pass validation and be valid for processing.
        /// </summary>
        /// <returns>A valid <see cref="UpdateSaleCommand"/> with randomized fields.</returns>
        public static UpdateSaleCommand GenerateValidCommand()
        {
            return faker.Generate();
        }

        /// <summary>
        /// Generates an invalid <see cref="UpdateSaleCommand"/>. 
        /// The generated command is missing required fields and will fail validation.
        /// This is useful for testing failure cases.
        /// </summary>
        /// <returns>An invalid <see cref="UpdateSaleCommand"/> with missing fields.</returns>
        public static UpdateSaleCommand GenerateInvalidCommand()
        {
            return new UpdateSaleCommand(); // This will create an invalid command with missing values
        }
    }
}
