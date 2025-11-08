using AutoMapper;
using Simple_Billing_API.DTOs.NotaFiscal;
using Simple_Billing_API.Models;

namespace Simple_Billing_API.Profiles;

public class NotaFiscalProfile : Profile
{
    public NotaFiscalProfile()
    {
        CreateMap<NotaFiscal, NotaFiscalDto>()
            .ForMember(dest => dest.Cliente, opt => opt.MapFrom(src => src.Cliente))
            .ForMember(dest => dest.Itens, opt => opt.MapFrom(src => src.Itens));
            
        CreateMap<CreateNotaFiscalDto, NotaFiscal>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DataEmissao, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.ValorTotal, opt => opt.Ignore())
            .ForMember(dest => dest.Cliente, opt => opt.Ignore())
            .ForMember(dest => dest.Itens, opt => opt.MapFrom(src => src.Itens));
            
        CreateMap<UpdateNotaFiscalDto, NotaFiscal>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DataEmissao, opt => opt.Ignore())
            .ForMember(dest => dest.ValorTotal, opt => opt.Ignore())
            .ForMember(dest => dest.Cliente, opt => opt.Ignore())
            .ForMember(dest => dest.Itens, opt => opt.MapFrom(src => src.Itens));
    }
}
