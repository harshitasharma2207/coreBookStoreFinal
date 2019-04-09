using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineBookStoreUser.Models;

namespace OnlineBookStoreUser.Controllers
{
    public class PublicationController : Controller
    {
        Book_Store_DbContext context = new Book_Store_DbContext();

        public IActionResult Index()
        {
            var publication = context.Publications.ToList();
            return View(publication);
        }


        public IActionResult Display(int id)
        {
            var book = context.Books.Where(x => x.PublicationId == id);
            return View(book);
        }
    }
}