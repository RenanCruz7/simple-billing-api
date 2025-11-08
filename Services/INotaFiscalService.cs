using Simple_Billing_API.DTOs.NotaFiscal;

namespace Simple_Billing_API.Services;

public interface INotaFiscalService
{
    Task<IEnumerable<NotaFiscalDto>> GetAllAsync();
    Task<NotaFiscalDto?> GetByIdAsync(int id);
    Task<NotaFiscalDto> CreateAsync(CreateNotaFiscalDto createNotaFiscalDto);
    Task<NotaFiscalDto?> UpdateAsync(int id, UpdateNotaFiscalDto updateNotaFiscalDto);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<IEnumerable<NotaFiscalDto>> GetByClienteIdAsync(int clienteId);
}
