using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WaitlistManager.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Waitlist_Manager.Controllers
{
    public class ShopsController : Controller
    {

        private ApplicationDbContext _context;

        public ShopsController(ApplicationDbContext context)
        {
            _context = context;
        }
        

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Shop model)
        {
            if (ModelState.IsValid)
            {
                var shop = new Shop();
                shop.ShopName = model.ShopName;
                shop.CutTimeAverage = 10.0;
                shop.TotalCompletedVisits = 0;
                _context.Shops.Add(shop);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToActionPermanent("Create", "Barbers");
        }
    }
}
