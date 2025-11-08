using Simple_Billing_API.DTOs.Cliente;
using Simple_Billing_API.DTOs.ItemNota;

namespace Simple_Billing_API.DTOs.NotaFiscal;

public class NotaFiscalDto
{
    public int Id { get; set; }
    public int Numero { get; set; }
    public int ClienteId { get; set; }
    public ClienteDto? Cliente { get; set; }
    public DateTime DataEmissao { get; set; }
    public decimal ValorTotal { get; set; }
    public List<ItemNotaDto> Itens { get; set; } = new();
}

