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

        public async Task<IActionResult> Index(int? categoryId, string? searchTerm,decimal? minPrice,decimal? maxPrice,int? sortBy,int? PageNo)
        {
            PageNo ??= 1;
            int recordSize = 3;
            var searchVm = new SearchProductVM()
            {
                Products = await _productManager.SearchProduct(searchTerm, categoryId, minPrice, maxPrice, sortBy,PageNo.Value,recordSize),
                SearchTerm = searchTerm,
                CategoryId = categoryId,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                SortBy = sortBy,
                PageNo = PageNo,
                Categories=_categoryManager.GetAll()
            };
            searchVm.MaxPage = _productManager.ProductCount();

            searchVm.Pager = new Models.Pager(searchVm.MaxPage, PageNo, recordSize, 3);
            return View(searchVm);
        }
    }
}
