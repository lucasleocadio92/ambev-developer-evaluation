using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    /// <summary>
    /// Contains unit tests for the Sale entity class.
    /// Tests cover status changes, total calculation, and validation scenarios.
    /// </summary>
    public class SaleTests
    {
        /// <summary>
        /// Tests that the total amount is correctly calculated based on the sale items.
        /// </summary>
        [Fact(DisplayName = "Sale total amount should be correctly calculated")]
        public void Given_SaleWithItems_When_CalculateTotalAmount_Then_TotalAmountShouldBeCorrect()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            var initialTotal = sale.TotalAmount;

            // Act
            sale.CalculateTotalAmount();

            // Assert
            Assert.Equal(initialTotal, sale.TotalAmount);
        }

        /// <summary>
        /// Tests that a sale can be marked as completed.
        /// </summary>
        [Fact(DisplayName = "Sale status should change to Completed when completed")]
        public void Given_PendingSale_When_Completed_Then_StatusShouldBeCompleted()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.Status = SaleStatus.Pending;

            // Act
            sale.Complete();

            // Assert
            Assert.Equal(SaleStatus.Completed, sale.Status);
        }

        /// <summary>
        /// Tests that an exception is thrown when trying to complete a non-pending sale.
        /// </summary>
        [Fact(DisplayName = "Sale should throw an exception when completing a non-pending sale")]
        public void Given_CompletedSale_When_Completed_Then_ShouldThrowException()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.Status = SaleStatus.Completed;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => sale.Complete());
        }

        /// <summary>
        /// Tests that a sale can be cancelled if it is not completed.
        /// </summary>
        [Fact(DisplayName = "Sale status should change to Cancelled when cancelled")]
        public void Given_PendingSale_When_Cancelled_Then_StatusShouldBeCancelled()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.Status = SaleStatus.Pending;

            // Act
            sale.Cancel();

            // Assert
            Assert.Equal(SaleStatus.Canceled, sale.Status);
        }

        /// <summary>
        /// Tests that an exception is thrown when trying to cancel a completed sale.
        /// </summary>
        [Fact(DisplayName = "Sale should throw an exception when cancelling a completed sale")]
        public void Given_CompletedSale_When_Cancelled_Then_ShouldThrowException()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.Status = SaleStatus.Completed;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => sale.Cancel());
        }

        /// <summary>
        /// Tests that validation passes for a valid sale entity.
        /// </summary>
        [Fact(DisplayName = "Validation should pass for valid sale data")]
        public void Given_ValidSaleData_When_Validated_Then_ShouldReturnValid()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            // Act
            var result = sale.Validate();

            // Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        /// <summary>
        /// Tests that validation fails for an invalid sale entity.
        /// </summary>
        [Fact(DisplayName = "Validation should fail for invalid sale data")]
        public void Given_InvalidSaleData_When_Validated_Then_ShouldReturnInvalid()
        {
            // Arrange
            var sale = new Sale(null) // Invalid: Customer is required
            {
                SaleDate = DateTime.UtcNow.AddDays(1), // Invalid: Sale date cannot be in the future
                Status = SaleStatus.Pending
            };

            // Act
            var result = sale.Validate();

            // Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }
    }

    /// <summary>
    /// Contains unit tests for the SaleItem entity class.
    /// Tests cover discount application and validation scenarios.
    /// </summary>
    public class SaleItemTests
    {
        /// <summary>
        /// Tests that the discount is correctly applied based on quantity.
        /// </summary>
        [Fact(DisplayName = "SaleItem discount should be applied correctly based on quantity")]
        public void Given_SaleItem_When_QuantityIsGreaterThanThreshold_Then_DiscountShouldBeApplied()
        {
            // Arrange
            var saleItem = SaleTestData.GenerateValidSaleItem();
            saleItem.Quantity = 15;  // Will apply a 20% discount

            // Act
            saleItem.ApplyDiscount();

            // Assert
            Assert.True(saleItem.Discount > 0);
            Assert.Equal(saleItem.TotalAmount, (saleItem.Quantity * saleItem.UnitPrice) - saleItem.Discount);
        }

        /// <summary>
        /// Tests that the total amount is correctly calculated for a SaleItem.
        /// </summary>
        [Fact(DisplayName = "SaleItem total amount should be correctly calculated")]
        public void Given_SaleItem_When_ApplyDiscount_Then_TotalAmountShouldBeCorrect()
        {
            // Arrange
            var saleItem = SaleTestData.GenerateValidSaleItem();

            // Act
            saleItem.ApplyDiscount();

            // Assert
            var expectedTotal = (saleItem.Quantity * saleItem.UnitPrice) - saleItem.Discount;
            Assert.Equal(expectedTotal, saleItem.TotalAmount);
        }
    }
}
