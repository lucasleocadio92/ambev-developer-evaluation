using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Unit.Domain.Specifications.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications
{
    public class SaleSpecificationTests
    {
        /// <summary>
        /// Testa se a SaleExistsSpecification passa quando a venda não é nula.
        /// </summary>
        [Fact(DisplayName = "SaleExistsSpecification deve passar quando a venda não for nula")]
        public void IsSatisfiedBy_ShouldValidateSaleExists_WhenSaleIsNotNull()
        {
            // Arrange
            var sale = SaleSpecificationTestData.GenerateSale(SaleStatus.Pending);
            var specification = new SaleExistsSpecification();

            // Act
            var result = specification.IsSatisfiedBy(sale);

            // Assert
            result.Should().BeTrue();
        }

        /// <summary>
        /// Testa se a SaleHasValidQuantitySpecification falha quando a quantidade de itens for maior que 20.
        /// </summary>
        [Fact(DisplayName = "SaleHasValidQuantitySpecification deve falhar quando a quantidade for maior que 20")]
        public void IsSatisfiedBy_ShouldFailWhenQuantityIsGreaterThan20()
        {
            // Arrange
            var sale = SaleSpecificationTestData.GenerateSaleWithInvalidQuantity();
            var specification = new SaleHasValidQuantitySpecification();

            // Act
            var result = specification.IsSatisfiedBy(sale);

            // Assert
            result.Should().BeFalse();
        }

        /// <summary>
        /// Testa se a SaleIsWithinDiscountRulesSpecification passa quando as regras de desconto forem válidas.
        /// </summary>
        [Fact(DisplayName = "SaleIsWithinDiscountRulesSpecification deve passar quando as regras de desconto forem válidas")]
        public void IsSatisfiedBy_ShouldPassWhenDiscountRulesAreValid()
        {
            // Arrange
            var sale = SaleSpecificationTestData.GenerateSaleWithValidDiscounts();
            var specification = new SaleIsWithinDiscountRulesSpecification();

            // Act
            var result = specification.IsSatisfiedBy(sale);

            // Assert
            result.Should().BeTrue();
        }

        /// <summary>
        /// Testa se a SaleIsWithinDiscountRulesSpecification falha quando as regras de desconto não forem atendidas.
        /// </summary>
        [Fact(DisplayName = "SaleIsWithinDiscountRulesSpecification deve falhar quando as regras de desconto não forem atendidas")]
        public void IsSatisfiedBy_ShouldFailWhenDiscountRulesAreInvalid()
        {
            // Arrange
            var sale = SaleSpecificationTestData.GenerateSaleWithInvalidDiscount();
            var specification = new SaleIsWithinDiscountRulesSpecification();

            // Act
            var result = specification.IsSatisfiedBy(sale);

            // Assert
            result.Should().BeFalse();
        }

        /// <summary>
        /// Testa se a SaleNotCancelledSpecification falha quando o status da venda for "Canceled".
        /// </summary>
        [Fact(DisplayName = "SaleNotCancelledSpecification deve falhar quando a venda for cancelada")]
        public void IsSatisfiedBy_ShouldFailWhenSaleIsCancelled()
        {
            // Arrange
            var sale = SaleSpecificationTestData.GenerateSale(SaleStatus.Canceled);
            var specification = new SaleNotCancelledSpecification();

            // Act
            var result = specification.IsSatisfiedBy(sale);

            // Assert
            result.Should().BeFalse();
        }

        /// <summary>
        /// Testa se a SaleNotCancelledSpecification passa quando a venda não for "Canceled".
        /// </summary>
        [Fact(DisplayName = "SaleNotCancelledSpecification deve passar quando a venda não for cancelada")]
        public void IsSatisfiedBy_ShouldPassWhenSaleIsNotCancelled()
        {
            // Arrange
            var sale = SaleSpecificationTestData.GenerateSale(SaleStatus.Pending);
            var specification = new SaleNotCancelledSpecification();

            // Act
            var result = specification.IsSatisfiedBy(sale);

            // Assert
            result.Should().BeTrue();
        }
    }
}
