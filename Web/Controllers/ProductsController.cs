using Microsoft.AspNetCore.Mvc;
using Services;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductManager _productManager;
        private readonly CategoryManager _categoryManager;
        public ProductsController(ProductManager productManager, CategoryManager categoryManager)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
        }

        public async Task<IActionResult> Index(int? categoryId,string? searchTerm,decimal? minPrice,decimal? maxPrice,int? sortBy,int? PageNo)
        {
            var searchVm = new SearchProductVM()
            {
                Products = await _productManager.SearchProduct(searchTerm, categoryId, minPrice, maxPrice, sortBy),
                SearchTerm = searchTerm,
                CategoryId = categoryId,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                SortBy = sortBy,
                PageNo = PageNo,
                Categories=_categoryManager.GetAll()
            };
               
            return View(searchVm);
        }
    }
}
