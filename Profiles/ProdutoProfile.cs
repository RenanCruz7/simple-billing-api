using AutoMapper;
using Simple_Billing_API.DTOs.Produto;
using Simple_Billing_API.Models;

namespace Simple_Billing_API.Profiles;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<Produto, ProdutoDto>();
            
        CreateMap<CreateProdutoDto, Produto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
            
        CreateMap<UpdateProdutoDto, Produto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}