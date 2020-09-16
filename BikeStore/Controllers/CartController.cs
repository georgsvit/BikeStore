using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.Data;
using BikeStore.Extensions;
using BikeStore.Models.Cart;
using BikeStore.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Host.Mef;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string returnUrl)
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewData["ReturnUrl"] = returnUrl;

            if (cart != null)
            {
                ViewData["Total"] = cart.Sum(item => item.Model.Price * item.Quantity);
            } else
            {
                ViewData["Total"] = 0;
                cart = new List<Item>();
            }

            return View(cart);
        }

        public async Task<IActionResult> Buy(int? modelId, int itemsAmount, string returnUrl, string info)
        {
            if (modelId == null)
            {
                return NotFound();
            }

            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

            cart ??= new List<Item>();

            int[] arr = info.Split(" ").Select(x => Convert.ToInt32(x)).ToArray();

            if (cart.Find(i => i.Model.Id == modelId && i.ModelColour.Id == arr[0] && i.FrameSize.Id == arr[1]) != null)
            {
                cart.Find(i => i.Model.Id == modelId).Quantity++;
            } else
            {
                ModelColour modelColour = await _context.ModelColours.Include(mc => mc.Colour).FirstOrDefaultAsync(x => x.Id == arr[0]);
                FrameSize size = await _context.FrameSizes.FirstOrDefaultAsync(x => x.Id == arr[1]);
                
                Model model = await _context.Models
                .Include(m => m.ModelName)
                .Include(m => m.ModelPrefix)
                .FirstOrDefaultAsync(m => m.Id == modelId);

                if (model == null)
                {
                    return NotFound();
                }

                cart.Add(new Item() { Model = model, ModelColour = modelColour, FrameSize = size, Quantity = itemsAmount });
            }

            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return Redirect(returnUrl);
        }

        public IActionResult Remove(int Id, string returnUrl, int sizeId)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            cart.Remove(cart.FirstOrDefault(i => i.ModelColour.Id == Id && i.FrameSize.Id == sizeId));

            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}
