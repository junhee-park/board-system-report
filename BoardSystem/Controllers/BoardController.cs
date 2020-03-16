using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardSystem.DataContext;
using BoardSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                var list = db.Boards.OrderByDescending(s => s.BoardNum).ToList();
                return View(list);
            }
                
        }

        public IActionResult Detail(int boardNum)
        {
            if (HttpContext.Session.GetString("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            using (var db = new BoardSystemContext())
            {
                var board = db.Boards.FirstOrDefault(n => n.BoardNum.Equals(boardNum));
                db.Entry(board).Entity.BoardViews = board.BoardViews + 1;
                db.SaveChanges();
                var commentList = db.Comments.ToList().Where(d => d.BoardNum.Equals(boardNum));
                ViewBag.commentList = commentList;
                return View(board);
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

    
        public IActionResult Delete(int boardNum)
        {
            if (HttpContext.Session.GetString("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            using (var db = new BoardSystemContext())
            {
                var board = db.Boards.Find(boardNum);
                db.Boards.Remove(board);
                db.SaveChanges();
                return Redirect("Index");
            }
        }

        public IActionResult DeleteComment(int boardNum, int commentNum)
        {
            if (HttpContext.Session.GetString("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            using (var db = new BoardSystemContext())
            {
                var comment = db.Comments.Find(commentNum);
                db.Comments.Remove(comment);
                db.SaveChanges();
                return Redirect("Detail?boardNum=" + boardNum);
            }
        }

        [HttpPost]
        [ActionName("AddComment")]
        public IActionResult AddComment()
        {
            if (HttpContext.Session.GetString("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            string commentContents = Request.Form["CommentContents"].ToString();
            string userId = Request.Form["UserId"].ToString();
            int boardNum = int.Parse(Request.Form["BoardNum"].ToString());
            Comment comment = new Comment
            {
                BoardNum = boardNum,
                CommentContents = commentContents,
                UserId = userId
            };
            using (var db = new BoardSystemContext())
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return Redirect("Detail?boardNum="+ boardNum);
            }
        }

    }
}
