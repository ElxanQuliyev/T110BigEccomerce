using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Services;

namespace Web.ViewComponents
{
    public class CategoryViewComponent:ViewComponent
    {
        private readonly CategoryManager _categoryManager;

        public CategoryViewComponent(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public ViewViewComponentResult Invoke()
        {
            return View(_categoryManager.GetAll(c=>c.IsFeatured));
        }
    }
}
