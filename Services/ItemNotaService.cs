using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Simple_Billing_API.Data;
using Simple_Billing_API.DTOs.ItemNota;
using Simple_Billing_API.Models;

namespace Simple_Billing_API.Services;

public class ItemNotaService : IItemNotaService
{
    private readonly SimpleBillingContext _context;
    private readonly IMapper _mapper;

    public ItemNotaService(SimpleBillingContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ItemNotaDto>> GetAllAsync()
    {
        var itens = await _context.ItensNota
            .Include(i => i.Produto)
            .ToListAsync();
        return _mapper.Map<IEnumerable<ItemNotaDto>>(itens);
    }

    public async Task<ItemNotaDto?> GetByIdAsync(int id)
    {
        var item = await _context.ItensNota
            .Include(i => i.Produto)
            .FirstOrDefaultAsync(i => i.Id == id);
        return item == null ? null : _mapper.Map<ItemNotaDto>(item);
    }

    public async Task<ItemNotaDto> CreateAsync(CreateItemNotaDto createItemNotaDto)
    {
        var item = _mapper.Map<ItemNota>(createItemNotaDto);
        
        // Buscar o produto para calcular o valor total
        var produto = await _context.Produtos.FindAsync(createItemNotaDto.ProdutoId);
        if (produto == null)
            throw new ArgumentException("Produto não encontrado");

        item.ValorTotal = createItemNotaDto.Quantidade * produto.ValorUnitario;
        
        _context.ItensNota.Add(item);
        await _context.SaveChangesAsync();
        
        // Recarregar com includes para retornar completo
        item = await _context.ItensNota
            .Include(i => i.Produto)
            .FirstAsync(i => i.Id == item.Id);
        
        return _mapper.Map<ItemNotaDto>(item);
    }

    public async Task<ItemNotaDto?> UpdateAsync(int id, UpdateItemNotaDto updateItemNotaDto)
    {
        var item = await _context.ItensNota.FindAsync(id);
        if (item == null)
            return null;

        _mapper.Map(updateItemNotaDto, item);
        
        // Recalcular o valor total
        var produto = await _context.Produtos.FindAsync(updateItemNotaDto.ProdutoId);
        if (produto == null)
            throw new ArgumentException("Produto não encontrado");

        item.ValorTotal = updateItemNotaDto.Quantidade * produto.ValorUnitario;
        
        await _context.SaveChangesAsync();
        
        // Recarregar com includes
        item = await _context.ItensNota
            .Include(i => i.Produto)
            .FirstAsync(i => i.Id == item.Id);
        
        return _mapper.Map<ItemNotaDto>(item);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _context.ItensNota.FindAsync(id);
        if (item == null)
            return false;

        _context.ItensNota.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.ItensNota.AnyAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<ItemNotaDto>> GetByNotaFiscalIdAsync(int notaFiscalId)
    {
        var itens = await _context.ItensNota
            .Include(i => i.Produto)
            .Where(i => i.NotaFiscalId == notaFiscalId)
            .ToListAsync();
        return _mapper.Map<IEnumerable<ItemNotaDto>>(itens);
    }
}
