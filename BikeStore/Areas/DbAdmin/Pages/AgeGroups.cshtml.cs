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
    public class AgeGroupsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<AgeGroup> Groups { get; set; }

        public string Value { get; set; }

        public AgeGroupsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGet()
        {
            Groups = await _context.AgeGroups.ToListAsync();
        }

        public async Task OnPostCreate([Bind("Value")]AgeGroup group)
        {
            ValidateGroup(group, ModelState);
            if (ModelState.IsValid)
            {
                _context.AgeGroups.Add(group);
                await _context.SaveChangesAsync();            
            } 
            
            Groups = await _context.AgeGroups.ToListAsync();
        }

        public async Task OnPostEdit([Bind("Id, Value")] AgeGroup group)
        {
            var oldGroup = _context.AgeGroups.AsNoTracking().SingleOrDefault(g => g.Id == group.Id);
            if (ModelState.IsValid)
            {              
                if (!group.Equals(oldGroup) && !_context.AgeGroups.Any(g => g.Value == group.Value && g.Id != group.Id))
                {
                    oldGroup = group;
                    _context.AgeGroups.Update(oldGroup);
                    await _context.SaveChangesAsync();
                } else
                {
                    ModelState.AddModelError("Value", "Дане ім'я не є унікальним");
                    ModelState["Value"].ValidationState = ModelValidationState.Invalid;
                }      
            }
            Groups = await _context.AgeGroups.ToListAsync();
        }

        public async Task OnPostDelete(int id)
        {
            var ageGroup = await _context.AgeGroups.FindAsync(id);

            if (ageGroup != null)
            {
                _context.AgeGroups.Remove(ageGroup);
                await _context.SaveChangesAsync();
            }
            Groups = await _context.AgeGroups.ToListAsync();
        }

        public void ValidateGroup(AgeGroup group, ModelStateDictionary ms)
        {
            if (_context.AgeGroups.Any(g => g.Value == group.Value))
            {
                ms.AddModelError("Value", "Дане ім'я не є унікальним");
                ms["Value"].ValidationState = ModelValidationState.Invalid;
            }                            
        }
    }
}
