using System.ComponentModel.DataAnnotations;
using Simple_Billing_API.DTOs.ItemNota;

namespace Simple_Billing_API.DTOs.NotaFiscal;

public class CreateNotaFiscalDto
{
    [Required(ErrorMessage = "Número é obrigatório.")]
    public int Numero { get; set; }
    
    [Required(ErrorMessage = "Cliente é obrigatório.")]
    public int ClienteId { get; set; }
    
    public List<CreateItemNotaDto> Itens { get; set; } = new();
}

