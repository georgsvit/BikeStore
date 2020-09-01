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
    public class ModelPrefixesModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<ModelPrefix> Prefixes { get; set; }

        public string Value { get; set; }

        public ModelPrefixesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGet()
        {
            Prefixes = await _context.ModelPrefixes.ToListAsync();
        }

        public async Task OnPostCreate([Bind("Value")] ModelPrefix modelPrefix)
        {
            ValidatePrefix(modelPrefix, ModelState);
            if (ModelState.IsValid)
            {
                _context.ModelPrefixes.Add(modelPrefix);
                await _context.SaveChangesAsync();
            }

            Prefixes = await _context.ModelPrefixes.ToListAsync();
        }

        public async Task OnPostEdit([Bind("Id, Value")] ModelPrefix modelPrefix)
        {
            var oldPrefix = _context.ModelPrefixes.AsNoTracking().SingleOrDefault(p => p.Id == modelPrefix.Id);
            if (ModelState.IsValid)
            {
                if (!modelPrefix.Equals(oldPrefix) && !_context.ModelPrefixes.Any(p => p.Value == modelPrefix.Value && p.Id != modelPrefix.Id))
                {
                    oldPrefix = modelPrefix;
                    _context.ModelPrefixes.Update(oldPrefix);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError("Value", "This prefix is not unique.");
                    ModelState["Value"].ValidationState = ModelValidationState.Invalid;
                }
            }
            Prefixes = await _context.ModelPrefixes.ToListAsync();
        }

        public async Task OnPostDelete(int id)
        {
            var modelPrefix = await _context.ModelPrefixes.FindAsync(id);

            if (modelPrefix != null)
            {
                _context.ModelPrefixes.Remove(modelPrefix);
                await _context.SaveChangesAsync();
            }
            Prefixes = await _context.ModelPrefixes.ToListAsync();
        }

        public void ValidatePrefix(ModelPrefix modelPrefix, ModelStateDictionary ms)
        {
            if (_context.ModelPrefixes.Any(g => g.Value == modelPrefix.Value))
            {
                ms.AddModelError("Value", "This prefix is not unique.");
                ms["Value"].ValidationState = ModelValidationState.Invalid;
            }
        }
    }
}
