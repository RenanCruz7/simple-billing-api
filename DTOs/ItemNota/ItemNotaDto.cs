using Simple_Billing_API.DTOs.Produto;

namespace Simple_Billing_API.DTOs.ItemNota;

public class ItemNotaDto
{
    public int Id { get; set; }
    public int NotaFiscalId { get; set; }
    public int ProdutoId { get; set; }
    public ProdutoDto? Produto { get; set; }
    public int Quantidade { get; set; }
    public decimal ValorTotal { get; set; }
}
