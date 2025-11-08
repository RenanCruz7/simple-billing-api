using AutoMapper;
using Simple_Billing_API.DTOs.ItemNota;
using Simple_Billing_API.Models;

namespace Simple_Billing_API.Profiles;

public class ItemNotaProfile : Profile
{
    public ItemNotaProfile()
    {
        CreateMap<ItemNota, ItemNotaDto>()
            .ForMember(dest => dest.Produto, opt => opt.MapFrom(src => src.Produto));
            
        CreateMap<CreateItemNotaDto, ItemNota>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ValorTotal, opt => opt.Ignore())
            .ForMember(dest => dest.NotaFiscal, opt => opt.Ignore())
            .ForMember(dest => dest.Produto, opt => opt.Ignore());
            
        CreateMap<UpdateItemNotaDto, ItemNota>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ValorTotal, opt => opt.Ignore())
            .ForMember(dest => dest.NotaFiscal, opt => opt.Ignore())
            .ForMember(dest => dest.Produto, opt => opt.Ignore());
    }
}
