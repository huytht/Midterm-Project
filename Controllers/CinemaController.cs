using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Midterm_Project.Controllers
{
    public class CinemaController : Controller
    {
        private readonly DataContext _context;
        public CinemaController(DataContext context)
        {
            _context = context;
        }
        // GET: CinemaController
        public ActionResult ShowAll()
        {
            var listCinema = _context.Cinemas.Select(c=>c);
            return View(listCinema);
        }

        // GET: CinemaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CinemaController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: CinemaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(ShowAll));
            }
            catch
            {
                return View();
            }
        }

        // GET: CinemaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CinemaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CinemaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CinemaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
