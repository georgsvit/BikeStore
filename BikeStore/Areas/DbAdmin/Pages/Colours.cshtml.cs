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
    public class ColoursModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<Colour> Colours { get; set; }

        public string ColourValue { get; set; }

        public ColoursModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGet()
        {
            Colours = await _context.Colours.ToListAsync();
        }

        public async Task OnPostCreate([Bind("ColourValue")] Colour colour)
        {
            ValidateColour(colour, ModelState);
            if (ModelState.IsValid)
            {
                _context.Colours.Add(colour);
                await _context.SaveChangesAsync();
            }

            Colours = await _context.Colours.ToListAsync();
        }

        public async Task OnPostEdit([Bind("Id, ColourValue")] Colour colour)
        {
            var oldColour = _context.Colours.AsNoTracking().SingleOrDefault(c => c.Id == colour.Id);
            if (ModelState.IsValid)
            {
                if (!colour.Equals(oldColour) && !_context.Colours.Any(c => c.ColourValue == colour.ColourValue && c.Id != colour.Id))
                {
                    oldColour = colour;
                    _context.Colours.Update(oldColour);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError("ColourValue", "This value is not unique.");
                    ModelState["ColourValue"].ValidationState = ModelValidationState.Invalid;
                }
            }
            Colours = await _context.Colours.ToListAsync();
        }

        public async Task OnPostDelete(int id)
        {
            var colour = await _context.Colours.FindAsync(id);

            if (colour != null)
            {
                _context.Colours.Remove(colour);
                await _context.SaveChangesAsync();
            }
            Colours = await _context.Colours.ToListAsync();
        }

        public void ValidateColour(Colour colour, ModelStateDictionary ms)
        {
            if (_context.Colours.Any(c => c.ColourValue == colour.ColourValue))
            {
                ms.AddModelError("ColourValue", "This value is not unique.");
                ms["ColourValue"].ValidationState = ModelValidationState.Invalid;
            }
        }
    }
}
