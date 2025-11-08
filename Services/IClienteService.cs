using Simple_Billing_API.DTOs.Cliente;

namespace Simple_Billing_API.Services;

public interface IClienteService
{
    Task<IEnumerable<ClienteDto>> GetAllAsync();
    Task<ClienteDto?> GetByIdAsync(int id);
    Task<ClienteDto> CreateAsync(CreateClienteDto createClienteDto);
    Task<ClienteDto?> UpdateAsync(int id, UpdateClienteDto updateClienteDto);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
