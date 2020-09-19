using BikeStore.Attributes;
using BikeStore.Data;
using BikeStore.Models.Domain;
using BikeStore.Models.View;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Controllers
{
    [NotForCustomer]
    public class ModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Models
        public IActionResult Index()
        {
            var applicationDbContext = _context.Models.Include(m => m.AgeGroup).Include(m => m.Category).Include(m => m.ModelName).Include(m => m.ModelPrefix).Include(m => m.Sex).Include(m => m.Suspension).Include(m => m.ModelColour).ThenInclude(m => m.Bike).ThenInclude(b => b.Status).ToList();
            var models = applicationDbContext.Select(m => new AdminModel(m, m.ModelColour.Sum(mc => mc.Bike.Count), m.ModelColour.Sum(mc => mc.Bike.Where(b => b.StatusId == 2).Count()))).ToList();

            return View(models);
        }

        // GET: Models/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Models
                .Include(m => m.AgeGroup)
                .Include(m => m.Category)
                .Include(m => m.ModelName)
                .Include(m => m.ModelPrefix)
                .Include(m => m.Sex)
                .Include(m => m.Suspension)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            await _context.ModelColours.ToListAsync();
            await _context.Colours.ToListAsync();
            await _context.Bikes.ToListAsync();
            await _context.FrameSizes.ToListAsync();
            await _context.Statuses.ToListAsync();
            await _context.StoringPlaces.ToListAsync();

            Dictionary<string, List<int>> ColourAndSizes = new Dictionary<string, List<int>>() { };
            List<Bike> bikes = new List<Bike>() { };
            if (model.ModelColour != null)
            {
                model.ModelColour.ToList().ForEach(mc => ColourAndSizes.Add(mc.Colour.ColourValue, mc.FrameGetter(_context.FrameSizes.Select(f => f.Size).ToList().ToDictionary(x => x, x => int.Parse("0")))));
                bikes = model.ModelColour.ToList().Select(mc => mc.Bike ?? new List<Bike>() { }).ToList().Aggregate((r, l) => r.Union(l).ToList()).ToList();
            }

            ViewData["ColoursAndSizes"] = ColourAndSizes;
            ViewData["AllBikes"] = bikes;

            return View(model);
        }

        public IActionResult Create()
        {
            ViewData["AgeGroupId"] = new SelectList(_context.AgeGroups, "Id", "Value");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title");
            ViewData["ModelNameId"] = new SelectList(_context.ModelNames, "Id", "Value");
            ViewData["ModelPrefixId"] = new SelectList(_context.ModelPrefixes, "Id", "Value");
            ViewData["SexId"] = new SelectList(_context.Sexes, "Id", "Value");
            ViewData["SuspensionId"] = new SelectList(_context.Suspensions, "Id", "Type");
            return View();
        }

        // POST: Models/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Price,Description,Year,WheelSize,SexId,AgeGroupId,ModelNameId,ModelPrefixId,SuspensionId,CategoryId")] Model model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "ModelColours", new { modelId = model.Id });
            }
            ViewData["AgeGroupId"] = new SelectList(_context.AgeGroups, "Id", "Value", model.AgeGroupId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title", model.CategoryId);
            ViewData["ModelNameId"] = new SelectList(_context.ModelNames, "Id", "Value", model.ModelNameId);
            ViewData["ModelPrefixId"] = new SelectList(_context.ModelPrefixes, "Id", "Value", model.ModelPrefixId);
            ViewData["SexId"] = new SelectList(_context.Sexes, "Id", "Value", model.SexId);
            ViewData["SuspensionId"] = new SelectList(_context.Suspensions, "Id", "Type", model.SuspensionId);
            return View(model);
        }

        // GET: Models/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Models.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            ViewData["AgeGroupId"] = new SelectList(_context.AgeGroups, "Id", "Value", model.AgeGroupId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title", model.CategoryId);
            ViewData["ModelNameId"] = new SelectList(_context.ModelNames, "Id", "Value", model.ModelNameId);
            ViewData["ModelPrefixId"] = new SelectList(_context.ModelPrefixes, "Id", "Value", model.ModelPrefixId);
            ViewData["SexId"] = new SelectList(_context.Sexes, "Id", "Value", model.SexId);
            ViewData["SuspensionId"] = new SelectList(_context.Suspensions, "Id", "Type", model.SuspensionId);
            return View(model);
        }

        // POST: Models/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Price,Description,Year,WheelSize,SexId,AgeGroupId,ModelNameId,ModelPrefixId,SuspensionId,CategoryId")] Model model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "ModelColours", new { modelId = model.Id });
            }
            ViewData["AgeGroupId"] = new SelectList(_context.AgeGroups, "Id", "Value", model.AgeGroupId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title", model.CategoryId);
            ViewData["ModelNameId"] = new SelectList(_context.ModelNames, "Id", "Value", model.ModelNameId);
            ViewData["ModelPrefixId"] = new SelectList(_context.ModelPrefixes, "Id", "Value", model.ModelPrefixId);
            ViewData["SexId"] = new SelectList(_context.Sexes, "Id", "Value", model.SexId);
            ViewData["SuspensionId"] = new SelectList(_context.Suspensions, "Id", "Type", model.SuspensionId);
            return View(model);
        }

        // GET: Models/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Models
                .Include(m => m.AgeGroup)
                .Include(m => m.Category)
                .Include(m => m.ModelName)
                .Include(m => m.ModelPrefix)
                .Include(m => m.Sex)
                .Include(m => m.Suspension)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Models/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = await _context.Models.FindAsync(id);
            _context.Models.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModelExists(int id)
        {
            return _context.Models.Any(e => e.Id == id);
        }
    }
}
