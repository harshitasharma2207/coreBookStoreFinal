using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookStoreUser.Models;

namespace OnlineBookStoreUser.Controllers
{
    public class BookController : Controller
    {
        Book_Store_DbContext context = new Book_Store_DbContext();


        public ActionResult Details(int id)
        {
            HttpContext.Session.SetString("bookId", id.ToString());
            Books bk = context.Books.Where(x => x.BookId == id).SingleOrDefault();
            context.SaveChanges();
            ViewBag.reviews = context.Review.ToList();
            return View(bk);
        }

        [HttpGet]
        public ViewResult Review()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Review(Review re)
        {
            if(HttpContext.Session.GetString("cid") != null || HttpContext.Session.GetString("bookId") != null)
            {
                re.CustomerId = Convert.ToInt32(HttpContext.Session.GetString("cid"));
                re.BookId = Convert.ToInt32(HttpContext.Session.GetString("bookId"));
            }
            context.Review.Add(re);
            context.SaveChanges();
           
            return RedirectToAction("Details","Book", new { @id = re.BookId });
        }

      
    }
}