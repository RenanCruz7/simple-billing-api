using Microsoft.AspNetCore.Mvc;
using Simple_Billing_API.DTOs.ItemNota;
using Simple_Billing_API.Services;

namespace Simple_Billing_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItensNotaController : ControllerBase
{
    private readonly IItemNotaService _itemNotaService;

    public ItensNotaController(IItemNotaService itemNotaService)
    {
        _itemNotaService = itemNotaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemNotaDto>>> GetAll()
    {
        var itens = await _itemNotaService.GetAllAsync();
        return Ok(itens);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemNotaDto>> GetById(int id)
    {
        var item = await _itemNotaService.GetByIdAsync(id);
        if (item == null)
            return NotFound();

        return Ok(item);
    }

    [HttpGet("nota-fiscal/{notaFiscalId}")]
    public async Task<ActionResult<IEnumerable<ItemNotaDto>>> GetByNotaFiscalId(int notaFiscalId)
    {
        var itens = await _itemNotaService.GetByNotaFiscalIdAsync(notaFiscalId);
        return Ok(itens);
    }

    [HttpPost]
    public async Task<ActionResult<ItemNotaDto>> Create([FromBody] CreateItemNotaDto createItemNotaDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var item = await _itemNotaService.CreateAsync(createItemNotaDto);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ItemNotaDto>> Update(int id, [FromBody] UpdateItemNotaDto updateItemNotaDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var item = await _itemNotaService.UpdateAsync(id, updateItemNotaDto);
            if (item == null)
                return NotFound();

            return Ok(item);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await _itemNotaService.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }

    [HttpHead("{id}")]
    public async Task<ActionResult> Exists(int id)
    {
        var exists = await _itemNotaService.ExistsAsync(id);
        if (!exists)
            return NotFound();

        return Ok();
    }
}
