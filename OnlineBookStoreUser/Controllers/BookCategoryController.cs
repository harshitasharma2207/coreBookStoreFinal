using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookStoreUser.Helper;
using OnlineBookStoreUser.Models;

namespace OnlineBookStoreUser.Controllers
{
    public class BookCategoryController : Controller
    {


        Book_Store_DbContext context = new Book_Store_DbContext();



        public IActionResult Index()
        {
            var bookCategory = context.BookCategories.ToList();
            return View(bookCategory);
        }


        public IActionResult Display(int id)
        {
            var book = context.Books.Where(x => x.BookCategoryId == id);
            return View(book);
        }


    }
}