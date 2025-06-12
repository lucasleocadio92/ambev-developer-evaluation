using Ambev.DeveloperEvaluation.Application.Sales.GetAllSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales
{
    public class GetAllSaleProfile : Profile
    {
        public GetAllSaleProfile()
        {
            CreateMap<GetAllSaleRequest, GetAllSaleCommand>();
        }
    }
}
