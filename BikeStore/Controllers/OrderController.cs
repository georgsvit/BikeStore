using BikeStore.Data;
using BikeStore.Extensions;
using BikeStore.Models.Cart;
using BikeStore.Models.Domain;
using BikeStore.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMailer _mailer;

        public OrderController(ApplicationDbContext context, IMailer mailer)
        {
            _context = context;
            _mailer = mailer;
        }

        public IActionResult Checkout() => View(new OrderHeader());

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderHeader orderHeader)
        {
            if (ModelState.IsValid)
            {
                orderHeader.CreationDate = DateTime.Now;
                orderHeader.UserId = _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name).Id;
                await _context.AddAsync(orderHeader);
                await _context.SaveChangesAsync();

                List<Item> cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");

                foreach (var item in cart)
                {
                    for (int i = 0; i < item.Quantity; i++)
                    {
                        Bike bike = _context.Bikes.FirstOrDefault(x => x.ModelColour == item.ModelColour && x.FrameSizeId == item.FrameSize.Id && x.StatusId == 1);
                        bike.StatusId = 2;
                        _context.Update(bike);
                        await _context.AddAsync(new OrderDetail() { BikeId = bike.Id, OrderHeaderId = orderHeader.Id });
                        await _context.SaveChangesAsync();
                    }
                }
                await _mailer.SendOrderDetails(User.Identity.Name.ToLower(), cart);
                return RedirectToAction("CompletedOrder");
            }
            else
            {
                return View(orderHeader);
            }
        }

        public ViewResult CompletedOrder()
        {
            HttpContext.Session.SetObjectAsJson("cart", new List<Item>());
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
