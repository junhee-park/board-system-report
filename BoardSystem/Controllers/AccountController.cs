using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardSystem.DataContext;
using BoardSystem.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BoardSystem.Controllers
{
    public class AccountController : Controller
    {
        // GET: /<controller>/
        public IActionResult Login()
        {
            return View();
        }

        
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User model)
        {
            if(ModelState.IsValid)
            {
                using (var db = new BoardSystemContext())
                {
                    db.Users.Add(model);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
