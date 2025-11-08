using Simple_Billing_API.DTOs.ItemNota;

namespace Simple_Billing_API.DTOs.NotaFiscal;

public class UpdateNotaFiscalDto
{
    public int Numero { get; set; }
    public int ClienteId { get; set; }
    public List<UpdateItemNotaDto> Itens { get; set; } = new();
}

