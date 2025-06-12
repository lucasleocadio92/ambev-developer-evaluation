using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library.
    /// This class centralizes all test data generation to ensure consistency
    /// across test cases and provide both valid and invalid data scenarios.
    /// </summary>
    public static class SaleTestData
    {
        private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
            .RuleFor(s => s.Id, f => f.Random.Guid())
            .RuleFor(s => s.SaleDate, f => f.Date.Past(1))
            .RuleFor(s => s.Customer, f => CustomerTestData.GenerateValidCustomer())
            .RuleFor(s => s.Branch, f => f.Company.CompanyName())
            .RuleFor(s => s.Status, f => f.PickRandom<SaleStatus>())
            .RuleFor(s => s.Items, f => GenerateValidSaleItems(2))
            .RuleFor(s => s.TotalAmount, f => f.Finance.Amount(100, 1000));

        private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
            .RuleFor(si => si.ProductId, f => f.Random.Guid())
            .RuleFor(si => si.Quantity, f => f.Random.Int(1, 20))
            .RuleFor(si => si.UnitPrice, f => f.Finance.Amount(10, 500))
            .RuleFor(si => si.Discount, f => f.Finance.Amount(0, 50))
            .RuleFor(si => si.TotalAmount, (f, si) => (si.Quantity * si.UnitPrice) - si.Discount);

        /// <summary>
        /// Generates a valid Sale entity with randomized data.
        /// </summary>
        /// <returns>A valid Sale entity with randomly generated data.</returns>
        public static Sale GenerateValidSale()
        {
            var sale = SaleFaker.Generate();
            sale.CalculateTotalAmount();  // Recalculate total amount after item generation
            return sale;
        }

        /// <summary>
        /// Generates a valid SaleItem entity with randomized data.
        /// </summary>
        /// <returns>A valid SaleItem entity with randomly generated data.</returns>
        public static SaleItem GenerateValidSaleItem()
        {
            return SaleItemFaker.Generate();
        }

        /// <summary>
        /// Generates a list of valid SaleItems.
        /// </summary>
        /// <param name="itemCount">The number of SaleItems to generate</param>
        /// <returns>A list of valid SaleItems</returns>
        public static List<SaleItem> GenerateValidSaleItems(int itemCount = 1)
        {
            return SaleItemFaker.Generate(itemCount);
        }

        /// <summary>
        /// Generates an invalid Sale entity (for testing negative scenarios).
        /// </summary>
        /// <returns>An invalid Sale entity</returns>
        public static Sale GenerateInvalidSale()
        {
            var sale = SaleFaker.Generate();
            sale.SaleDate = DateTime.UtcNow.AddDays(1);  // Invalid: Sale date can't be in the future
            sale.Status = SaleStatus.Canceled;  // Invalid: Sale should not be canceled by default
            return sale;
        }

        /// <summary>
        /// Generates an invalid SaleItem entity (for testing negative scenarios).
        /// </summary>
        /// <returns>An invalid SaleItem entity</returns>
        public static SaleItem GenerateInvalidSaleItem()
        {
            var saleItem = SaleItemFaker.Generate();
            saleItem.Quantity = 0;  // Invalid: Quantity can't be zero or negative
            return saleItem;
        }
    }

    // Assuming you have a CustomerTestData for generating Customer entities
    public static class CustomerTestData
    {
        private static readonly Faker<Customer> CustomerFaker = new Faker<Customer>()
            .RuleFor(c => c.Name, f => f.Name.FullName())
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}");

        public static Customer GenerateValidCustomer()
        {
            return CustomerFaker.Generate();
        }
    }
}
