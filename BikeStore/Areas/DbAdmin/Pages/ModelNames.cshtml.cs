using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.Attributes;
using BikeStore.Data;
using BikeStore.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.Areas.DbAdmin.Pages
{
    [ForAdmin]
    public class ModelNamesModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<ModelName> Names { get; set; }

        public string Value { get; set; }

        public ModelNamesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGet()
        {
            Names = await _context.ModelNames.ToListAsync();
        }

        public async Task OnPostCreate([Bind("Value")] ModelName modelName)
        {
            ValidateName(modelName, ModelState);
            if (ModelState.IsValid)
            {
                _context.ModelNames.Add(modelName);
                await _context.SaveChangesAsync();
            }

            Names = await _context.ModelNames.ToListAsync();
        }

        public async Task OnPostEdit([Bind("Id, Value")] ModelName modelName)
        {
            var oldName = _context.ModelNames.AsNoTracking().SingleOrDefault(n => n.Id == modelName.Id);
            if (ModelState.IsValid)
            {
                if (!modelName.Equals(oldName) && !_context.ModelNames.Any(n => n.Value == modelName.Value && n.Id != modelName.Id))
                {
                    oldName = modelName;
                    _context.ModelNames.Update(oldName);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError("Value", "Дане ім'я не є унікальним");
                    ModelState["Value"].ValidationState = ModelValidationState.Invalid;
                }
            }
            Names = await _context.ModelNames.ToListAsync();
        }

        public async Task OnPostDelete(int id)
        {
            var modelName = await _context.ModelNames.FindAsync(id);

            if (modelName != null)
            {
                _context.ModelNames.Remove(modelName);
                await _context.SaveChangesAsync();
            }
            Names = await _context.ModelNames.ToListAsync();
        }

        public void ValidateName(ModelName modelName, ModelStateDictionary ms)
        {
            if (_context.ModelNames.Any(n => n.Value == modelName.Value))
            {
                ms.AddModelError("Value", "Дане ім'я не є унікальним");
                ms["Value"].ValidationState = ModelValidationState.Invalid;
            }
        }
    }
}
