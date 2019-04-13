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
        public IActionResult Details(int id)
        {
            Order od = context.Orders.Where(x => x.OrderId == id).SingleOrDefault();
            context.SaveChanges();
            return View(od);
        }
        public IActionResult OrderBookDetails()
        {
            
            return View();
        }
    }
}