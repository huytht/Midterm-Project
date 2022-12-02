using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Midterm_Project.Controllers
{
    public class FilmScheduleController : Controller
    {
        private readonly DataContext _context;
        public FilmScheduleController(DataContext context)
        {
            _context = context;
        }
        // GET: HomeController1
        public ActionResult ShowAll()
        {
            var list = _context.FilmSchedules.Include("Film").Include("Cinema").Select(f => f);
            return View(list);
        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            IEnumerable<SelectListItem> listFilm = _context.Films.Select(f => new SelectListItem { Text = f.Name, Value = f.ID.ToString() });
            IEnumerable<SelectListItem> listCinema = _context.Cinemas.Select(c => new SelectListItem { Text = c.Name, Value = c.ID.ToString() });
            ViewBag.FilmID = listFilm;
            ViewBag.CinemaID = listCinema;
            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("FilmID, CinemaID, PremiereTime")] FilmSchedule schedule)
        {
            try
            {
                schedule.AmountEmpty = _context.Cinemas.FirstOrDefault(c => c.ID == schedule.CinemaID).AmountDefault;
                _context.FilmSchedules.Add(schedule);
                _context.SaveChanges();
                return RedirectToAction(nameof(ShowAll));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController1/Edit/5
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

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController1/Delete/5
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
