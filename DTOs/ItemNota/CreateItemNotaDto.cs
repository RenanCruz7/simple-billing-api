using System.ComponentModel.DataAnnotations;

namespace Simple_Billing_API.DTOs.ItemNota;

public class CreateItemNotaDto
{
    [Required(ErrorMessage = "NotaFiscal é obrigatória.")]
    public int NotaFiscalId { get; set; }
    
    [Required(ErrorMessage = "Produto é obrigatório.")]
    public int ProdutoId { get; set; }
    
    [Required(ErrorMessage = "Quantidade é obrigatória.")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser maior que zero.")]
    public int Quantidade { get; set; }
}
