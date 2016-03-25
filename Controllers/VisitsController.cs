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
        private IAverageCalculator _avecalc;

        public VisitsController(ApplicationDbContext context, IWaitCalculator waitcalc, IAverageCalculator avecalc)
        {
            _context = context;
            _waitcalc = waitcalc;
            _avecalc = avecalc;
        }

        // GET: Visits
        public IActionResult Index()
        {

            if (!_context.Shops.Any())
            {
                return RedirectToAction("Index", "Shops");
            }
            

            var visit = new Visit();
            var currentVisits = _context.Visits;
            var activeVisitors = currentVisits.Count() -
                currentVisits.Where(x => x.isCheckedOff).Count();
            var shop = _context.Shops.First();

            if (currentVisits.Count() != 0)
            {
                foreach (var v in currentVisits)
                {
                    v.WaitTime = (v.WaitTime - DateTime.Now.Subtract(v.SignInTime).TotalMinutes) + 1;
                    _context.Update(v);
                }
                _context.SaveChanges();
            };

            ViewData["BarberId"] = new SelectList(_context.Barbers, "BarberId", "FullName");
            ViewData["wait"] = _waitcalc
                .CalculateWait(activeVisitors, shop.CutTimeAverage)
                .ToLocalTime()
                .ToString("hh:mm tt");
            ViewData["ShopName"] = shop.ShopName;

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
                    BarberId = model.Visit.BarberId,
                };
                visit.Barber = _context.Barbers.SingleOrDefault(x => x.BarberId == model.Visit.BarberId);

                var currentVisits = _context.Visits;
                var barbersVisitCount = currentVisits.Where(x => x.BarberId != null).GroupBy(g => g.BarberId)
                    .ToDictionary(gx => gx.Key, gx => gx.ToList());

                int activeVisitors = currentVisits.Count() -
                currentVisits.Where(x => x.isCheckedOff).Count();
                var shop = _context.Shops.First();

                if (visit.Barber != null)
                {
                    var bvc = 0;
                    if (!currentVisits.Any(x => x.BarberId == model.Visit.BarberId))
                    {
                        bvc = 0;
                    } else
                    {
                        bvc = barbersVisitCount[visit.BarberId].Count;
                    }
                    visit.WaitTime = _waitcalc
                                    .CalculateWaitPerVisit(activeVisitors,
                                                           shop.CutTimeAverage,
                                                           visit.Barber.AvgCutTime,
                                                           bvc);
                } else
                {
                    visit.WaitTime = (shop.CutTimeAverage * activeVisitors);
                }
                
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
                var barbers = _context.Barbers;
            // retrieved from the SelectBarber action.
                int pass = (int)TempData["pass"];
            // adding or updating the Visit's Barber & BarberId based on who is cutting(pass)
                visit.Barber = barbers.SingleOrDefault(m => m.Password == pass);
                visit.BarberId = barbers.SingleOrDefault(m => m.Password == pass).BarberId;
                Barber barber = barbers.SingleOrDefault(m => m.BarberId == visit.BarberId);
                var shop = _context.Shops.First();
                var cutLength = shop.CutTimeAverage;
                // Saves the amount of minutes since the last checkoff and now
                if (visit.BarberId != null)
                {
                    var visitsWId = _context.Visits.Where(x => x.BarberId == visit.BarberId).ToList();
                    var last = visitsWId.FindLast(x => x.isCheckedOff);
                    if (last != null)
                    {
                        cutLength = (DateTime.Now - last.CheckOffTime).TotalMinutes;

                        barber.AvgCutTime = _avecalc.CalculateAverage(barber.AvgCutTime, barber.VisitAmount, cutLength);
                        barber.VisitAmount += 1;
                    }
                }
                
                shop.CutTimeAverage =
                    _avecalc.CalculateAverage(shop.CutTimeAverage, 
                                                shop.TotalCompletedVisits, 
                                                cutLength);
                shop.TotalCompletedVisits += 1;
                _context.Update(shop);
                
                visit.isCheckedOff = true;
                visit.CheckOffTime = DateTime.Now;


                _context.Update(visit);
                _context.Update(barber);
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
            TempData["pass"] = pass;
            var exists = _context.Barbers.Any(x => x.Password == pass);
            return Json(exists);
            
        }
    }
}
