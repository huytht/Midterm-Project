using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Midterm_Project.Controllers
{
    public class OrderController : Controller
    {
        private readonly DataContext _context;
        public OrderController(DataContext context)
        {
            _context = context;
        }
        // GET: HomeController1
        // GET: OrderController
        public ActionResult ShowAll(int? scheduleId)
        {
            IEnumerable<Order> list;

            if (scheduleId == null)
            {
                list = _context.Orders.Select(o => o).Include("FilmSchedule").Include("FilmSchedule.Film").Include("FilmSchedule.Cinema");
            } else
            {
                list = _context.Orders.Where(o => o.FilmScheduleID == scheduleId).Include("FilmSchedule").Include("FilmSchedule.Film").Include("FilmSchedule.Cinema");
            }
            ViewBag.OrderList = list;
            
            ViewBag.FilmScheduleID = new[] { new SelectListItem { Text = "Vui lòng chọn lịch chiếu", Value = "-1" } }.Concat(
                    _context.FilmSchedules.Select(f => new SelectListItem { Text = f.Film.Name + "-" + f.Cinema.Name + "-" + f.PremiereTime.ToString(), Value = f.ID.ToString() })
                );
            
            return View(list);
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            IEnumerable<SelectListItem> listSchedule = _context.FilmSchedules.Select(f => new SelectListItem { Text = f.Film.Name + "-" + f.Cinema.Name + "-" + f.PremiereTime.ToString(), Value = f.ID.ToString() });
            ViewBag.FilmScheduleID = listSchedule;
            
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("PhoneNumber, FilmScheduleID, AmountTicket")] Order order)
        {
            try
            {
                //ViewBag.MaxTicket = _context.FilmSchedules.FirstOrDefault(f => f.ID == order.FilmScheduleID).AmountEmpty;
                var filmScheduleTicketRemain = _context.FilmSchedules.FirstOrDefault(f => f.ID == order.FilmScheduleID).AmountEmpty;
                var existFilmSchedule = _context.FilmSchedules.FirstOrDefault(f => f.ID == order.FilmScheduleID);
                if (order.AmountTicket <= filmScheduleTicketRemain)
                {
                    existFilmSchedule.AmountEmpty -= order.AmountTicket;
                    _context.Orders.Add(order);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(ShowAll));
                } else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
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

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
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
