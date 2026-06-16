using Microsoft.AspNetCore.Mvc;
using EcommerceModa.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceModa.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var destaques = await _context.Produtos
                .OrderByDescending(p => p.DataCadastro)
                .Take(3)
                .ToListAsync();

            return View(destaques);
        }
    }
}
