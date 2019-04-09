using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineBookStoreUser.Models;

namespace OnlineBookStoreUser.Controllers
{
    public class AuthorController : Controller
    {
        Book_Store_DbContext context = new Book_Store_DbContext();



        public IActionResult Index()
        {
            var author = context.Authors.ToList();
            return View(author);
        }


        public IActionResult Display(int id)
        {
            var book = context.Books.Where(x => x.AuthorId == id);
            return View(book);
        }

    }
}