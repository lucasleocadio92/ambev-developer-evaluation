using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Profile for mapping between Sale entity and CreateSaleResult
    /// </summary>
    public class CreateSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateSale operation
        /// </summary>
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleCommand, Sale>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))  
                .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<CreateSaleItemCommand, SaleItem>()
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => (src.Quantity * src.UnitPrice) - src.Discount)); // Calculando o total do item após o desconto

            CreateMap<Sale, CreateSaleResult>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount)) 
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}
