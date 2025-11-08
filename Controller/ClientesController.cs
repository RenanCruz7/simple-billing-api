using Microsoft.AspNetCore.Mvc;
using Simple_Billing_API.DTOs.Cliente;
using Simple_Billing_API.Services;

namespace Simple_Billing_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteDto>>> GetAll()
    {
        var clientes = await _clienteService.GetAllAsync();
        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClienteDto>> GetById(int id)
    {
        var cliente = await _clienteService.GetByIdAsync(id);
        if (cliente == null)
            return NotFound();

        return Ok(cliente);
    }

    [HttpPost]
    public async Task<ActionResult<ClienteDto>> Create([FromBody] CreateClienteDto createClienteDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var cliente = await _clienteService.CreateAsync(createClienteDto);
        return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ClienteDto>> Update(int id, [FromBody] UpdateClienteDto updateClienteDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var cliente = await _clienteService.UpdateAsync(id, updateClienteDto);
        if (cliente == null)
            return NotFound();

        return Ok(cliente);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await _clienteService.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }

    [HttpHead("{id}")]
    public async Task<ActionResult> Exists(int id)
    {
        var exists = await _clienteService.ExistsAsync(id);
        if (!exists)
            return NotFound();

        return Ok();
    }
}
