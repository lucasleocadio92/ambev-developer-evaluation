using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class GetSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly GetSaleHandler _handler;

        public GetSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetSaleHandler(_saleRepository, _mapper);
        }

        [Fact(DisplayName = "Given valid sale ID When retrieving sale Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = new GetSaleCommand(Guid.NewGuid());
            var sale = new Sale(CustomerTestData.GenerateValidCustomer()) { Id = command.Id, Status = SaleStatus.Pending };

            var result = new GetSaleResult { Id = sale.Id, Status = sale.Status };

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(sale);
            _mapper.Map<GetSaleResult>(sale).Returns(result);

            // When
            var getSaleResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            getSaleResult.Should().NotBeNull();
            getSaleResult.Id.Should().Be(sale.Id);
            await _saleRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given non-existing sale ID When retrieving sale Then throws KeyNotFoundException")]
        public async Task Handle_NonExistingSale_ThrowsKeyNotFoundException()
        {
            // Given
            var command = new GetSaleCommand(Guid.NewGuid());
            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((Sale)null);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}
