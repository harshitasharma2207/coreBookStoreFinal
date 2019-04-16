using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreBookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace coreBookStore.Controllers
{
    [Route("account")]

    public class AdminController : Controller
    {
        BookStoreDbContext context = new BookStoreDbContext();

        [Route("")]
            [Route("index")]
            [Route("~/")]
            [HttpGet]
            public IActionResult Index()
            {
                return View();
            }

        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Admin ad)
        {
            context.Admins.Add(ad);
            context.SaveChanges();

            return RedirectToAction("Login");
        }


        [Route("login")]
        [HttpPost]
        public IActionResult Login(int id, Admin admin)
        {
            var adminLogin = context.Admins.Where(x => x.AdminUserName == admin.AdminUserName && x.AdminPassword.Equals(admin.AdminPassword)).SingleOrDefault();
            if (adminLogin == null)
            {
                ViewBag.Error = "Invalid Credential";
                return View("Index");
            }
            else
            {
                if (adminLogin != null)
                {
                    HttpContext.Session.SetString("uname", admin.AdminUserName);
                    return View("Home");
                }
                else
                {
                    ViewBag.Error = "Invalid Credential";
                    return View("Index");
                }
            }


        }

        [Route("Home")]

        public IActionResult Home()
        {
          
            return View();
        }

        [Route("logout")]
            [HttpGet]
            public IActionResult Logout()
            {
                HttpContext.Session.Remove("uname");
                return RedirectToAction("Index");
            }

 
    }
}





//[Route("login")]
//public IActionResult Login()
//{
//    var custId = HttpContext.Session.GetString("cId");
//    if (custId != null)
//    {
//        int cId = int.Parse(custId);
//        return RedirectToAction("CheckOut", "Cart", new { @id = cId });
//    }
//    else
//    {
//        return View("Login");

//    }
//}
//[Route("login")]
//[HttpPost]
//public ActionResult Login(int id, Customers cust)
//{

//    var user = context.Customers.Where(x => x.UserName == cust.UserName && x.NewPassword.Equals(cust.NewPassword)).SingleOrDefault();
//    if (user == null)
//    {
//        ViewBag.Error = "Invalid Credential";
//        return View("Login");
//    }
//    else
//    {
//        int custId = user.CustomerId;
//        ViewBag.custName = cust.UserName;

//        if (user != null)
//        {

//            HttpContext.Session.SetString("uname", cust.UserName);
//            HttpContext.Session.SetString("id", user.CustomerId.ToString());

//            HttpContext.Session.SetString("cid", custId.ToString());

//            return RedirectToAction("CheckOut", "Cart", new { @id = custId });


//        }
//        else
//        {
//            ViewBag.Error = "Invalid Credential";
//            return View("Index");
//        }
//}
//    }



