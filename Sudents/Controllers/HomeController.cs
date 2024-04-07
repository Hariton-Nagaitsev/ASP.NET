using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sudents.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sudents.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentContext _context;

        public HomeController(ILogger<HomeController> logger, StudentContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _context.Studs.ToListAsync();
            return View("Index", students);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
