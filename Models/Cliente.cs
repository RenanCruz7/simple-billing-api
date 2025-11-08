using System.ComponentModel.DataAnnotations;

namespace Simple_Billing_API.Models;

public class Cliente
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    
    
    public ICollection<NotaFiscal> NotasFiscais { get; set; } = new List<NotaFiscal>();
}