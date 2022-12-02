using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

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
        public ActionResult Create([BindAttribute("Name", "AmountDefault")] Cinema cinema)
        {
            try
            {
                _context.Cinemas.Add(cinema);
                _context.SaveChanges();
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
            var existCinema = _context.Cinemas.FirstOrDefault(c => c.ID == id);
            if (existCinema != null)
            {
                return View(existCinema);
            } else
            {
                return NotFound();
            }
            
        }

        // POST: CinemaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [BindAttribute("Name", "AmountDefault")] Cinema cinema)
        {
            try
            {
                var existCinema = _context.Cinemas.FirstOrDefault(c => c.ID == id);
                if (existCinema != null)
                {
                    existCinema.Name = cinema.Name;
                    existCinema.AmountDefault = cinema.AmountDefault;
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(ShowAll));
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
