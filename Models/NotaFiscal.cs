using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simple_Billing_API.Models;

public class NotaFiscal
{
    [Key]
    public int Id { get; set; }
    public int Numero { get; set; }
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }
    public DateTime DataEmissao { get; set; } = DateTime.UtcNow;
    public decimal ValorTotal { get; set; }
    public List<ItemNota> Itens { get; set; } = new();

}