using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebLab.DAL.Data;
using WebLab.DAL.Entities;

namespace WebLab.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly WebLab.DAL.Data.ApplicationDbContext _context;

        private IWebHostEnvironment _environment;
        public EditModel(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _environment = env;
        }
        [BindProperty]
        public Food Food { get; set; }
        [BindProperty]
        public IFormFile Image { get; set; }        


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
           ViewData["FoodGroupId"] = new SelectList(_context.FoodGroups, "FoodGroupId", "GroupName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }
        //    if (Image != null)
        //    {
        //        var fileName = $"{Food.FoodId}" + Path.GetExtension(Image.FileName);
        //        Food.Image = fileName;
        //        var path = Path.Combine(_environment.WebRootPath, "Images", fileName);
        //        using (var fStream = new FileStream(path, FileMode.Create))
        //        {
        //            await Image.CopyToAsync(fStream);
        //        }
        //    }
        //}




        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Image != null)
            {
                var fileName = $"{Food.FoodId}" + Path.GetExtension(Image.FileName);
                Food.Image = fileName;
                var path = Path.Combine(_environment.WebRootPath, "Images", fileName);
                using (var fStream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(fStream);
                }
            }

            _context.Attach(Food).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(Food.FoodId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FoodExists(int id)
        {
            return _context.Foods.Any(e => e.FoodId == id);
        }
    }
}
