using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookStoreUser.Helper;
using OnlineBookStoreUser.Models;
using Stripe;

namespace OnlineBookStoreUser.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
      

        Book_Store_DbContext context = new Book_Store_DbContext();


        [Route("index")]
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int i = 0;


            if (cart != null)
            {
                foreach (var item in cart)
                {
                    i++;
                }
                if (i != 0)
                {
                    ViewBag.cart = cart;
                    ViewBag.total = cart.Sum(item => item.Books.BookPrice * item.Quantity);
                    return View();
                }
            }

            return View("EmptyCart");


        }

        [Route("buy/{id}")]
        public IActionResult Buy(int id)
        {
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> booklist = new List<Item>();

                booklist.Add(new Item
                {
                    Books = context.Books.Find(id),
                    Quantity = 1
                });

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", booklist);
            }


            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item
                    {
                        Books = context.Books.Find(id),
                        Quantity = 1
                    });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            }


            return RedirectToAction("Index", "Home");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            int i = 0;
            foreach (var item in cart)
            {
                i++;
            }

            if (cart == null)
            {
                HttpContext.Session.Remove("cart");
                return View("EmptyCart");

            }
            return RedirectToAction("Index");
        }

        private int isExist(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Books.BookId.Equals(id))
                {
                    return i;
                }

            }
            return -1;
        }
        //[Route("details")]
        //public ActionResult Details(int id)
        //{
        //    Books bk = context.Books.Where(x => x.BookId == id).SingleOrDefault();
        //    context.SaveChanges();
        //    return View(bk);
        //}

        [Route("checkout/{id}")]
        [HttpGet]
        public IActionResult CheckOut(int id)
        {
            int i = 0;
            ViewBag.i = i;

            var customers = context.Customers.Where(x => x.CustomerId == id).SingleOrDefault();

            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (cart == null)
            {
                return RedirectToAction("Index", "Home");
            }

            else
            {

                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Books.BookPrice * item.Quantity);
                TempData["total"] = ViewBag.total;
                TempData["cid"] = id;
                return View(customers);
            }
        }
        [Route("checkout/{id}")]
        [HttpPost]
        public IActionResult CheckOut(int id, Customers c, string stripeEmail, string stripeToken)
        {
            // var cid = (TempData["cid"]).ToString();
            //context.Customers.Add(c);
            //context.SaveChanges();

            Orders ord = new Orders()
            {
                OrderAmount = Convert.ToSingle(TempData["total"]),
                OrderDate = DateTime.Now,
                CustomerId = id
            };
            context.Orders.Add(ord);
            context.SaveChanges();
            //    return RedirectToAction("Payment");
            //}
            //return View(customers);

            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            List<OrderBooks> orderBooks = new List<OrderBooks>();
            for (int i = 0; i < cart.Count; i++)
            {
                OrderBooks orderBook = new OrderBooks()
                {
                    OrderId = ord.OrderId,
                    BookId = cart[i].Books.BookId,
                    Quantity = cart[i].Quantity
                };
                orderBooks.Add(orderBook);
            }
            orderBooks.ForEach(n => context.OrderBooks.Add(n));
            context.SaveChanges();
            TempData["cust"] = id;
            //return RedirectToAction("Index", "Payment");


            var customers = new CustomerService();
            var charges = new ChargeService();
            var Amount = TempData["total"];
            var order = ord.OrderId;
            var custmr = TempData["cust"];
            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                SourceToken = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = 500,
                Description = "Total Charge",
                Currency = "usd",
                CustomerId = customer.Id
            });

            Payment payment = new Payment();
            {
                payment.StripePaymentId = charge.PaymentMethodId;
                payment.PaymentAmount = Convert.ToInt32(Amount);
                payment.DateOfPayment = System.DateTime.Now;
                payment.PaymentDescription = "Payment Initiated";
                payment.CardLastDigit = Convert.ToInt32(charge.PaymentMethodDetails.Card.Last4);
                payment.OrderId = Convert.ToInt32(order);
                payment.CustomerId = Convert.ToInt32(custmr);
            }

            //_context.Add<Payments>(payment);
            context.Payment.Add(payment);
            context.SaveChanges();

            return RedirectToAction("Invoice", "Cart");




        }
        public IActionResult PaymentIndex()
        {
            var checkout = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.checkout = checkout;
            ViewBag.total = checkout.Sum(item => item.Books.BookPrice * item.Quantity);
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }


        //return View(customers);

        [Route("emptycart")]
        [HttpGet]

        public IActionResult EmptyCart()
        {
            return View();
        }
        [Route("invoice")]
        [HttpGet]
        public IActionResult Invoice()
        {
            int CustId = int.Parse(TempData["cust"].ToString());
            Customers customers = context.Customers.Where(x => x.CustomerId == CustId).SingleOrDefault();
            ViewBag.Customers = customers;
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Books.BookPrice * item.Quantity);
            TempData["total"] = ViewBag.total;
            HttpContext.Session.Remove("cart");
            HttpContext.Session.Remove("CartItem");
            return View();


        }

        [Route("Plus")]
        [HttpGet]
        public IActionResult Plus(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            if (index != -1)
            {
                cart[index].Quantity++;

            }

            else
            {
                cart.Add(new Item
                {
                    Books = context.Books.Find(id),
                    Quantity = 1
                });
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }
        [Route("Minus")]
        [HttpGet]
        public IActionResult Minus(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            if (index != -1)
            {
                if (cart[index].Quantity != 1)
                {
                    cart[index].Quantity--;
                }
                else
                    return RedirectToAction("Remove", "Cart", new { @id = id });
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        [Route("search")]
        [HttpGet]
        public IActionResult Search(string search)
        {
            ViewBag.Book = context.Books.Where(x => x.BookName == search || x.Author.AuthorName == search || x.BookCategory.BookCategoryName == search || x.Publication.PublicationName == search || search == null).ToList();
            return View(context.Books.Where(x => x.BookName == search || x.Author.AuthorName == search || x.BookCategory.BookCategoryName == search || x.Publication.PublicationName == search || search == null).ToList());
        }



    }
}