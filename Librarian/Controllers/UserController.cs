using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DAL;
using DataModels;
using System.IO;
using PagedList;



namespace Librarian.Controllers
{
    public class UserController : Controller
    {
        //HttpCookie ck = new HttpCookie("myFile");
        LibrarianEntities db = new LibrarianEntities();
        UserRepository rebo = new UserRepository();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public JsonResult AddAccount(UserVM User)
        {

            if (!db.User.Select(x => x.E_mail).Contains(User.E_mail))
            {
                rebo.Add(User);
                //start cookies:let the login available for 2 months.
                //ck["id"] = db.User.Where(x => x.E_mail == User.E_mail).Select(x => x.UserID).FirstOrDefault().ToString();
                //ck.Expires.AddMonths(2);
                //Response.Cookies.Add(ck);
                //end cookies
                Session["UserData"] = db.User.Where(x => x.E_mail == User.E_mail).Select(x => x.UserID).FirstOrDefault();
                Session.Timeout = 15;
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
                return Json("error", JsonRequestBehavior.AllowGet);

        }
        public JsonResult EnterAccount(UserVM user)
        {
            if (db.User.Select(x => x.E_mail).Contains(user.E_mail))
            {
                if (db.User.Where(x => x.E_mail == user.E_mail && x.Password == user.Password).FirstOrDefault() != null)
                {
                    //start cookies:let the login available for 2 months.
                    //ck["id"] = db.User.Where(x => x.E_mail == user.E_mail).Select(x => x.UserID).FirstOrDefault().ToString();
                    //ck.Expires.AddMonths(2);
                    //Response.Cookies.Add(ck);
                    //end cookies
                    Session["UserData"] = db.User.Where(x => x.E_mail == user.E_mail).Select(x => x.UserID).FirstOrDefault();
                    Session.Timeout = 15;
                    var id = db.User.Where(x => x.E_mail == user.E_mail).Select(x => x.UserID).FirstOrDefault();
                    return Json(id, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //wrong password
                    return Json("error", JsonRequestBehavior.AllowGet);
                }
            }
            else
                //wrong email
                return Json("failed", JsonRequestBehavior.AllowGet);

        }
        public ActionResult Home(int? BookID , int? UserID , string sortOrder, string currentFilter, string searchString , int? page)
        {
            ViewBag.CurrentSort = sortOrder;
      
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var books = db.Book.OrderBy(x => x.BookName).ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.BookName.Contains(searchString)).OrderBy(x => x.BookName).ToList();
            }
            if (BookID == null)
            {
                ViewBag.books = books.ToList();
                ViewBag.authors = db.Author.ToList();
                ViewBag.categories = db.Category.ToList();
                ViewBag.bookparts = db.Book_Parts.ToList();
                TempData["userinfo"] = db.User.Where(x => x.UserID == UserID).FirstOrDefault();
                int pageSize = 3;
                int pageNumber = (page ?? 1);
                return View(books.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                ViewBag.bookparts = db.Book_Parts.Where(x=>x.BookID==BookID).ToList();
                ViewBag.authors = db.Author.ToList();
                TempData["userinfo"] = db.User.Where(x => x.UserID == UserID).FirstOrDefault();
            }

            return View();
        }

       
        public ActionResult BookDetails(int? BookID , int? UserID, int? AuthorID)
        {
            if (AuthorID != null)
            {
                var b = db.Book.Where(x => x.BookID == BookID).Select(x => x).FirstOrDefault();
                ViewBag.book = b;
                ViewBag.author = db.Author.Where(x => x.AuthorID == b.AuthorID).Select(x => x).FirstOrDefault();
                ViewBag.category = db.Category.Where(x => x.CategoryID == b.CategoryID).Select(x => x).FirstOrDefault();
                TempData["userinfo"] = db.User.Where(x => x.UserID == UserID).FirstOrDefault(); 

            }
            else
            {
                var b = db.Book_Parts.Where(x => x.Book_PartID == BookID).Select(x => x).FirstOrDefault();
                ViewBag.book = b;
                ViewBag.author = db.Author.Where(x => x.AuthorID == b.AuthorID).Select(x=>x).FirstOrDefault();
                ViewBag.category = db.Category.Where(x => x.CategoryID == db.Book.Where(c => c.BookID == b.BookID).Select(c => c.CategoryID).FirstOrDefault()).Select(x => x).FirstOrDefault();
                TempData["userinfo"] = db.User.Where(x => x.UserID == UserID).FirstOrDefault(); 

            }

            return View();
        }

        public ActionResult UserProfile(int? UserID)
        {
            var user = db.User.Where(x => x.UserID == UserID).FirstOrDefault();
            var books = db.User_Books.Where(x => x.UserID == UserID && x.IsBought == true).Select(x=>x.BookID).ToList();
            var bookparts = db.User_Books.Where(x => x.UserID == UserID && x.IsBought == true).Select(x => x.Book_PartID).ToList();
            TempData["userinfo"] = user;
            List<Book> AllBooks = new List<Book>();
            List<Book_Parts> AllBookParts = new List<Book_Parts>();
            if (books !=null)
            {
                foreach (var item in db.Book)
                {
                    foreach (var x in books)
                    {
                        if (item.BookID == x)
                        {
                            AllBooks.Add(item);
                        }
                    }
                }
                ViewBag.allbooks = AllBooks;
                ViewBag.Null1 = true;
            }
            else
            {
                ViewBag.Null1 = false;
            }
            if (books != null)
            {
                foreach (var item in db.Book_Parts)
                {
                    foreach (var x in bookparts)
                    {
                        if (item.Book_PartID == x)
                        {
                            AllBookParts.Add(item);
                        }
                    }
                }
                ViewBag.allbookparts = AllBookParts;
                ViewBag.Null2 = true;
            }
            else
            {
                ViewBag.Null2 = false;
            }
            return View();
        }
        public ActionResult SaveProfileImage(int id)
        {

            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];
                HttpPostedFileBase filebase = new HttpPostedFileWrapper(pic);
                var fileName = Path.GetFileName(filebase.FileName);
                var fileUrl = Server.MapPath("~/ProfilesImg/");
                string extension = Path.GetExtension(filebase.FileName);
                string newFileName = Guid.NewGuid() + extension;
                var path = Path.Combine(fileUrl, newFileName);
                filebase.SaveAs(path);
                var user = db.User.FirstOrDefault(x => x.UserID == id);
                user.Image = newFileName;
                db.SaveChanges();
                return Json("File Saved Successfully.");
            }
            else { return Json("No File Saved."); }
        }

