using Microsoft.AspNetCore.Mvc;
using Simple_Billing_API.DTOs.Produto;
using Simple_Billing_API.Services;

namespace Simple_Billing_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetAll()
    {
        var produtos = await _produtoService.GetAllAsync();
        return Ok(produtos);
    }

    [HttpGet("ativos")]
    public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetActives()
    {
        var produtos = await _produtoService.GetActivesAsync();
        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoDto>> GetById(int id)
    {
        var produto = await _produtoService.GetByIdAsync(id);
        if (produto == null)
            return NotFound();

        return Ok(produto);
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoDto>> Create([FromBody] CreateProdutoDto createProdutoDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var produto = await _produtoService.CreateAsync(createProdutoDto);
        return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProdutoDto>> Update(int id, [FromBody] UpdateProdutoDto updateProdutoDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var produto = await _produtoService.UpdateAsync(id, updateProdutoDto);
        if (produto == null)
            return NotFound();

        return Ok(produto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await _produtoService.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }

    [HttpHead("{id}")]
    public async Task<ActionResult> Exists(int id)
    {
        var exists = await _produtoService.ExistsAsync(id);
        if (!exists)
            return NotFound();

        return Ok();
    }
}
