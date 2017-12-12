using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WeddingPlanner.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Controllers
{
    public class WeddingController : Controller
    {

        private Context _context;
        private User ActiveUser 
        {
            get{ return _context.Users.Where(u => u.UserId == HttpContext.Session.GetInt32("id")).FirstOrDefault();}
        }
        public WeddingController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if(ActiveUser == null)
                return RedirectToAction("Index","Home");
            ViewBag.User = ActiveUser;
            Dashboard dashData = new Dashboard
            {
                Weddings = _context.Weddings.Include(w => w.Guests).ToList(),
                User = ActiveUser
            };
        return View(dashData);
        }

        public IActionResult Create()
        {
            if(ActiveUser == null)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public IActionResult Create(ViewWedding model)
        {
            if(ActiveUser == null)
                return RedirectToAction("Index", "Home");
            if(ModelState.IsValid)
            {
                Wedding wedding = new Wedding
                {
                    WedderOne = model.WedderOne,
                    WedderTwo = model.WedderTwo,
                    Date = model.Date,
                    Address = model.Address,
                    UserId = ActiveUser.UserId
                };
                _context.Weddings.Add(wedding);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Create");
        }

        public IActionResult Rsvp(int id)
        {
            if(ActiveUser == null)
                return RedirectToAction("Index", "Home");
            RSVP rsvp = new RSVP
            {
                UserId = ActiveUser.UserId,
                WeddingId = id
            };
            _context.Rsvps.Add(rsvp);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UnRsvp(int id)
        {
            if(ActiveUser == null)
                return RedirectToAction("Index", "Home");
            RSVP toDelete = _context.Rsvps.Where(r => r.WeddingId == id)
                                          .Where(r => r.UserId == ActiveUser.UserId)
                                          .SingleOrDefault();
            _context.Rsvps.Remove(toDelete);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Show(int id)
        {
            if(HttpContext.Session.GetInt32("id") == null)
               return RedirectToAction("Index");
            return View("Show",_context.Weddings.Include(w => w.Guests).ThenInclude(g => g.Guest).Where(w => w.WeddingId == id).SingleOrDefault());
        }

        public IActionResult Delete(int id)
        {
            if(ActiveUser == null)
                return RedirectToAction("Index", "Home");
            Wedding toDelete = _context.Weddings.SingleOrDefault(w => w.WeddingId == id);
            _context.Weddings.Remove(toDelete);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}