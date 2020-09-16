using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BikeStore.Data;
using BikeStore.Models.Domain;
using Microsoft.AspNetCore.Http.Extensions;
using BikeStore.Attributes;

namespace BikeStore.Controllers
{
    [NotForCustomer]
    public class BikesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BikesController(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task<List<SelectListItem>> getModelColours(int id = -1)
        {
            List<ModelColour> modelColours = await _context.ModelColours.ToListAsync();
            await _context.Models.ToListAsync();
            await _context.Colours.ToListAsync();
            await _context.ModelPrefixes.ToListAsync();
            await _context.ModelNames.ToListAsync();
            return modelColours.Select(mc => mc.Id == id 
                                            ? new SelectListItem(mc.Model.FullNameWithYear + " " + mc.Colour.ColourValue, mc.Id.ToString(), true)
                                            : new SelectListItem(mc.Model.FullNameWithYear + " " + mc.Colour.ColourValue, mc.Id.ToString(), false))
                                            .ToList();
        }

        // GET: Bikes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Bikes.Include(b => b.FrameSize).Include(b => b.ModelColour).Include(b => b.Status).Include(b => b.StoringPlace);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bikes/Create
        public async Task<IActionResult> Create(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["FrameSizeId"] = new SelectList(_context.FrameSizes, "Id", "Size");
            ViewData["ModelColourId"] = await getModelColours();
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Value");
            ViewData["StoringPlaceId"] = new SelectList(_context.StoringPlaces, "Id", "Place");
            return View();
        }

        // POST: Bikes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string returnUrl, [Bind("Id,FrameNumber,StatusId,StoringPlaceId,FrameSizeId,ModelColourId")] Bike bike)
        {
            if (ModelState.IsValid)
            {
                await _context.Bikes.AddAsync(bike);
                await _context.SaveChangesAsync(); 
                
                int supplyHeaderId = int.Parse(returnUrl.Split('=')[1]);
                await _context.SupplyDetails.AddAsync(new SupplyDetail() { SupplyHeaderId = supplyHeaderId, BikeId = bike.Id });
                await _context.SaveChangesAsync();
                return Redirect(returnUrl);                
            }
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["FrameSizeId"] = new SelectList(_context.FrameSizes, "Id", "Size", bike.FrameSizeId);
            ViewData["ModelColourId"] = await getModelColours(bike.ModelColourId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Value", bike.StatusId);
            ViewData["StoringPlaceId"] = new SelectList(_context.StoringPlaces, "Id", "Place", bike.StoringPlaceId);
            return View(bike);
        }

        // GET: Bikes/Edit/5
        public async Task<IActionResult> Edit(int? id, string returnUrl)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bike = await _context.Bikes.FindAsync(id);
            if (bike == null)
            {
                return NotFound();
            }
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["FrameSizeId"] = new SelectList(_context.FrameSizes, "Id", "Size", bike.FrameSizeId);
            ViewData["ModelColourId"] = await getModelColours(bike.ModelColourId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Description", bike.StatusId);
            ViewData["StoringPlaceId"] = new SelectList(_context.StoringPlaces, "Id", "Place", bike.StoringPlaceId);
            return View(bike);
        }

        // POST: Bikes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string returnUrl, [Bind("Id,FrameNumber,StatusId,StoringPlaceId,FrameSizeId,ModelColourId")] Bike bike)
        {
            if (id != bike.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bike);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BikeExists(bike.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect(returnUrl);
            }
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["FrameSizeId"] = new SelectList(_context.FrameSizes, "Id", "Size", bike.FrameSizeId);
            ViewData["ModelColourId"] = await getModelColours(bike.ModelColourId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Description", bike.StatusId);
            ViewData["StoringPlaceId"] = new SelectList(_context.StoringPlaces, "Id", "Place", bike.StoringPlaceId);
            return View(bike);
        }        

        // POST: Bikes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string returnUrl)
        {
            var bike = await _context.Bikes.FindAsync(id);
            _context.Bikes.Remove(bike);
            await _context.SaveChangesAsync();
            return Redirect(returnUrl);
        }

        private bool BikeExists(int id)
        {
            return _context.Bikes.Any(e => e.Id == id);
        }
    }
}
