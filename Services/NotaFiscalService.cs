using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Simple_Billing_API.Data;
using Simple_Billing_API.DTOs.NotaFiscal;
using Simple_Billing_API.Models;

namespace Simple_Billing_API.Services;

public class NotaFiscalService : INotaFiscalService
{
    private readonly SimpleBillingContext _context;
    private readonly IMapper _mapper;

    public NotaFiscalService(SimpleBillingContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<NotaFiscalDto>> GetAllAsync()
    {
        var notas = await _context.NotasFiscais
            .Include(n => n.Cliente)
            .Include(n => n.Itens)
                .ThenInclude(i => i.Produto)
            .ToListAsync();
        return _mapper.Map<IEnumerable<NotaFiscalDto>>(notas);
    }

    public async Task<NotaFiscalDto?> GetByIdAsync(int id)
    {
        var nota = await _context.NotasFiscais
            .Include(n => n.Cliente)
            .Include(n => n.Itens)
                .ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(n => n.Id == id);
        return nota == null ? null : _mapper.Map<NotaFiscalDto>(nota);
    }

    public async Task<NotaFiscalDto> CreateAsync(CreateNotaFiscalDto createNotaFiscalDto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var nota = _mapper.Map<NotaFiscal>(createNotaFiscalDto);
            
            // Calcular valor total dos itens
            decimal valorTotal = 0;
            foreach (var itemDto in createNotaFiscalDto.Itens)
            {
                var produto = await _context.Produtos.FindAsync(itemDto.ProdutoId);
                if (produto == null)
                    throw new ArgumentException($"Produto com ID {itemDto.ProdutoId} não encontrado");

                var item = _mapper.Map<ItemNota>(itemDto);
                item.ValorTotal = itemDto.Quantidade * produto.ValorUnitario;
                valorTotal += item.ValorTotal;
                
                nota.Itens.Add(item);
            }
            
            nota.ValorTotal = valorTotal;
            
            _context.NotasFiscais.Add(nota);
            await _context.SaveChangesAsync();
            
            await transaction.CommitAsync();
            
            // Recarregar com includes para retornar completo
            nota = await _context.NotasFiscais
                .Include(n => n.Cliente)
                .Include(n => n.Itens)
                    .ThenInclude(i => i.Produto)
                .FirstAsync(n => n.Id == nota.Id);
            
            return _mapper.Map<NotaFiscalDto>(nota);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<NotaFiscalDto?> UpdateAsync(int id, UpdateNotaFiscalDto updateNotaFiscalDto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var nota = await _context.NotasFiscais
                .Include(n => n.Itens)
                .FirstOrDefaultAsync(n => n.Id == id);
            
            if (nota == null)
                return null;

            // Remover itens existentes
            _context.ItensNota.RemoveRange(nota.Itens);
            
            // Mapear dados básicos
            _mapper.Map(updateNotaFiscalDto, nota);
            
            // Adicionar novos itens
            decimal valorTotal = 0;
            foreach (var itemDto in updateNotaFiscalDto.Itens)
            {
                var produto = await _context.Produtos.FindAsync(itemDto.ProdutoId);
                if (produto == null)
                    throw new ArgumentException($"Produto com ID {itemDto.ProdutoId} não encontrado");

                var item = _mapper.Map<ItemNota>(itemDto);
                item.NotaFiscalId = id;
                item.ValorTotal = itemDto.Quantidade * produto.ValorUnitario;
                valorTotal += item.ValorTotal;
                
                nota.Itens.Add(item);
            }
            
            nota.ValorTotal = valorTotal;
            
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            
            // Recarregar com includes
            nota = await _context.NotasFiscais
                .Include(n => n.Cliente)
                .Include(n => n.Itens)
                    .ThenInclude(i => i.Produto)
                .FirstAsync(n => n.Id == nota.Id);
            
            return _mapper.Map<NotaFiscalDto>(nota);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var nota = await _context.NotasFiscais
            .Include(n => n.Itens)
            .FirstOrDefaultAsync(n => n.Id == id);
            
        if (nota == null)
            return false;

        _context.NotasFiscais.Remove(nota);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.NotasFiscais.AnyAsync(n => n.Id == id);
    }

    public async Task<IEnumerable<NotaFiscalDto>> GetByClienteIdAsync(int clienteId)
    {
        var notas = await _context.NotasFiscais
            .Include(n => n.Cliente)
            .Include(n => n.Itens)
                .ThenInclude(i => i.Produto)
            .Where(n => n.ClienteId == clienteId)
            .ToListAsync();
        return _mapper.Map<IEnumerable<NotaFiscalDto>>(notas);
    }
}
