using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Simple_Billing_API.Data;
using Simple_Billing_API.DTOs.Cliente;
using Simple_Billing_API.Models;

namespace Simple_Billing_API.Services;

public class ClienteService : IClienteService
{
    private readonly SimpleBillingContext _context;
    private readonly IMapper _mapper;

    public ClienteService(SimpleBillingContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ClienteDto>> GetAllAsync()
    {
        var clientes = await _context.Clientes.ToListAsync();
        return _mapper.Map<IEnumerable<ClienteDto>>(clientes);
    }

    public async Task<ClienteDto?> GetByIdAsync(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        return cliente == null ? null : _mapper.Map<ClienteDto>(cliente);
    }

    public async Task<ClienteDto> CreateAsync(CreateClienteDto createClienteDto)
    {
        var cliente = _mapper.Map<Cliente>(createClienteDto);
        
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
        
        return _mapper.Map<ClienteDto>(cliente);
    }

    public async Task<ClienteDto?> UpdateAsync(int id, UpdateClienteDto updateClienteDto)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null)
            return null;

        _mapper.Map(updateClienteDto, cliente);
        await _context.SaveChangesAsync();
        
        return _mapper.Map<ClienteDto>(cliente);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null)
            return false;

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Clientes.AnyAsync(c => c.Id == id);
    }
}
