using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class CancelSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMediator _mediator;
        private readonly CancelSaleHandler _handler;

        public CancelSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mediator = Substitute.For<IMediator>();
            _handler = new CancelSaleHandler(_saleRepository, _mediator);
        }

        [Fact(DisplayName = "Given valid sale ID When canceling sale Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = new CancelSaleCommand(Guid.NewGuid());
            var sale = new Sale(CustomerTestData.GenerateValidCustomer()) { Id = command.Id, Status = SaleStatus.Pending };

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(sale);
            _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(sale);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Status.Should().Be(SaleStatus.Canceled);
            await _saleRepository.Received(1).UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given non-existing sale ID When canceling sale Then throws KeyNotFoundException")]
        public async Task Handle_NonExistingSale_ThrowsKeyNotFoundException()
        {
            // Given
            var command = new CancelSaleCommand(Guid.NewGuid());
            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((Sale)null);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact(DisplayName = "Given already canceled sale When canceling sale Then throws DomainException")]
        public async Task Handle_AlreadyCanceledSale_ThrowsDomainException()
        {
            // Given
            var command = new CancelSaleCommand(Guid.NewGuid());
            var sale = new Sale(CustomerTestData.GenerateValidCustomer()) { Id = command.Id, Status = SaleStatus.Canceled };
            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(sale);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<DomainException>();
        }
    }
}
