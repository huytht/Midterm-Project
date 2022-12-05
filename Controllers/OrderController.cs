using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
        public IActionResult GetAllOrder(int? scheduleId)
        {
            IEnumerable<Order> list = null;
            if (scheduleId == null || scheduleId == 0)
            {
                list = _context.Orders.Select(o => o).Include("FilmSchedule").Include("FilmSchedule.Film").Include("FilmSchedule.Cinema");
            }
            else
            {
                list = _context.Orders.Where(o => o.FilmScheduleID == scheduleId).Include("FilmSchedule").Include("FilmSchedule.Film").Include("FilmSchedule.Cinema");
            }

            return Json(list);
        }
        public ActionResult ShowAll(int? scheduleId)
        {
            IEnumerable<Order> list;

            if (scheduleId == null || scheduleId == 0)
            {
                list = _context.Orders.Select(o => o).Include("FilmSchedule").Include("FilmSchedule.Film").Include("FilmSchedule.Cinema");
            } else
            {
                list = _context.Orders.Where(o => o.FilmScheduleID == scheduleId).Include("FilmSchedule").Include("FilmSchedule.Film").Include("FilmSchedule.Cinema");
            }
            ViewBag.OrderList = list;
            
            ViewBag.FilmScheduleID = new[] { new SelectListItem { Text = "Vui lòng chọn lịch chiếu", Value = "-1" }, new SelectListItem { Text = "Tất cả", Value = "0" } }.Concat(
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
            ViewBag.FilmScheduleID = new[] { new SelectListItem { Text = "Vui lòng chọn lịch chiếu", Value = "-1" } }.Concat(
                    _context.FilmSchedules.Select(f => new SelectListItem { Text = f.Film.Name + "-" + f.Cinema.Name + "-" + f.PremiereTime.ToString(), Value = f.ID.ToString() }));

            return View();
        }

        public int CountTicketRemain(int scheduleId)
        {
            var amount = _context.FilmSchedules.FirstOrDefault(f => f.ID == scheduleId).AmountEmpty;
            return amount;
        }
        public bool CheckCorrectAmount(int ticketRemain, int amountTicket)
        {
            return amountTicket <= ticketRemain;
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
                    ViewBag.FilmScheduleID = new[] { new SelectListItem { Text = "Vui lòng chọn lịch chiếu", Value = "-1" } }.Concat(
                    _context.FilmSchedules.Select(f => new SelectListItem { Text = f.Film.Name + "-" + f.Cinema.Name + "-" + f.PremiereTime.ToString(), Value = f.ID.ToString() }));
                    return View();
                } 

            }
            catch (Exception ex)
            {
                return Json(ex.Message);
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
