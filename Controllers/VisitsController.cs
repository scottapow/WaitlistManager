using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WaitlistManager.Models;
using System;
using WaitlistManager.Services;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using WaitlistManager.ViewModels.Visits;

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
            
            var currentWaitPerVisitor = 8.00;
            var visit = new Visit();
            var applicationDbContext = _context.Visits.Include(v => v.Barber);

            var activeVisitors = _context.Visits.Count() -
                _context.Visits.Where(x => x.isCheckedOff).Count();

            ViewData["BarberId"] = new SelectList(_context.Barbers, "BarberId", "FullName");
            ViewData["wait"] = _waitcalc
                .CalculateWait(activeVisitors, currentWaitPerVisitor)
                .ToLocalTime()
                .ToString("hh:mm tt");

            return View(new VisitsViewModel {
                Visits = _context.Visits.ToList(),
                Visit = visit,
                Barbers = _context.Barbers.ToList()
            });
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
        public IActionResult Create(VisitsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var visit = new Visit {
                    FirstName = model.Visit.FirstName,
                    LastName = model.Visit.LastName,
                    BarberId = model.Visit.BarberId
                };
                visit.SignInTime = DateTime.Now;
                _context.Visits.Add(visit);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        [HttpGet]
        public IActionResult Cut(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Visit visit = _context.Visits.Single(m => m.VisitId == id);
            CutViewModel cvm = new CutViewModel();
                cvm.Visitor = visit;

            return View(cvm);
        }

        [HttpPost]
        public async Task<IActionResult> Cut(int id)
        {
            if (ModelState.IsValid)
            {
                
                Visit visit = _context.Visits.SingleOrDefault(m => m.VisitId == id);
                //Barber barber = _context.Barbers.SingleOrDefault(m => m.BarberId == visit.BarberId);
                //    barber.Visits.Add(visit);
                visit.isCheckedOff = true;
                visit.CheckOffTime = DateTime.Now;
                _context.Update(visit);
                //_context.Update(barber);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Visits");
            }
             return View("Index");
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (ModelState.IsValid)
            {
                Visit visit = _context.Visits.SingleOrDefault(m => m.VisitId == id);
                _context.Visits.Remove(visit);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Visits");
            }
            return View("Index");
        }

        [HttpPost]
        public JsonResult SelectBarber(int pass)
        {
            var exists = _context.Barbers.Any(x => x.Password == pass);
            return Json(exists);
        }
    }
}
