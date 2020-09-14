using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.Data;
using BikeStore.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.Areas.DbAdmin.Pages
{
    public class CategoriesModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<Category> Categories { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public CategoriesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGet()
        {
            Categories = await _context.Categories.ToListAsync();
        }

        public async Task OnPostCreate([Bind("Title, Description")] Category category)
        {
            ValidateCategory(category, ModelState);
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
            }

            Categories = await _context.Categories.ToListAsync();
        }

        public async Task OnPostEdit([Bind("Id, Title, Description")] Category category)
        {
            var oldCategory = _context.Categories.AsNoTracking().SingleOrDefault(c => c.Id == category.Id);
            if (ModelState.IsValid)
            {
                if (!category.Equals(oldCategory) && !_context.Categories.Any(c => (c.Title == category.Title || c.Description == category.Description) && c.Id != category.Id))
                {
                    oldCategory = category;
                    _context.Categories.Update(oldCategory);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError("Title", "Дана назва не є унікальною");
                    ModelState["Title"].ValidationState = ModelValidationState.Invalid;
                }
            }
            Categories = await _context.Categories.ToListAsync();
        }

        public async Task OnPostDelete(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            Categories = await _context.Categories.ToListAsync();
        }

        public void ValidateCategory(Category category, ModelStateDictionary ms)
        {
            if (_context.Categories.Any(c => c.Title == category.Title))
            {
                ms.AddModelError("Title", "Дана назва не є унікальною");
                ms["Title"].ValidationState = ModelValidationState.Invalid;
            }
            if (_context.Statuses.Any(c => c.Description == category.Description))
            {
                ms.AddModelError("Description", "Даний опис не є унікальним");
                ms["Description"].ValidationState = ModelValidationState.Invalid;
            }
        }
    }
}
