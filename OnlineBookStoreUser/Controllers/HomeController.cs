using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookStoreUser.Helper;
using OnlineBookStoreUser.Models;

namespace OnlineBookStoreUser.Controllers
{
    public class HomeController : Controller
    {
        Book_Store_DbContext context = new Book_Store_DbContext();
        public IActionResult Index()
        {
            ViewBag.bookcategoryAuthor = context.Books.Include(c => c.Author).ToList();
            ViewBag.bookcategoryPublication = context.Books.Include(p => p.Publication).ToList();
            ViewBag.bookcategoryBook = context.Books.Include(b => b.BookCategory).ToList();

            var books = context.Books.ToList();
            int i = 0;
            int j = 0;
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (cart != null)
            {
                foreach (var item in cart)
                {
                    i++;
                }
                if (i != 0)
                {
                    foreach (var item in cart)
                    {
                        j++;
                    }
                }
                HttpContext.Session.SetString("CartItem", i.ToString());
            }
            return View(books);

        }



        
    }
}