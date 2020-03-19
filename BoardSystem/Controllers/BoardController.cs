using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardSystem.DataContext;
using BoardSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
                List<Board> list;
                try
                {
                    list = getList(0);
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
                
                return View(list);
            }
                
        }

        public JsonResult Paging(int page)
        {
            List<Board> list = null;
            list = getList(page);
           
            JArray json = new JArray();
            foreach (Board board in list)
            {
                String jori = "{";
                jori += "\"No\":\"" + board.BoardNum + "\",";
                jori += "\"Title\":\"" + board.BoardTitle + "\",";
                jori += "\"User\":\"" + board.UserId + "\",";
                jori += "\"Date\":\"" + board.BoardDate + "\",";
                jori += "\"Views\":\"" + board.BoardViews + "\"";
                jori += "}";
                JObject jtmp = JObject.Parse(jori);
                json.Add(jtmp);
            }

            return new JsonResult(json);
            
        }

        private List<Board> getList(int page)
        {
            using (var db = new BoardSystemContext())
            {
                List<Board> list;
                if (page == 0)
                {
                    list = db.Boards.OrderByDescending(s => s.BoardNum).Take(20).ToList();
                }
                else
                {
                    list = db.Boards.OrderByDescending(s => s.BoardNum).Skip(20 + (page - 1) * 10).Take(10).ToList();
                }

                return list;
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
                Board board;
                IEnumerable<Comment> commentList;
                try
                {
                    board = db.Boards.FirstOrDefault(n => n.BoardNum.Equals(boardNum));
                    db.Entry(board).Entity.BoardViews = board.BoardViews + 1;
                    db.SaveChanges();
                    commentList = db.Comments.ToList().Where(d => d.BoardNum.Equals(boardNum));
                    ViewBag.commentList = commentList;
                } catch {
                    return RedirectToAction("Error", "Home");
                }
                
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
                    try
                    {
                        db.Boards.Add(model);

                        if (db.SaveChanges() > 0)
                        {
                            return Redirect("Index");
                        }
                    }
                    catch
                    {
                        return RedirectToAction("Error", "Home");
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
                Board board;
                try
                {
                    board = db.Boards.Find(boardNum);
                    db.Boards.Remove(board);
                    db.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
                
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
                Comment comment;
                try
                {
                    comment = db.Comments.Find(commentNum);
                    db.Comments.Remove(comment);
                    db.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
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
                try
                {
                    db.Comments.Add(comment);
                    db.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
                
                return Redirect("Detail?boardNum="+ boardNum);
            }
        }

    }
}
