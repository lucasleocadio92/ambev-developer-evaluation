using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;
using Bogus.Extensions.Brazil;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Sales
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library.
    /// This class centralizes all test data generation for CreateSaleHandler tests
    /// to ensure consistency across test cases and provide both valid and invalid data scenarios.
    /// </summary>
    public static class CreateSaleHandlerTestData
    {
        /// <summary>
        /// Configures the Faker to generate valid CreateSaleCommand entities.
        /// The generated sales will have valid:
        /// - Customer (using random customer names)
        /// - Branch (using random branch names)
        /// - Status (Active or Suspended)
        /// - Items (a list of SaleItems with valid quantities and discounts)
        /// </summary>
        private static readonly Faker<CreateSaleCommand> createSaleCommandFaker = new Faker<CreateSaleCommand>()
            .RuleFor(s => s.Customer, f => new Customer(f.Person.FullName, f.Person.Email, f.Person.Phone, f.Person.Cpf()))
            .RuleFor(s => s.Branch, f => f.Address.City())
            .RuleFor(s => s.Status, f => f.PickRandom(SaleStatus.Pending, SaleStatus.Completed, SaleStatus.Canceled))
            .RuleFor(s => s.Items, f => f.Make(3, () => new CreateSaleItemCommand(
                f.Random.Guid(),
                f.Random.Number(1, 15),
                f.Random.Decimal(10, 100),
                f.Random.Decimal(0, 0.2m))
            ));

        /// <summary>
        /// Generates a valid CreateSaleCommand entity with randomized data.
        /// </summary>
        /// <returns>A valid CreateSaleCommand entity with randomized data.</returns>
        public static CreateSaleCommand GenerateValidCommand()
        {
            return createSaleCommandFaker.Generate();
        }

        /// <summary>
        /// Generates an invalid CreateSaleCommand entity where items have invalid quantities.
        /// </summary>
        /// <returns>An invalid CreateSaleCommand entity.</returns>
        public static CreateSaleCommand GenerateInvalidCommand()
        {
            return createSaleCommandFaker
                .RuleFor(s => s.Items, f => f.Make(3, () => new CreateSaleItemCommand(
                    f.Random.Guid(),
                    f.Random.Number(21, 50),  // Invalid quantity greater than 20
                    f.Random.Decimal(10, 100),
                    f.Random.Decimal(0, 0.2m))
                ))
                .Generate();
        }
    }
}
