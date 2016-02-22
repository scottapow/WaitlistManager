using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WaitlistManager.Models;

namespace WaitlistManager.Controllers
{
    public class VisitsController : Controller
    {
        private ApplicationDbContext _context;

        public VisitsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Visits
        public IActionResult Index()
        {
            var applicationDbContext = _context.Visits.Include(v => v.Barber);
            return View(applicationDbContext.ToList());
        }

        // GET: Visits/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Visit visit = _context.Visits.Single(m => m.VisitId == id);
            if (visit == null)
            {
                return HttpNotFound();
            }

            return View(visit);
        }

        // GET: Visits/Create
        public IActionResult Create()
        {
            ViewData["BarberId"] = new SelectList(_context.Barbers, "BarberId", "Barber");
            return View();
        }

        // POST: Visits/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Visit visit)
        {
            if (ModelState.IsValid)
            {
                _context.Visits.Add(visit);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["BarberId"] = new SelectList(_context.Barbers, "BarberId", "Barber", visit.BarberId);
            return View(visit);
        }

        // GET: Visits/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Visit visit = _context.Visits.Single(m => m.VisitId == id);
            if (visit == null)
            {
                return HttpNotFound();
            }
            ViewData["BarberId"] = new SelectList(_context.Barbers, "BarberId", "Barber", visit.BarberId);
            return View(visit);
        }

        // POST: Visits/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Visit visit)
        {
            if (ModelState.IsValid)
            {
                _context.Update(visit);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["BarberId"] = new SelectList(_context.Barbers, "BarberId", "Barber", visit.BarberId);
            return View(visit);
        }

        // GET: Visits/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Visit visit = _context.Visits.Single(m => m.VisitId == id);
            if (visit == null)
            {
                return HttpNotFound();
            }

            return View(visit);
        }

        // POST: Visits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Visit visit = _context.Visits.Single(m => m.VisitId == id);
            _context.Visits.Remove(visit);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
