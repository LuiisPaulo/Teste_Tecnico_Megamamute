using Microsoft.AspNetCore.Mvc;
using Database;
using ProdutosModels;
using Microsoft.EntityFrameworkCore;

namespace ProdutosApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosApiController : ControllerBase
    {
        public readonly DatabaseProdutos _context;
        public ProdutosApiController(DatabaseProdutos context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _context.Produtos.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var produtos = await _context.Produtos.FindAsync(id);
            return produtos == null ? NotFound() : Ok(produtos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }

            if (!_context.Produtos.Any(p => p.Id == id))
            {
                return NotFound();
            }

            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

