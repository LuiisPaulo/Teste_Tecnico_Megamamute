using Microsoft.AspNetCore.Mvc;
using Database;
using ProdutosModels;
using Microsoft.EntityFrameworkCore;

namespace WebController
{
    public class ProdutosController : Controller
    {
        private readonly DatabaseProdutos _context;

        public ProdutosController(DatabaseProdutos context) => _context = context;
       
        public async Task<IActionResult> Index() => View(await _context.Produtos.ToListAsync());
        
        public IActionResult Create() => View();


        [HttpPost]
        public async Task<IActionResult> Create(Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return View(produto);
            }
            _context.Add(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            return produto == null ? NotFound() : View(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(produto);
            }
            _context.Update(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            return produto == null ? NotFound() : View(produto);
            
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        
    }
}

