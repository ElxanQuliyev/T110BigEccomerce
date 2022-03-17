using DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductManager
    {
        private readonly AgencyContext _context;

        public ProductManager(AgencyContext context)
        {
            _context = context;
        }
        public async Task Add(Product product)
        {
           await _context.Products.AddAsync(product);
           await _context.SaveChangesAsync();
        }

        public async Task Update(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var selectedProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id==id && !x.IsDeleted);
            if (selectedProduct == null) return;
            selectedProduct.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
        public List<Product> GetAll(Func<Product,bool> filter=null)
        {
            return  _context.Products
                .Where(p=>!p.IsDeleted)
                .Include(p => p.Category)
                .Include(p => p.ProductRecords)
                .Include(p => p.ProductPictures).ThenInclude(p => p.Picture)
                .Where(filter)
                .OrderByDescending(c=>c.ModifiedOn).ToList();
        }
        public async Task<List<Product>> GetAllAdmin()
        {
            return await _context.Products.
                Where(x => !x.IsDeleted)
                .Include(p => p.Category)
                .Include(p => p.ProductRecords)
                .OrderByDescending(c => c.ModifiedOn).ToListAsync();
        }

        public async Task<List<Product>> SearchProduct(string? q,int? categoryId,decimal? minPrice,decimal? maxPrice,int? sortBy,int pageNo,int recordSize)
        {

            var products = _context.Products
                .Where(p=>!p.IsDeleted)
                .Include(p => p.Category)
                .Include(p => p.ProductRecords)
                
                .Include(p=>p.ProductPictures).ThenInclude(p=>p.Picture)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
            {
                products = products.Where(p => p.ProductRecords.Any(c => c.LanguageId==1 && c.Name.ToLower().Contains(q.ToLower())));
            }
            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId);
            }

            if (minPrice.HasValue && maxPrice.HasValue)
            {
                products = products.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
            }
            if(sortBy != null)
            {
                products = sortBy switch
                {
                    1 => products.OrderByDescending(c => c.Price),
                    2 => products.OrderBy(c => c.Price),
                    _ => products.OrderByDescending(c => c.PublishDate),
                };
            }
            int skipCount = (pageNo - 1) * recordSize;

            return await products.Skip(skipCount).Take(recordSize).OrderByDescending(c=>c.ModifiedOn).ToListAsync();
        }

        public int ProductCount()
        {
            return _context.Products.Where(c=>!c.IsDeleted).Count();
        }

        public async Task<Product?> GetById(int id)
        {
            var selectedProduct = await _context.Products
                .Include(c=>c.ProductRecords).Include(c=>c.ProductPictures).ThenInclude(c=>c.Picture)
                .FirstOrDefaultAsync(p=>!p.IsDeleted && p.Id==id);
            if (selectedProduct == null) return null;
            return selectedProduct;
        }
    }
}
