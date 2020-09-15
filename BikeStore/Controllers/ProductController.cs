using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.Data;
using BikeStore.Models.Domain;
using BikeStore.Models.View;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private int itemsPerPage = 6;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            List<Model> models = await _context.Models.ToListAsync();

            await _context.ModelColours.ToListAsync();
            await _context.Sexes.ToListAsync();
            await _context.ModelNames.ToListAsync();
            await _context.ModelPrefixes.ToListAsync();

            models = models.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList();

            ViewData["PageViewModel"] = new PageViewModel(await _context.Models.CountAsync(), page, itemsPerPage);

            return View(models);
        }
    }
}
