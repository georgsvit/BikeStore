using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.Data;
using BikeStore.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        private void CheckSupplyHeader(SupplyHeader h)
        {
            if (h.SupplyDetail == null)
            {
                _context.Remove(h);
                _context.SaveChanges();
            }
        }

        private void RemoveEmptySupplyHeaders()
        {
            var headers = _context.SupplyHeaders.ToList();
            _context.SupplyDetails.ToList();
            headers.ForEach(h => CheckSupplyHeader(h));
        }

        #region Supply

        public IActionResult CreateSupply()
        {
            var items = _context.Users.Select(u => u.Id).ToList();
            // TODO: add 'only for staff' filter
            ViewData["Staff"] = _context.Users.Select(u => new SelectListItem(u.GetFullName, u.Id));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupply([Bind("Provider, RecipientId")] SupplyHeader header)
        {
            if (ModelState.IsValid)
            {
                RemoveEmptySupplyHeaders();
                header.CreationDate = DateTime.Now;
                _context.SupplyHeaders.Add(header);
                await _context.SaveChangesAsync();
                return RedirectToAction("SupplyBikes", "Admin", new { headerid = header.Id });
            }
            ViewData["Staff"] = new SelectList(_context.Users, "Id", "GetFullName", header.RecipientId);
            return View("CreateSupply", header);       
        }

        public async Task<IActionResult> SupplyBikes(int? headerId)
        {
            if (headerId == null)
            {
                return NotFound();
            }

            var bikeIds = _context.SupplyDetails.Where(sd => sd.SupplyHeaderId == headerId).Select(sd => sd.BikeId);

            var bikes = await _context.Bikes.Where(b => bikeIds.ToList().Contains(b.Id))
                .Include(b => b.ModelColour)
                .Include(b => b.FrameSize)
                .Include(b => b.StoringPlace)
                .ToListAsync();

            
            _context.Colours.ToList();

            return View(bikes);
        }

        #endregion
    }
}
