using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WaitlistManager.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WaitlistManager.Controllers
{
    public class SignInController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> New(Visit patron)
        {
            if (!ModelState.IsValid)
                return View(patron);

            patron.SignInTime = DateTime.Now;

            var db = new VisitDataContext();
            db.Patrons.Add(patron);
            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Home", new { id = patron.Id });
        }
    }
}
