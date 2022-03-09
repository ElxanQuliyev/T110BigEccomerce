using Microsoft.AspNetCore.Mvc;
using Services;
using System.Diagnostics;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductManager _productManager;
        public HomeController(ILogger<HomeController> logger, ProductManager productManager)
        {
            _logger = logger;
            _productManager = productManager;
        }

        public IActionResult Index()
        {
            IndexVM vm = new()
            {
                ProductFeatured = _productManager.GetAll(c => c.IsFeatured),
                ProductIsSlider = _productManager.GetAll(c => c.IsSlider)
            };
            return View(vm);
        }


        public async Task<IActionResult> OpenModal(int? id)
        {
            if (id == null) return NotFound();
           var selectedProduct=await _productManager.GetById(id.Value);
            if (selectedProduct == null) return NotFound();
            return PartialView("_ProductModal",selectedProduct);
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