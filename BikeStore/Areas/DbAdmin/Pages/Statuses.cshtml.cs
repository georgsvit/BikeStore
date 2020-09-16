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
    public class StatusesModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<Status> Statuses { get; set; }

        public string Value { get; set; }
        public string Description { get; set; }

        public StatusesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGet()
        {
            Statuses = await _context.Statuses.ToListAsync();
        }

        public async Task OnPostCreate([Bind("Value, Description")] Status status)
        {
            ValidateStatus(status, ModelState);
            if (ModelState.IsValid)
            {
                _context.Statuses.Add(status);
                await _context.SaveChangesAsync();
            }

            Statuses = await _context.Statuses.ToListAsync();
        }

        public async Task OnPostEdit([Bind("Id, Value, Description")] Status status)
        {
            var oldStatus = _context.Statuses.AsNoTracking().SingleOrDefault(s => s.Id == status.Id);
            if (ModelState.IsValid)
            {
                if (!status.Equals(oldStatus) && !_context.Statuses.Any(s => (s.Value == status.Value || s.Description == status.Description) && s.Id != status.Id))
                {
                    oldStatus = status;
                    _context.Statuses.Update(oldStatus);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError("Value", "Даний статус не є унікальним");
                    ModelState["Value"].ValidationState = ModelValidationState.Invalid;
                }
            }
            Statuses = await _context.Statuses.ToListAsync();
        }

        public async Task OnPostDelete(int id)
        {
            var status = await _context.Statuses.FindAsync(id);

            if (status != null)
            {
                _context.Statuses.Remove(status);
                await _context.SaveChangesAsync();
            }
            Statuses = await _context.Statuses.ToListAsync();
        }

        public void ValidateStatus(Status status, ModelStateDictionary ms)
        {
            if (_context.Statuses.Any(s => s.Value == status.Value))
            {
                ms.AddModelError("Value", "Даний статус не є унікальним");
                ms["Value"].ValidationState = ModelValidationState.Invalid;
            }
            if (_context.Statuses.Any(s => s.Description == status.Description))
            {
                ms.AddModelError("Description", "Даний опис не є унікальним");
                ms["Description"].ValidationState = ModelValidationState.Invalid;
            }
        }
    }
}
