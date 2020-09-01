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
    public class SuspensionsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<Suspension> Suspensions { get; set; }

        public string Type { get; set; }
        public string Description { get; set; }

        public SuspensionsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGet()
        {
            Suspensions = await _context.Suspensions.ToListAsync();
        }

        public async Task OnPostCreate([Bind("Type, Description")] Suspension suspension)
        {
            ValidateSuspension(suspension, ModelState);
            if (ModelState.IsValid)
            {
                _context.Suspensions.Add(suspension);
                await _context.SaveChangesAsync();
            }

            Suspensions = await _context.Suspensions.ToListAsync();
        }

        public async Task OnPostEdit([Bind("Id, Type, Description")] Suspension suspension)
        {
            var oldSuspension = _context.Suspensions.AsNoTracking().SingleOrDefault(s => s.Id == suspension.Id);
            if (ModelState.IsValid)
            {
                if (!suspension.Equals(oldSuspension) && !_context.Suspensions.Any(s => (s.Type == suspension.Type || s.Description == suspension.Description) && s.Id != suspension.Id))
                {
                    oldSuspension = suspension;
                    _context.Suspensions.Update(oldSuspension);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError("Type", "This type is not unique.");
                    ModelState["Type"].ValidationState = ModelValidationState.Invalid;
                }
            }
            Suspensions = await _context.Suspensions.ToListAsync();
        }

        public async Task OnPostDelete(int id)
        {
            var suspension = await _context.Suspensions.FindAsync(id);

            if (suspension != null)
            {
                _context.Suspensions.Remove(suspension);
                await _context.SaveChangesAsync();
            }
            Suspensions = await _context.Suspensions.ToListAsync();
        }

        public void ValidateSuspension(Suspension suspension, ModelStateDictionary ms)
        {
            if (_context.Suspensions.Any(s => s.Type == suspension.Type))
            {
                ms.AddModelError("Type", "This type is not unique.");
                ms["Type"].ValidationState = ModelValidationState.Invalid;
            }
            if (_context.Statuses.Any(s => s.Description == suspension.Description))
            {
                ms.AddModelError("Description", "This description is not unique.");
                ms["Description"].ValidationState = ModelValidationState.Invalid;
            }
        }
    }
}
