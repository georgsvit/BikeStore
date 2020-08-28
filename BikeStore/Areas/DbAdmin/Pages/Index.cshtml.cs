using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.Data;
using BikeStore.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.Areas.DbAdmin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<AgeGroup> groups { get; set; }

        public string Value { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGet()
        {
            groups = await _context.AgeGroups.ToListAsync();
        }

        public async Task OnPostCreate(string value)
        {
            var group = new AgeGroup() { Value = value };
            if (ModelState.IsValid)
            {
                _context.AgeGroups.Add(group);
                await _context.SaveChangesAsync();
                groups = await _context.AgeGroups.ToListAsync();
                Value = "";
            }
        }

        public async Task OnPostEdit(int id, string value)
        {
            var group = new AgeGroup() { Id = id, Value = value };
            var oldGroup = _context.AgeGroups.AsNoTracking().SingleOrDefault(g => g.Id == id);

            if (!group.Equals(oldGroup))
            {
                oldGroup = group;
                _context.AgeGroups.Update(oldGroup);
                await _context.SaveChangesAsync();
            }
            Value = "";
            groups = await _context.AgeGroups.ToListAsync();
        }

        public async Task OnPostDelete(int id)
        {
            var ageGroup = await _context.AgeGroups.FindAsync(id);

            if (ageGroup != null)
            {
                _context.AgeGroups.Remove(ageGroup);
                await _context.SaveChangesAsync();
            }
            groups = await _context.AgeGroups.ToListAsync();
        }
    }
}
