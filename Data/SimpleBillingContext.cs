using Microsoft.EntityFrameworkCore;
using Simple_Billing_API.Models;

namespace Simple_Billing_API.Data;

public class SimpleBillingContext(DbContextOptions<SimpleBillingContext> options) : DbContext(options)
{
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<NotaFiscal> NotasFiscais { get; set; }
    public DbSet<ItemNota> ItensNota { get; set; }
}