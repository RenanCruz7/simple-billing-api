using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Simple_Billing_API.Data;
using Simple_Billing_API.DTOs.Produto;
using Simple_Billing_API.Models;

namespace Simple_Billing_API.Services;

public class ProdutoService : IProdutoService
{
    private readonly SimpleBillingContext _context;
    private readonly IMapper _mapper;

    public ProdutoService(SimpleBillingContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProdutoDto>> GetAllAsync()
    {
        var produtos = await _context.Produtos.ToListAsync();
        return _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
    }

    public async Task<ProdutoDto?> GetByIdAsync(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        return produto == null ? null : _mapper.Map<ProdutoDto>(produto);
    }

    public async Task<ProdutoDto> CreateAsync(CreateProdutoDto createProdutoDto)
    {
        var produto = _mapper.Map<Produto>(createProdutoDto);
        
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
        
        return _mapper.Map<ProdutoDto>(produto);
    }

    public async Task<ProdutoDto?> UpdateAsync(int id, UpdateProdutoDto updateProdutoDto)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
            return null;

        _mapper.Map(updateProdutoDto, produto);
        await _context.SaveChangesAsync();
        
        return _mapper.Map<ProdutoDto>(produto);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
            return false;

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Produtos.AnyAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<ProdutoDto>> GetActivesAsync()
    {
        var produtos = await _context.Produtos.Where(p => p.Ativo).ToListAsync();
        return _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
    }
}
