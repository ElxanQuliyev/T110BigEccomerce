using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PictureManager
    {
        private readonly AgencyContext _context;

        public PictureManager(AgencyContext context)
        {
            _context = context;
        }

        public async Task AddPicture(Picture picture)
        {
            await _context.AddAsync(picture);
            await _context.SaveChangesAsync();
        }
    }
}
