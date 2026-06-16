using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceModa.Data;
using EcommerceModa.Models;

namespace EcommerceModa.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProdutoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Produto — Listagem de todos os produtos
        public async Task<IActionResult> Index(string? categoria, string? busca)
        {
            var query = _context.Produtos.AsQueryable();

            if (!string.IsNullOrEmpty(categoria))
                query = query.Where(p => p.Categoria == categoria);

            if (!string.IsNullOrEmpty(busca))
                query = query.Where(p => p.Nome.Contains(busca) || (p.Descricao != null && p.Descricao.Contains(busca)));

            ViewBag.Categorias = Produto.Categorias;
            ViewBag.CategoriaAtual = categoria;
            ViewBag.Busca = busca;

            var produtos = await query.OrderByDescending(p => p.DataCadastro).ToListAsync();
            return View(produtos);
        }

        // GET: /Produto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return NotFound();

            return View(produto);
        }

        // GET: /Produto/Create
        public IActionResult Create()
        {
            ViewBag.Categorias = Produto.Categorias;
            return View();
        }

        // POST: /Produto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Descricao,Preco,Categoria,Estoque,Tamanhos,Cor")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                produto.DataCadastro = DateTime.Now;
                _context.Add(produto);
                await _context.SaveChangesAsync();
                TempData["Sucesso"] = $"Produto \"{produto.Nome}\" cadastrado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categorias = Produto.Categorias;
            return View(produto);
        }

        // GET: /Produto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return NotFound();

            ViewBag.Categorias = Produto.Categorias;
            return View(produto);
        }

        // POST: /Produto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,Preco,Categoria,Estoque,Tamanhos,Cor,DataCadastro")] Produto produto)
        {
            if (id != produto.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                    TempData["Sucesso"] = $"Produto \"{produto.Nome}\" atualizado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Produtos.Any(e => e.Id == produto.Id)) return NotFound();
                    throw;
                }
            }
            ViewBag.Categorias = Produto.Categorias;
            return View(produto);
        }

        // GET: /Produto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return NotFound();

            return View(produto);
        }

        // POST: /Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
                TempData["Sucesso"] = $"Produto \"{produto.Nome}\" excluído com sucesso.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
