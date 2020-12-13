using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebLab.DAL.Data;
using WebLab.DAL.Entities;

namespace WebLab.Areas.Admin.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly WebLab.DAL.Data.ApplicationDbContext _context;

        public DetailsModel(WebLab.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Food Food { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Food = await _context.Foods
                .Include(p => p.Group).FirstOrDefaultAsync(m => m.FoodId == id);

            if (Food == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
