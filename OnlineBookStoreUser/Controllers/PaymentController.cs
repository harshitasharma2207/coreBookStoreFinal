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

            Payment payment = new Payment()
            {
                StripePaymentId = charge.PaymentMethodId,
                PaymentAmount = 500,

                DateOfPayment = System.DateTime.Now,
                PaymentDescription = "Payment Initiated..",
                CardLastDigit = Convert.ToInt32(charge.PaymentMethodDetails.Card.Last4),

                CustomerId = 1,
                OrderId = 2
            };

            context.Add<Payment>(payment);

            context.Payment.Add(payment);
            context.SaveChanges();

            return RedirectToAction("Invoice", "Cart");
        }

    }
}