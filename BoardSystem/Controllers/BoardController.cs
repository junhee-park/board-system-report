using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardSystem.DataContext;
using BoardSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BoardSystem.Controllers
{
    public class BoardController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            using (var db = new BoardSystemContext())
            {
                var list = db.Boards.ToList();
                return View(list);
            }
                
        }

        public IActionResult Add()
        {
            if (HttpContext.Session.GetString("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Add(Board model)
        {
            if (HttpContext.Session.GetString("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            model.UserId = HttpContext.Session.GetString("USER_LOGIN_KEY");

            if (ModelState.IsValid)
            {
                using (var db = new BoardSystemContext())
                {
                    db.Boards.Add(model);

                    if(db.SaveChanges() > 0)
                    {
                        return Redirect("Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "投稿できません。");
            }
            return View(model);
        }

        public IActionResult Edit()
        {
            if (HttpContext.Session.GetString("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public IActionResult Delete()
        {
            if (HttpContext.Session.GetString("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
    }
}
