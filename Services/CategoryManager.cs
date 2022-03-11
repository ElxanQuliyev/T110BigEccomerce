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
    public class CategoryManager
    {
        private readonly AgencyContext _context;

        public CategoryManager(AgencyContext context)
        {
            _context = context;
        }

        public List<Category>? GetAll(Func<Category,bool>? filter=null)
        {
            if (filter != null)
            {
                return _context.Categories.Include(c=>c.CategoryRecords).Where(filter).ToList();   
            }

            return _context.Categories.Include(c => c.CategoryRecords).ToList();
        }
    }
}
