using System.ComponentModel.DataAnnotations;

namespace Simple_Billing_API.Models;

public class Produto
{
    [Key]
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal ValorUnitario { get; set; }
    public bool Ativo { get; set; }
}