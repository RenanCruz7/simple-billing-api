using System.ComponentModel.DataAnnotations;

namespace Simple_Billing_API.DTOs.Cliente;

public class CreateClienteDto
{
    [Required(ErrorMessage = "Nome é obrigatório.")]
    public string Nome { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Email inválido.")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Telefone é obrigatório.")]
    public string Telefone { get; set; } = string.Empty;
}
