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
    public class FrameSizesModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<FrameSize> Sizes { get; set; }

        public string Size { get; set; }
        public int MinHeight { get; set; }
        public int MaxHeight { get; set; }

        public FrameSizesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGet()
        {
            Sizes = await _context.FrameSizes.ToListAsync();
        }

        public async Task OnPostCreate([Bind("Size, MinHeight, MaxHeight")] FrameSize frameSize)
        {            
            ValidateSize(frameSize, ModelState);
            if (ModelState.IsValid)
            {
                _context.FrameSizes.Add(frameSize);
                await _context.SaveChangesAsync();
            }

            Sizes = await _context.FrameSizes.ToListAsync();
        }

        public async Task OnPostEdit([Bind("Id, Size, MinHeight, MaxHeight")] FrameSize frameSize)
        {
            var oldSize = _context.FrameSizes.AsNoTracking().SingleOrDefault(s => s.Id == frameSize.Id);
            if (ModelState.IsValid)
            {
                if (!frameSize.Equals(oldSize) && !_context.FrameSizes.Any(s => s.Size == frameSize.Size && s.Id != frameSize.Id))
                {
                    oldSize = frameSize;
                    _context.FrameSizes.Update(oldSize);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError("Size", "Даний розмір рами не є унікальним");
                    ModelState["Size"].ValidationState = ModelValidationState.Invalid;
                }
            }
            Sizes = await _context.FrameSizes.ToListAsync();
        }

        public async Task OnPostDelete(int id)
        {
            var frameSize = await _context.FrameSizes.FindAsync(id);

            if (frameSize != null)
            {
                _context.FrameSizes.Remove(frameSize);
                await _context.SaveChangesAsync();
            }
            Sizes = await _context.FrameSizes.ToListAsync();
        }

        public void ValidateSize(FrameSize frameSize, ModelStateDictionary ms)
        {
            if (_context.FrameSizes.Any(s => s.Size == frameSize.Size))
            {
                ms.AddModelError("Size", "Даний розмір рами не є унікальним");
                ms["Size"].ValidationState = ModelValidationState.Invalid;
            }

            if (_context.FrameSizes.Any(s => s.MinHeight == frameSize.MinHeight && s.MaxHeight == frameSize.MaxHeight))
            {
                ms.AddModelError("MaxHeight", "Дані значення зрісту не є унікальними");
                ms["MaxHeight"].ValidationState = ModelValidationState.Invalid;
            }

            if (frameSize.MinHeight >= frameSize.MaxHeight && frameSize.MaxHeight != 0 && frameSize.MinHeight != 0)
            {
                ms.AddModelError("MinHeight", "Min height can`t be greater or equal than max height");
                ms["MinHeight"].ValidationState = ModelValidationState.Invalid;
            }
        }
    }
}
