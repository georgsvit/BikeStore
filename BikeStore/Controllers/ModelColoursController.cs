using BikeStore.Attributes;
using BikeStore.Data;
using BikeStore.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Controllers
{
    [NotForCustomer]
    public class ModelColoursController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ModelColoursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ModelColours for defined Model
        public async Task<IActionResult> Index(int? modelId)
        {
            if (modelId == null)
            {
                return NotFound();
            }
            ViewData["ModelId"] = modelId;
            var applicationDbContext = _context.ModelColours.Where(mc => mc.ModelId == modelId).Include(m => m.Colour).Include(m => m.Model);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ModelColours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelColour = await _context.ModelColours
                .Include(m => m.Colour)
                .Include(m => m.Model)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modelColour == null)
            {
                return NotFound();
            }

            return View(modelColour);
        }

        // GET: ModelColours/Create
        public IActionResult Create(int modelId)
        {
            ViewData["ColourId"] = new SelectList(_context.Colours, "Id", "ColourValue");
            ViewData["ModelId"] = new SelectList(_context.Models, "Id", "Description");
            return View(new ModelColour() { ModelId = modelId });
        }

        // POST: ModelColours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageLink,ModelId,ColourId")] ModelColour modelColour)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modelColour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { modelId = modelColour.ModelId });
            }
            ViewData["ColourId"] = new SelectList(_context.Colours, "Id", "ColourValue", modelColour.ColourId);
            ViewData["ModelId"] = new SelectList(_context.Models, "Id", "Description", modelColour.ModelId);
            return View(modelColour);
        }

        // GET: ModelColours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelColour = await _context.ModelColours.FindAsync(id);
            if (modelColour == null)
            {
                return NotFound();
            }
            ViewData["ColourId"] = new SelectList(_context.Colours, "Id", "ColourValue", modelColour.ColourId);
            ViewData["ModelId"] = new SelectList(_context.Models, "Id", "Description", modelColour.ModelId);
            return View(modelColour);
        }

        // POST: ModelColours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageLink,ModelId,ColourId")] ModelColour modelColour)
        {
            if (id != modelColour.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modelColour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelColourExists(modelColour.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { modelId = modelColour.ModelId });
            }
            ViewData["ColourId"] = new SelectList(_context.Colours, "Id", "ColourValue", modelColour.ColourId);
            ViewData["ModelId"] = new SelectList(_context.Models, "Id", "Description", modelColour.ModelId);
            return View(modelColour);
        }

        // GET: ModelColours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelColour = await _context.ModelColours
                .Include(m => m.Colour)
                .Include(m => m.Model)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modelColour == null)
            {
                return NotFound();
            }

            return View(modelColour);
        }

        // POST: ModelColours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modelColour = await _context.ModelColours.FindAsync(id);
            var modelId = modelColour.ModelId;
            _context.ModelColours.Remove(modelColour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { modelId });
        }

        private bool ModelColourExists(int id)
        {
            return _context.ModelColours.Any(e => e.Id == id);
        }
    }
}
