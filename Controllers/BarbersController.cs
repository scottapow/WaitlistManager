using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WaitlistManager.Models;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using System.IO;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;

namespace WaitlistManager.Controllers
{
    public class BarbersController : Controller
    {
        private ApplicationDbContext _context;
        private IHostingEnvironment _environment;

        public BarbersController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment; 
        }

        // GET: Barbers
        public IActionResult Index()
        {
            return View(_context.Barbers.ToList());
        }

        // GET: Barbers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Barber barber = _context.Barbers.Single(m => m.BarberId == id);
            if (barber == null)
            {
                return HttpNotFound();
            }

            return View(barber);
        }

        // GET: Barbers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Barbers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Barber barber, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "ProfilePicPath");
                if (image != null)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(image.ContentDisposition).FileName.Trim('"');
                    await image.SaveAsAsync(Path.Combine(uploads, fileName));
                    barber.ProfilePicPath = fileName;
                }
                
                _context.Barbers.Add(barber);
                _context.SaveChanges();
                

                return RedirectToAction("Index");
            }
            return View(barber);
        }



        // GET: Barbers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Barber barber = _context.Barbers.Single(m => m.BarberId == id);
            if (barber == null)
            {
                return HttpNotFound();
            }
            return View(barber);
        }

        // POST: Barbers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Barber barber)
        {
            if (ModelState.IsValid)
            {
                _context.Update(barber);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(barber);
        }

        // GET: Barbers/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Barber barber = _context.Barbers.Single(m => m.BarberId == id);
            if (barber == null)
            {
                return HttpNotFound();
            }

            return View(barber);
        }

        // POST: Barbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Barber barber = _context.Barbers.Single(m => m.BarberId == id);
            _context.Barbers.Remove(barber);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