        public ActionResult LogOut(int? UserID)
        {
            Session["UserData"] = null;
            Session.Abandon();
            return RedirectToAction("Home", new { UserID = UserID});
        }

        public ActionResult Cart(int? UserID)
        {
            var user = db.User.Where(x => x.UserID == UserID).FirstOrDefault();
            TempData["userinfo"] = user;
            return View();
        }
        [HttpPost]
        public ActionResult SaveCart(User_Books[] cart)
        {
            if (cart != null)
            {
                User_Books B = new User_Books();
                foreach (var item in cart)
                {
                    B.UserID = item.UserID;
                    B.HasParts = item.HasParts;
                    B.BookNums = item.BookNums;
                    B.TotalPrice = (item.BookNums * item.TotalPrice);
                    B.IsBought = true;
                    B.Bought_Date = DateTime.Now;
                    if (item.HasParts == false)
                    {
                        B.BookID = item.BookID;
                        B.Book_PartID = null;
                    }
                    else
                    {
                        B.Book_PartID = item.BookID;
                        B.BookID = null;
                    }
                    db.User_Books.Add(B);
                    db.SaveChanges();
                }
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error", JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult CancelBought(int? BookID, int? UserID, int? AuthorID)
        {
            if (AuthorID != null)
            {
                var row = db.User_Books.FirstOrDefault(x => x.UserID == UserID && x.BookID == BookID && x.IsBought==true);
                row.IsBought = false;
                db.SaveChanges();
                return RedirectToAction("UserProfile", new { UserID = UserID });
            }
            else
            {
                var row = db.User_Books.FirstOrDefault(x => x.UserID == UserID && x.Book_PartID == BookID && x.IsBought == true);
                row.IsBought = false;
                db.SaveChanges();
                return RedirectToAction("UserProfile", new { UserID = UserID });
            }
        }

    }
}