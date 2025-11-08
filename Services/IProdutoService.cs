using Simple_Billing_API.DTOs.Produto;

namespace Simple_Billing_API.Services;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoDto>> GetAllAsync();
    Task<ProdutoDto?> GetByIdAsync(int id);
    Task<ProdutoDto> CreateAsync(CreateProdutoDto createProdutoDto);
    Task<ProdutoDto?> UpdateAsync(int id, UpdateProdutoDto updateProdutoDto);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<IEnumerable<ProdutoDto>> GetActivesAsync();
}
