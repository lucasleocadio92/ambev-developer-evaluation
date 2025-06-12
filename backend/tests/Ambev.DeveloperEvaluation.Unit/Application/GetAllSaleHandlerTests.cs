using Ambev.DeveloperEvaluation.Application.Sales.GetAllSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain.Sales;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class GetAllSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly GetAllSaleHandler _handler;

        public GetAllSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetAllSaleHandler(_saleRepository, _mapper);
        }

        [Fact(DisplayName = "Given valid paging When retrieving sales Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var request = GetAllSaleCommand.Create(1, 10, "SaleDate");
            var listOfSales = GetAllSaleHandlerTestData.GenerateValidCommand(10);
            var pagedResultOfSales = PagedResult<Sale>.Create((IReadOnlyList<Sale>?)listOfSales[..3], 3, 10, 1);

            _saleRepository.GetAllAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(),
                    Arg.Any<CancellationToken>())
                .Returns(pagedResultOfSales);

            var getSaleResult = pagedResultOfSales.Items!.Select(x
                => new GetSaleResult
                {
                    Id = x.Id,
                    SaleDate = x.SaleDate,
                    TotalAmount = x.TotalAmount,
                    Status = x.Status,
                    Branch = x.Branch,
                    Items = x.Items.Select(i => new GetSaleItemDto 
                    { 
                        Discount = i.Discount, 
                        ProductId =  i.ProductId,
                        Quantity = i.Quantity,
                        TotalAmount = i.TotalAmount,
                        UnitPrice = i.UnitPrice
                    }).ToList() 
                }).ToList();

            var getAllSalesResult = PagedResult<GetSaleResult>.Create(
                items: getSaleResult,
                totalCount: pagedResultOfSales.TotalCount,
                pageSize: pagedResultOfSales.PageSize,
                currentPage: pagedResultOfSales.CurrentPage);

            var resultMap = new GetAllSaleResult(getAllSalesResult);

            _mapper.Map<GetAllSaleResult>(Arg.Any<PagedResult<Sale>>()).Returns(resultMap);

            // When
            var result = await _handler.Handle(request, CancellationToken.None);

            // Then
            _mapper.Received(1).Map<GetAllSaleResult>(Arg.Is<PagedResult<Sale>>(x =>
                x.Items!.Count == 3 &&
                x.TotalCount == 3 &&
                x.PageSize == 10 &&
                x.CurrentPage == 1 &&
                x.TotalPages == 1));

            result.Should().NotBeNull();
            result.Sales.Should().NotBeNull();
            result.Sales.Items.Should().NotBeEmpty();
            result.Sales.Items.Should().HaveCount(3);
            result.Sales.PageSize.Should().Be(10);
            result.Sales.TotalCount.Should().Be(3);
            result.Sales.CurrentPage.Should().Be(1);
            result.Sales.TotalPages.Should().Be(1);
            result.Sales.HasPreviousPage.Should().BeFalse();
            result.Sales.HasNextPage.Should().BeFalse();
        }
    }
}
