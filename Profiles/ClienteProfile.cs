using AutoMapper;
using Simple_Billing_API.DTOs.Cliente;
using Simple_Billing_API.Models;

namespace Simple_Billing_API.Profiles;

public class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        CreateMap<Cliente, ClienteDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Telefone, opt => opt.MapFrom(src => src.Telefone))
            .ForMember(dest => dest.DataCadastro, opt => opt.MapFrom(src => src.DataCadastro));
            
        CreateMap<CreateClienteDto, Cliente>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DataCadastro, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.NotasFiscais, opt => opt.Ignore());
            
        CreateMap<UpdateClienteDto, Cliente>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DataCadastro, opt => opt.Ignore())
            .ForMember(dest => dest.NotasFiscais, opt => opt.Ignore());
    }
}
