using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Web.ViewModels;

namespace Web.Areas.ShopAdmin.Controllers
{
    [Area(nameof(ShopAdmin))]
    public class ProductsController : Controller
    {
        private readonly ProductManager _productManager;
        private readonly IWebHostEnvironment _env;
        private readonly PictureManager _pictureManager;
        public ProductsController(ProductManager productManager, IWebHostEnvironment env, PictureManager pictureManager)
        {
            _productManager = productManager;
            _env = env;
            _pictureManager = pictureManager;
        }

        // GET: ProductsController
        public async Task<IActionResult> Index()
        {
            return View(await _productManager.GetAllAdmin());
        }

        // GET: ProductsController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue) return NotFound();
            var selectedProduct= await _productManager.GetById(id.Value);
            if (selectedProduct == null) return NotFound();
            return View(selectedProduct);
        }

        // GET: ProductsController/Create
        public async Task<IActionResult> Action(int? id)
        
        {
            ProductActionViewModel model = new();
            if (id.HasValue)
            {
                var product = await _productManager.GetById(id.Value);
                if (product == null) return NotFound();

                var currentLanguageRecord = new ProductRecord();
                model.ProductId =product.Id;
                model.CategoryID = product.CategoryId;
                model.Price = product.Price;
                model.Discount = product.Discount;
                model.IsFeatured = product.IsFeatured;
                model.IsSlider= product.IsSlider;
                model.StockQuantity = product.InStock;
                model.ProductPicturesList = product.ProductPictures;
                model.ThumbnailPicture = product.CoverPhotoId;
                model.ProductRecordID = currentLanguageRecord.Id;
                model.Name = currentLanguageRecord.Name;
                model.Summary = currentLanguageRecord.Summary;
                model.Description = currentLanguageRecord.Description;
                model.DayProduct = product.IsDay;
                return View(model);
            }

            return View(model);
        }
        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Action(int? id,IFormCollection collection)
        {
            var model = GetProductActionViewModelFromForm(collection);
            try
            {
                var pictureList = collection.Files;
                var upd = "uploads";
                var rootFile = Path.Combine(_env.WebRootPath, upd);
                if (!Directory.Exists(upd))
                {
                    Directory.CreateDirectory(upd);
                }
                if (id.HasValue)
                {
                }
                else
                {
                    Product product = new()
                    {
                        CategoryId = model.CategoryID,
                        Price = model.Price,
                        Discount = model.Discount,
                        InStock = (ushort)model.StockQuantity,
                        IsFeatured = model.IsFeatured,
                        IsSlider = model.IsSlider,
                        ModifiedOn = DateTime.Now,
                        PublishDate = DateTime.Now,
                        IsDay = model.DayProduct
                    };

                    if (pictureList != null && pictureList.Count>0)
                    {
                        product.ProductPictures = new List<ProductPicture>();
                        foreach (var picture in pictureList)
                        {
                            string fileName = Guid.NewGuid() + picture.FileName;
                            string fileRoot = Path.Combine(rootFile, fileName);
                            using FileStream stream = new(fileRoot, FileMode.Create);
                            picture.CopyTo(stream);
                            Picture newPicture = new()
                            {
                                URL="/uploads/"+fileName
                            };

                            //Picture Added sql code...
                            await _pictureManager.AddPicture(newPicture);
                            product.ProductPictures.Add(
                                new ProductPicture { PictureID = newPicture.Id, ProductID = product.Id }
                             );
                        }
                    }

                    var currentLanguageRecord = new ProductRecord
                    {
                        ProductId = product.Id,
                        LanguageId = 1,
                        Name = model.Name,
                        Summary = model.Summary,
                        Description = model.Description,
                    };
                    product.ProductRecords = new();
                    product.ProductRecords.Add(currentLanguageRecord);
                    await _productManager.Add(product);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        public static ProductActionViewModel GetProductActionViewModelFromForm(IFormCollection collection)
        {
            var model = new ProductActionViewModel
            {
                ProductId=!string.IsNullOrEmpty(collection["ProductId"]) ? int.Parse(collection["ProductId"]) : 0,
                CategoryID = !string.IsNullOrEmpty(collection["CategoryID"]) ? int.Parse(collection["CategoryID"]) : 1,
                Price = decimal.Parse(collection["Price"]),
                Discount = !string.IsNullOrEmpty(collection["Discount"]) ? decimal.Parse(collection["Discount"]) : 0,
                StockQuantity = int.Parse(collection["StockQuantity"]),
                IsFeatured = collection["IsFeatured"].Contains("true"),
                DayProduct = collection["DayProduct"].Contains("true"),
                IsSlider = collection["IsSlider"].Contains("true"),
                ProductPictures = collection["ProductPictures"],
                ThumbnailPicture = !string.IsNullOrEmpty(collection["ThumbnailPicture"]) ? 
                int.Parse(collection["ThumbnailPicture"]) : 0,
                ProductRecordID = !string.IsNullOrEmpty(collection["ProductRecordID"]) ?
                int.Parse(collection["ProductRecordID"]) : 0,
                Name = collection["Name"],
                Summary = collection["Summary"],
                Description = collection["Description"],
            };
            return model;
        }
        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
