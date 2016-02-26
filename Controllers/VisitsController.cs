using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WaitlistManager.Models;
using System;
using WaitlistManager.Services;

namespace WaitlistManager.Controllers
{
    public class VisitsController : Controller
    {
        private ApplicationDbContext _context;
        private IWaitCalculator _waitcalc;

        public VisitsController(ApplicationDbContext context, IWaitCalculator waitcalc)
        {
            _context = context;
            _waitcalc = waitcalc;
        }

        // GET: Visits
        public IActionResult Index()
        {
            //Enter or inject the average wait per visit.
            //Could be calculated based on another service which tallys the amount of barbers working
                // and the sum of average cuttime for each barber.
            var currentWaitPerVisitor = 8.00;

            var applicationDbContext = _context.Visits.Include(v => v.Barber);
            var activeVisitors = _context.Visits.Count() - _context.Visits.Where(x => x.isCheckedOff).Count();
            ViewData["wait"] = _waitcalc.CalculateWait(activeVisitors, currentWaitPerVisitor).ToLocalTime().ToString("hh:mm tt");
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
            ViewData["BarberId"] = new SelectList(_context.Barbers, "BarberId", "FullName");
            return View();
        }

        // POST: Visits/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Visit visit)
        {
            if (ModelState.IsValid)
            {
                visit.SignInTime = DateTime.Now;
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
