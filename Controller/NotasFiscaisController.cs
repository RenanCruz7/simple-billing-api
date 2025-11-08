using Microsoft.AspNetCore.Mvc;
using Simple_Billing_API.DTOs.NotaFiscal;
using Simple_Billing_API.Services;

namespace Simple_Billing_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotasFiscaisController : ControllerBase
{
    private readonly INotaFiscalService _notaFiscalService;

    public NotasFiscaisController(INotaFiscalService notaFiscalService)
    {
        _notaFiscalService = notaFiscalService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NotaFiscalDto>>> GetAll()
    {
        var notas = await _notaFiscalService.GetAllAsync();
        return Ok(notas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NotaFiscalDto>> GetById(int id)
    {
        var nota = await _notaFiscalService.GetByIdAsync(id);
        if (nota == null)
            return NotFound();

        return Ok(nota);
    }

    [HttpGet("cliente/{clienteId}")]
    public async Task<ActionResult<IEnumerable<NotaFiscalDto>>> GetByClienteId(int clienteId)
    {
        var notas = await _notaFiscalService.GetByClienteIdAsync(clienteId);
        return Ok(notas);
    }

    [HttpPost]
    public async Task<ActionResult<NotaFiscalDto>> Create([FromBody] CreateNotaFiscalDto createNotaFiscalDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var nota = await _notaFiscalService.CreateAsync(createNotaFiscalDto);
            return CreatedAtAction(nameof(GetById), new { id = nota.Id }, nota);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<NotaFiscalDto>> Update(int id, [FromBody] UpdateNotaFiscalDto updateNotaFiscalDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var nota = await _notaFiscalService.UpdateAsync(id, updateNotaFiscalDto);
            if (nota == null)
                return NotFound();

            return Ok(nota);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await _notaFiscalService.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }

    [HttpHead("{id}")]
    public async Task<ActionResult> Exists(int id)
    {
        var exists = await _notaFiscalService.ExistsAsync(id);
        if (!exists)
            return NotFound();

        return Ok();
    }
}
