using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Midterm_Project.Controllers
{
    public class FilmController : Controller
    {
        private readonly DataContext _context;
        public FilmController(DataContext context)
        {
            _context = context;
        }
        // GET: FilmController
        public ActionResult ShowAll()
        {
            var listFilm = _context.Films.Select(f => f);
            return View(listFilm);
        }

        // GET: FilmController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FilmController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FilmController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name", "Time")] Film film)
        {
            try
            {
                _context.Films.Add(film);
                _context.SaveChanges();
                return RedirectToAction(nameof(ShowAll));
            }
            catch
            {
                return View();
            }
        }

        // GET: FilmController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FilmController/Edit/5
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

        // GET: FilmController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FilmController/Delete/5
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
