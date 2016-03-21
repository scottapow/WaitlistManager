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
            
            // Enter or inject the average wait per visit.
            // Could be calculated based on another service which tallys the amount of barbers working
            // and the sum of average cuttime for each barber.
            var email = User.Identity.Name;
            var barber = _context.Barbers.FirstOrDefault(b => b.Email == email);
            
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
                Visit = visit
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
                visit.isCheckedOff = true;
                visit.CheckOffTime = DateTime.Now;
                _context.Update(visit);
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
