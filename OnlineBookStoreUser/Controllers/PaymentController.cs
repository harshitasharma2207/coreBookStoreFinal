using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineBookStoreUser.Models;
using Stripe;

namespace OnlineBookStoreUser.Controllers
{
    public class PaymentController : Controller
    {
        Book_Store_DbContext context = new Book_Store_DbContext();

        private readonly Book_Store_DbContext _context;

        public PaymentController(Book_Store_DbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Charge(string stripeEmail, string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();
            var Amount = TempData["total"];
            var order = TempData["orderId"];
            var custmr = TempData["cust"];
            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                SourceToken = stripeToken
            });
            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = 500,
                Description = "Sample Charge",
                Currency = "usd",
                CustomerId = customer.Id
            });

            Payment payment = new Payment();
            {
                payment.StripePaymentId = charge.PaymentMethodId;
                payment.PaymentAmount = Convert.ToInt32(Amount);
                payment.DateOfPayment = System.DateTime.Now;
                payment.CardLastDigit = Convert.ToInt32(charge.PaymentMethodDetails.Card.Last4);
                payment.OrderId = Convert.ToInt32(order);
                payment.CustomerId = Convert.ToInt32(custmr);
            };

            _context.Add<Payment>(payment);

            _context.Payment.Add(payment);
            _context.SaveChanges();

            return RedirectToAction("Invoice", "Cart");
        }

    }
}