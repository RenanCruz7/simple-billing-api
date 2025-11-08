namespace Simple_Billing_API.DTOs.Produto;

public class ProdutoDto
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal ValorUnitario { get; set; }
    public bool Ativo { get; set; }
}
