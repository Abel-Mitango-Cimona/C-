using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceModa.Data;
using EcommerceModa.Models;
using System.Text.Json;

namespace EcommerceModa.Controllers
{
    public class CarrinhoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string SESSION_KEY = "carrinho";

        public CarrinhoController(ApplicationDbContext context)
        {
            _context = context;
        }

        private Carrinho GetCarrinho()
        {
            var json = HttpContext.Session.GetString(SESSION_KEY);
            return json == null ? new Carrinho() : JsonSerializer.Deserialize<Carrinho>(json)!;
        }

        private void SaveCarrinho(Carrinho carrinho)
        {
            HttpContext.Session.SetString(SESSION_KEY, JsonSerializer.Serialize(carrinho));
        }

        // GET: /Carrinho
        public IActionResult Index()
        {
            var carrinho = GetCarrinho();
            return View(carrinho);
        }

        // POST: /Carrinho/Adicionar
        [HttpPost]
        public async Task<IActionResult> Adicionar(int produtoId, int quantidade = 1)
        {
            var produto = await _context.Produtos.FindAsync(produtoId);
            if (produto == null)
            {
                TempData["Erro"] = "Produto não encontrado.";
                return RedirectToAction("Index", "Produto");
            }

            if (produto.Estoque <= 0)
            {
                TempData["Erro"] = "Produto sem estoque disponível.";
                return RedirectToAction("Details", "Produto", new { id = produtoId });
            }

            var carrinho = GetCarrinho();
            var itemAtual = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == produtoId);
            var qtyNoCarrinho = itemAtual?.Quantidade ?? 0;

            if (qtyNoCarrinho + quantidade > produto.Estoque)
            {
                TempData["Erro"] = $"Estoque insuficiente. Disponível: {produto.Estoque - qtyNoCarrinho} unidade(s).";
                return RedirectToAction("Details", "Produto", new { id = produtoId });
            }

            carrinho.AdicionarItem(produto, quantidade);
            SaveCarrinho(carrinho);

            TempData["Sucesso"] = $"\"{produto.Nome}\" adicionado ao carrinho!";
            return RedirectToAction("Index", "Produto");
        }

        // POST: /Carrinho/AtualizarQuantidade
        [HttpPost]
        public IActionResult AtualizarQuantidade(int produtoId, int quantidade)
        {
            var carrinho = GetCarrinho();
            carrinho.AtualizarQuantidade(produtoId, quantidade);
            SaveCarrinho(carrinho);
            return RedirectToAction(nameof(Index));
        }

        // POST: /Carrinho/Remover
        [HttpPost]
        public IActionResult Remover(int produtoId)
        {
            var carrinho = GetCarrinho();
            var item = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == produtoId);
            if (item != null)
            {
                carrinho.RemoverItem(produtoId);
                SaveCarrinho(carrinho);
                TempData["Sucesso"] = $"\"{item.Nome}\" removido do carrinho.";
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: /Carrinho/Limpar
        [HttpPost]
        public IActionResult Limpar()
        {
            var carrinho = GetCarrinho();
            carrinho.Limpar();
            SaveCarrinho(carrinho);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Carrinho/Count (AJAX)
        [HttpGet]
        public IActionResult Count()
        {
            var carrinho = GetCarrinho();
            return Json(carrinho.TotalItens);
        }
    }
}
