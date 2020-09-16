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
    public class SexesModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<Sex> Sexes { get; set; }

        public string Value { get; set; }

        public SexesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGet()
        {
            Sexes = await _context.Sexes.ToListAsync();
        }

        public async Task OnPostCreate([Bind("Value")] Sex sex)
        {
            ValidateSex(sex, ModelState);
            if (ModelState.IsValid)
            {
                _context.Sexes.Add(sex);
                await _context.SaveChangesAsync();
            }

            Sexes = await _context.Sexes.ToListAsync();
        }

        public async Task OnPostEdit([Bind("Id, Value")] Sex sex)
        {
            var oldSex = _context.Sexes.AsNoTracking().SingleOrDefault(s => s.Id == sex.Id);
            if (ModelState.IsValid)
            {
                if (!sex.Equals(oldSex) && !_context.Sexes.Any(s => s.Value == sex.Value && s.Id != sex.Id))
                {
                    oldSex = sex;
                    _context.Sexes.Update(oldSex);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError("Value", "Дана стать не є унікальною");
                    ModelState["Value"].ValidationState = ModelValidationState.Invalid;
                }
            }
            Sexes = await _context.Sexes.ToListAsync();
        }

        public async Task OnPostDelete(int id)
        {
            var sex = await _context.Sexes.FindAsync(id);

            if (sex != null)
            {
                _context.Sexes.Remove(sex);
                await _context.SaveChangesAsync();
            }
            Sexes = await _context.Sexes.ToListAsync();
        }

        public void ValidateSex(Sex sex, ModelStateDictionary ms)
        {
            if (_context.Sexes.Any(s => s.Value == sex.Value))
            {
                ms.AddModelError("Value", "Дана стать не є унікальною");
                ms["Value"].ValidationState = ModelValidationState.Invalid;
            }
        }
    }
}