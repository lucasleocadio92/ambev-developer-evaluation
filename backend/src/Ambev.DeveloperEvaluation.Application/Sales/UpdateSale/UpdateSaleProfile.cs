using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Profile for mapping between Sale entity and UpdateSaleResult.
    /// </summary>
    public class UpdateSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for UpdateSale operation.
        /// </summary>
        public UpdateSaleProfile()
        {
            CreateMap<UpdateSaleItemCommand, SaleItem>()
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => (src.Quantity * src.UnitPrice) - src.Discount));

            CreateMap<Sale, UpdateSaleResult>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.SaleDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}
