using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation
{
    /// <summary>
    /// Contains unit tests for the SaleValidator class.
    /// Tests cover validation of all sale properties including sale items, customer, and sale details.
    /// </summary>
    public class SaleValidatorTests
    {
        private readonly SaleValidator _validator;

        public SaleValidatorTests()
        {
            _validator = new SaleValidator();
        }

        /// <summary>
        /// Tests that validation passes when all sale properties are valid.
        /// This test verifies that a sale with valid:
        /// - Items (ProductId, Quantity, UnitPrice, TotalAmount)
        /// - Customer (Name, Email, Phone)
        /// - Branch (Branch name)
        /// - SaleDate (Valid date)
        /// passes all validation rules without any errors.
        /// </summary>
        [Fact(DisplayName = "Valid sale should pass all validation rules")]
        public void Given_ValidSale_When_Validated_Then_ShouldNotHaveErrors()
        {
            // Arrange
            var sale = new Sale(new Customer("Customer", "customer@example.com", "123456789", "68249921003"))
            {
                SaleDate = DateTime.UtcNow,
                Branch = "Main Branch",
                Items =
                [
                    new SaleItem { ProductId = Guid.NewGuid(), Quantity = 5, UnitPrice = 100m, TotalAmount = 500m }
                ]
            };

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests that validation fails for invalid ProductId in SaleItem.
        /// This test verifies that when the ProductId is empty (Guid.Empty),
        /// the validation should fail with the correct error message.
        /// </summary>
        [Fact(DisplayName = "Invalid ProductId should fail validation")]
        public void Given_SaleWithInvalidProductId_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = new Sale(new Customer("Customer", "customer@example.com", "123456789", "68249921003"))
            {
                SaleDate = DateTime.UtcNow,
                Branch = "Main Branch",
                Items =
                [
                    new SaleItem { ProductId = Guid.Empty, Quantity = 5, UnitPrice = 100m, TotalAmount = 500m }
                ]
            };

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Items[0].ProductId);
        }

        /// <summary>
        /// Tests that validation fails for invalid Quantity in SaleItem.
        /// This test verifies that when Quantity is 0 or less, the validation should fail.
        /// </summary>
        [Fact(DisplayName = "Invalid Quantity should fail validation")]
        public void Given_SaleWithInvalidQuantity_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = new Sale(new Customer("Customer", "customer@example.com", "123456789", "68249921003"))
            {
                SaleDate = DateTime.UtcNow,
                Branch = "Main Branch",
                Items =
                [
                    new SaleItem { ProductId = Guid.NewGuid(), Quantity = 0, UnitPrice = 100m, TotalAmount = 0m }
                ]
            };

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Items[0].Quantity);
        }

        /// <summary>
        /// Tests that validation fails for invalid UnitPrice in SaleItem.
        /// This test verifies that when UnitPrice is 0 or less, the validation should fail.
        /// </summary>
        [Fact(DisplayName = "Invalid UnitPrice should fail validation")]
        public void Given_SaleWithInvalidUnitPrice_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = new Sale(new Customer("Customer", "customer@example.com", "123456789", "68249921003"))
            {
                SaleDate = DateTime.UtcNow,
                Branch = "Main Branch",
                Items =
                [
                    new SaleItem { ProductId = Guid.NewGuid(), Quantity = 5, UnitPrice = 0m, TotalAmount = 0m }
                ]
            };

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Items[0].UnitPrice);
        }

        /// <summary>
        /// Tests that validation fails for invalid TotalAmount in SaleItem.
        /// This test verifies that when TotalAmount is less than 0, the validation should fail.
        /// </summary>
        [Fact(DisplayName = "Invalid TotalAmount should fail validation")]
        public void Given_SaleWithInvalidTotalAmount_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = new Sale(new Customer("Customer", "customer@example.com", "123456789", "68249921003"))
            {
                SaleDate = DateTime.UtcNow,
                Branch = "Main Branch",
                Items =
                [
                    new SaleItem { ProductId = Guid.NewGuid(), Quantity = 5, UnitPrice = 100m, TotalAmount = -10m }
                ]
            };

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Items[0].TotalAmount);
        }

        /// <summary>
        /// Tests that validation fails for invalid Customer Email format.
        /// This test verifies that when Customer Email is invalid, the validation fails.
        /// </summary>
        [Fact(DisplayName = "Invalid Customer Email should fail validation")]
        public void Given_SaleWithInvalidCustomerEmail_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = new Sale(new Customer("Customer", "invalid-email", "123456789", "68249921003"))
            {
                SaleDate = DateTime.UtcNow,
                Branch = "Main Branch",
                Items =
                [
                    new SaleItem { ProductId = Guid.NewGuid(), Quantity = 5, UnitPrice = 100m, TotalAmount = 500m }
                ]
            };

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Customer.Email);
        }

        /// <summary>
        /// Tests that validation fails for missing SaleDate.
        /// This test verifies that SaleDate cannot be null or in the future.
        /// </summary>
        [Fact(DisplayName = "SaleDate in the future should fail validation")]
        public void Given_SaleWithFutureSaleDate_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = new Sale(new Customer("Customer", "customer@example.com", "123456789", "68249921003"))
            {
                SaleDate = DateTime.UtcNow.AddDays(1), // Future date
                Branch = "Main Branch",
                Items =
                [
                    new SaleItem { ProductId = Guid.NewGuid(), Quantity = 5, UnitPrice = 100m, TotalAmount = 500m }
                ]
            };

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SaleDate);
        }

        /// <summary>
        /// Tests that validation fails for empty Branch name.
        /// This test verifies that Branch name must be filled and not be empty.
        /// </summary>
        [Fact(DisplayName = "Empty Branch name should fail validation")]
        public void Given_SaleWithEmptyBranch_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = new Sale(new Customer("Customer", "customer@example.com", "123456789", "68249921003"))
            {
                SaleDate = DateTime.UtcNow,
                Branch = "", // Invalid branch name
                Items =
                [
                    new SaleItem { ProductId = Guid.NewGuid(), Quantity = 5, UnitPrice = 100m, TotalAmount = 500m }
                ]
            };

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Branch);
        }
    }
}
