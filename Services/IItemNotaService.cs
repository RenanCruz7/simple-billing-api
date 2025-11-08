using Simple_Billing_API.DTOs.ItemNota;

namespace Simple_Billing_API.Services;

public interface IItemNotaService
{
    Task<IEnumerable<ItemNotaDto>> GetAllAsync();
    Task<ItemNotaDto?> GetByIdAsync(int id);
    Task<ItemNotaDto> CreateAsync(CreateItemNotaDto createItemNotaDto);
    Task<ItemNotaDto?> UpdateAsync(int id, UpdateItemNotaDto updateItemNotaDto);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<IEnumerable<ItemNotaDto>> GetByNotaFiscalIdAsync(int notaFiscalId);
}
