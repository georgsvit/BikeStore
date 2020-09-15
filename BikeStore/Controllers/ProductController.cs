using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.Data;
using BikeStore.Models.Domain;
using BikeStore.Models.View;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private int itemsPerPage = 2;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(List<int> selectedCategories, 
                                               List<int> selectedSuspensions, 
                                               List<int> selectedSexes, 
                                               List<int> selectedAges, 
                                               List<int> selectedYears, 
                                               List<double> selectedWheels, 
                                               int lowPriceBorder, int highPriceBorder, 
                                               string searchString, int page = 1, SortState sortOrder = SortState.NameAsc)
        {
            List<Model> models = await _context.Models.ToListAsync();

            var modelColours = await _context.ModelColours.ToListAsync();
            await _context.Sexes.ToListAsync();
            await _context.ModelNames.ToListAsync();
            await _context.ModelPrefixes.ToListAsync();
            await _context.Bikes.ToListAsync();
            await _context.Colours.ToListAsync();            

            // Get models which are on stock
            var modelColoursId = modelColours.Where(mc => mc.Bike != null).Select(mc => mc.Id).ToList();
            models = models.Where(m => m.ModelColoursId.Intersect(modelColoursId).Count() != 0).ToList();

            // Filter
            if (selectedCategories.Count != 0) models = models.Where(m => selectedCategories.Any(id => m.CategoryId == id)).ToList();
            if (selectedSuspensions.Count != 0) models = models.Where(m => selectedSuspensions.Any(id => m.SuspensionId == id)).ToList();
            if (selectedSexes.Count != 0) models = models.Where(m => selectedSexes.Any(id => m.SexId == id)).ToList();
            if (selectedAges.Count != 0) models = models.Where(m => selectedAges.Any(id => m.AgeGroupId == id)).ToList();
            if (selectedYears.Count != 0) models = models.Where(m => selectedYears.Any(y => m.Year == y)).ToList();
            if (selectedWheels.Count != 0) models = models.Where(m => selectedWheels.Any(ws => m.WheelSize == ws)).ToList();

            highPriceBorder = (highPriceBorder == 0 && models.Count != 0) ? models.Max(m => (int)m.Price) : highPriceBorder;
            models = models.Where(m => m.Price >= lowPriceBorder && m.Price <= highPriceBorder).ToList();

            // Search by name
            if (!String.IsNullOrEmpty(searchString))
            {
                models = models.Where(m => m.FullName.ToLower().Contains(searchString.ToLower())).ToList();
            }

            #region Sort
            
            // Sort 
            models = sortOrder switch
            {
                SortState.NameDesc => models.OrderByDescending(m => m.Name).ThenByDescending(m => m.Prefix).ToList(),
                SortState.YearAsc => models.OrderBy(m => m.Year).ToList(),
                SortState.YearDesc => models.OrderByDescending(m => m.Year).ToList(),
                SortState.ColourAsc => models.OrderBy(m => m.ModelColour.ToList()[0].Colour.ColourValue).ToList(),
                SortState.ColourDesc => models.OrderByDescending(m => m.ModelColour.ToList()[0].Colour.ColourValue).ToList(),
                SortState.PriceAsc => models.OrderBy(m => m.Price).ToList(),
                SortState.PriceDesc => models.OrderByDescending(m => m.Price).ToList(),
                _ => models.OrderBy(m => m.Name).ThenBy(m => m.Prefix).ToList(),
            };

            ViewData["SortItems"] = new List<SelectListItem>
            {
                new SelectListItem { Text = "Назва  (за зростанням)", Value = "0", Selected = (int)sortOrder == 0},
                new SelectListItem { Text = "Назва (за спаданням)", Value = "1", Selected = (int)sortOrder == 1},
                new SelectListItem { Text = "Рік  (за зростанням)", Value = "2", Selected = (int)sortOrder == 2},
                new SelectListItem { Text = "Рік (за спаданням)", Value = "3", Selected = (int)sortOrder == 3},
                new SelectListItem { Text = "Колір  (за зростанням)", Value = "4", Selected = (int)sortOrder == 4},
                new SelectListItem { Text = "Колір (за спаданням)", Value = "5", Selected = (int)sortOrder == 5},
                new SelectListItem { Text = "Ціна (за зростанням)", Value = "6", Selected = (int)sortOrder == 6},
                new SelectListItem { Text = "Ціна  (за спаданням)", Value = "7", Selected = (int)sortOrder == 7},
            };          

            #endregion

            // Pagination
            int modelsCount = models.Count;
            models = models.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            
            ViewData["PageViewModel"] = new PageViewModel(modelsCount, page, itemsPerPage);
            ViewData["FilterViewModel"] = new FilterViewModel(await _context.Categories.ToListAsync(), selectedCategories,
                                                              await _context.Suspensions.ToListAsync(), selectedSuspensions,
                                                              await _context.Sexes.ToListAsync(), selectedSexes,
                                                              await _context.AgeGroups.ToListAsync(), selectedAges,
                                                              await _context.Models.Select(m => m.Year).Distinct().ToListAsync(), selectedYears,
                                                              await _context.Models.Select(m => m.WheelSize).Distinct().ToListAsync(), selectedWheels,
                                                              lowPriceBorder, 
                                                              (highPriceBorder == 0 && models.Count != 0) ? models.Max(m => (int)m.Price) : highPriceBorder,
                                                              searchString);

            return View(models);
        }
    }
}
