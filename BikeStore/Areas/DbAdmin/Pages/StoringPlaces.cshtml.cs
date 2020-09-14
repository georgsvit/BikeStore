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
    public class StoringPlacesModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<StoringPlace> Places { get; set; }

        public string Place { get; set; }

        public StoringPlacesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGet()
        {
            Places = await _context.StoringPlaces.ToListAsync();
        }

        public async Task OnPostCreate([Bind("Place")] StoringPlace storingPlace)
        {
            ValidatePlace(storingPlace, ModelState);
            if (ModelState.IsValid)
            {
                _context.StoringPlaces.Add(storingPlace);
                await _context.SaveChangesAsync();
            }

            Places = await _context.StoringPlaces.ToListAsync();
        }

        public async Task OnPostEdit([Bind("Id, Place")] StoringPlace storingPlace)
        {
            var oldPlace = _context.StoringPlaces.AsNoTracking().SingleOrDefault(p => p.Id == storingPlace.Id);
            if (ModelState.IsValid)
            {
                if (!storingPlace.Equals(oldPlace) && !_context.StoringPlaces.Any(p => p.Place == storingPlace.Place && p.Id != storingPlace.Id))
                {
                    oldPlace = storingPlace;
                    _context.StoringPlaces.Update(oldPlace);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError("Place", "Дане місце не є унікальним");
                    ModelState["Place"].ValidationState = ModelValidationState.Invalid;
                }
            }
            Places = await _context.StoringPlaces.ToListAsync();
        }

        public async Task OnPostDelete(int id)
        {
            var storingPlace = await _context.StoringPlaces.FindAsync(id);

            if (storingPlace != null)
            {
                _context.StoringPlaces.Remove(storingPlace);
                await _context.SaveChangesAsync();
            }
            Places = await _context.StoringPlaces.ToListAsync();
        }

        public void ValidatePlace(StoringPlace storingPlace, ModelStateDictionary ms)
        {
            if (_context.StoringPlaces.Any(p => p.Place == storingPlace.Place))
            {
                ms.AddModelError("Place", "Дане місце не є унікальним");
                ms["Place"].ValidationState = ModelValidationState.Invalid;
            }
        }
    }
}
