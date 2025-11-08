using System.ComponentModel.DataAnnotations;

namespace Simple_Billing_API.DTOs.Produto;

public class CreateProdutoDto
{
    [Required (ErrorMessage = "Descrição é obrigatória.")]
    public string Descricao { get; set; } = string.Empty;
    [Required (ErrorMessage = "Valor Unitário é obrigatório.")]
    public decimal ValorUnitario { get; set; }
    [Required (ErrorMessage = "Ativo é obrigatório.")]
    public bool Ativo { get; set; }
}