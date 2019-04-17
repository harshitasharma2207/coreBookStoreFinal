using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreBookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace coreBookStore.Controllers
{
    public class OrdersController : Controller
    {
        BookStoreDbContext context = new BookStoreDbContext();
        public IActionResult Index()
        {
            var od = context.Orders.ToList();
            return View(od);
            
        }
        //public IActionResult Details(int id)
        //{
        //    Order od = context.Orders.Where(x => x.OrderId == id).SingleOrDefault();
        //    context.SaveChanges();
        //    return View(od);
        //}

        public IActionResult Details(int id)
        {
            List<OrderBook> ob = new List<OrderBook>();
            List<Book> books = new List<Book>();
            ob = context.OrderBooks.Where(x => x.OrderId == id).ToList();
            foreach (var item in ob)
            {
                Book c = context.Books.Where(x => x.BookId == item.BookId).SingleOrDefault();
                books.Add(c);
            }
            ViewBag.bookDetail = books;
            return View();
        }
       
    }
}