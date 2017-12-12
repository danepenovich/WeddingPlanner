using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {

        private Context _context;
        private User ActiveUser 
        {
            get{ return _context.Users.Where(u => u.UserId == HttpContext.Session.GetInt32("id")).FirstOrDefault();}
        }
 
        public HomeController(Context context)
        {
            _context = context;
        }
       
        public IActionResult Index()
        {
            if(ActiveUser != null)
            {
                return RedirectToAction("Index","Wedding");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Register(NewUser model)
        {
            PasswordHasher<NewUser> hasher = new PasswordHasher<NewUser>();
            if(_context.Users.Where(u => u.Email == model.Email).SingleOrDefault() != null)
            {
                ModelState.AddModelError("Email", "That email is already in use.");
            }
            if(ModelState.IsValid)
            {
                User NewUser = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PW = hasher.HashPassword(model, model.PW),
                };
                _context.Users.Add(NewUser);
                _context.SaveChanges();
            }
            return View("Index");
        }

        [HttpPost]
        public IActionResult Login(LogUser model)
        {
            PasswordHasher<LogUser> hasher = new PasswordHasher<LogUser>();
            User userToLog = _context.Users.Where(u => u.Email == model.LogEmail).SingleOrDefault();
            if(userToLog == null)
            {
                ModelState.AddModelError("LogEmail", "Invalid Email");
            }
            else if(hasher.VerifyHashedPassword(model, userToLog.PW, model.LogPW) == 0)
            {
                ModelState.AddModelError("LogPW", "Invalid Password");
            }
            if(!ModelState.IsValid)
                return View("Index");
            HttpContext.Session.SetInt32("id", userToLog.UserId);
            return RedirectToAction("Index","Wedding");
        }
        
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
