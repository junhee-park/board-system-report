using System.Linq;
using BoardSystem.DataContext;
using BoardSystem.Models;
using BoardSystem.ViewModel;
using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                using (var db = new BoardSystemContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.UserId.Equals(model.UserId) && u.UserPassword.Equals(model.UserPassword));
                    if(user != null)
                    {
                        
                        HttpContext.Session.SetString("USER_LOGIN_KEY", user.UserId);
                        return RedirectToAction("LoginSuccess", "Home");
                        
                    }
                    
                }
                
                ModelState.AddModelError(string.Empty, "ユーザーのIDもしくはパスワードが正しくありません。");
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("USER_LOGIN_KEY");
            return RedirectToAction("Index", "Home");
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
