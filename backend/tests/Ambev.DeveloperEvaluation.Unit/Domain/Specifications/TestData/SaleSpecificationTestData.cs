using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Bogus;
using Bogus.Extensions.Brazil;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications.TestData
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library.
    /// This class centralizes all test data generation for Sale Specifications tests
    /// to ensure consistency across test cases.
    /// </summary>
    public static class SaleSpecificationTestData
    {
        /// <summary>
        /// Configures the Faker to generate valid Sale entities.
        /// The generated sales will have valid:
        /// - Customer
        /// - SaleDate (valid)
        /// - Branch (valid)
        /// - Status (Pending, InProgress, or Completed)
        /// - Items (with Quantity, UnitPrice, and Discount applied)
        /// </summary>
        private static readonly Faker<Sale> saleFaker = new Faker<Sale>()
            .CustomInstantiator(f => new Sale(CustomerTestData.GenerateValidCustomer())
            {
                SaleDate = f.Date.Past(),
                Branch = f.Company.CompanyName(),
                Status = f.PickRandom<SaleStatus>(),
                Items = new List<SaleItem>
                {
                    new SaleItem
                    {
                        ProductId = f.Random.Guid(),
                        Quantity = f.Random.Int(1, 20),
                        UnitPrice = f.Random.Decimal(1, 100),
                        Discount = f.Random.Decimal(0, 0.2m)
                    }
                }
            });

        /// <summary>
        /// Generates a valid Sale entity with the specified status.
        /// </summary>
        /// <param name="status">The SaleStatus to set for the generated sale.</param>
        /// <returns>A valid Sale entity with randomly generated data and specified status.</returns>
        public static Sale GenerateSale(SaleStatus status)
        {
            var sale = saleFaker.Generate();
            sale.Status = status;
            return sale;
        }

        /// <summary>
        /// Generates a Sale with an invalid quantity.
        /// This is used to test the SaleHasValidQuantitySpecification.
        /// </summary>
        /// <returns>A Sale entity with invalid quantity values (greater than 20).</returns>
        public static Sale GenerateSaleWithInvalidQuantity()
        {
            var sale = saleFaker.Generate();
            // Set one item with quantity greater than 20
            sale.Items.First().Quantity = 25;
            return sale;
        }

        /// <summary>
        /// Gera uma venda válida com descontos aplicados corretamente.
        /// </summary>
        public static Sale GenerateSaleWithValidDiscounts()
        {
            var sale = saleFaker.Generate();
            sale.Items = new List<SaleItem>
            {
                new SaleItem
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 5, // Entre 4 e 10
                    UnitPrice = 50m,
                    Discount = 0.10m, // Desconto de 10% para quantidade entre 4 e 10
                    TotalAmount = 5 * 50m * 0.90m // Total com desconto
                },
                new SaleItem
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 15, // Entre 10 e 20
                    UnitPrice = 60m,
                    Discount = 0.20m, // Desconto de 20% para quantidade entre 10 e 20
                    TotalAmount = 15 * 60m * 0.80m // Total com desconto
                }
            };
            return sale;
        }

        /// <summary>
        /// Generates a Sale where the discount rules are violated.
        /// This is used to test the SaleIsWithinDiscountRulesSpecification.
        /// </summary>
        /// <returns>A Sale entity with violated discount rules (wrong discount value).</returns>
        public static Sale GenerateSaleWithInvalidDiscount()
        {
            var sale = saleFaker.Generate();
            var item = sale.Items.First();
            // Apply a discount rule violation (wrong discount for quantity)
            if (item.Quantity >= 4 && item.Quantity < 10)
            {
                item.Discount = 0.25m; // Invalid discount for this quantity
            }
            else if (item.Quantity >= 10 && item.Quantity <= 20)
            {
                item.Discount = 0.05m; // Invalid discount for this quantity
            }
            return sale;
        }

        /// <summary>
        /// Generates a Sale that is cancelled.
        /// This is used to test the SaleNotCancelledSpecification.
        /// </summary>
        /// <returns>A Sale entity with status Canceled.</returns>
        public static Sale GenerateSaleWithCancelledStatus()
        {
            var sale = saleFaker.Generate();
            sale.Status = SaleStatus.Canceled;
            return sale;
        }
    }
}
