using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreBookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace coreBookStore.Controllers
{
    public class PaymentController : Controller
    {
        BookStoreDbContext context = new BookStoreDbContext();
        public IActionResult Index()
        {
            var pay = context.Payment.ToList();
            return View(pay);
        }
        
        public IActionResult Details(int id)
        {
            //List<Payment> pay = new List<Payment>();
            //List<Order> order = new List<Order>();
            //pay = context.Payment.Where(x => x.PaymentId == id).ToList();
            //foreach (var item in pay)
            //{
            //    Order c = context.Orders.Where(x => x.OrderId == item.OrderId).SingleOrDefault();
            //    order.Add(c);
            //}
            //ViewBag.paymentDetail = order;
            //return View();
            Payment pay = context.Payment.Where(x => x.PaymentId == id).SingleOrDefault();
            context.SaveChanges();
            return View(pay);

        }


    }
}