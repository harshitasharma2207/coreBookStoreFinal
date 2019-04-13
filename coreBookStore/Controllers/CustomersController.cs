using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreBookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace coreBookStore.Controllers
{
    public class CustomersController : Controller
    {
        BookStoreDbContext context = new BookStoreDbContext();
        public IActionResult Index()
        {
            var cust = context.Customers.ToList();
            return View(cust);
        }

        public IActionResult Details(int id)
        {
            Customer cust = context.Customers.Where(x => x.CustomerId == id).SingleOrDefault();
            context.SaveChanges();
            return View(cust);
        }
    }
}