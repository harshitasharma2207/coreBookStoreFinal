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

        [Route("login")]
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
           
            var user = context.Admins.Where(x => x.AdminUserName == username).SingleOrDefault();
            if (username == null)
            {

                ViewBag.Error = "Invalid Credential";
                return View("Index");
            }
            else
            {
                var userName = user.AdminUserName;
                var Password = user.AdminPassword;

                if (username != null && password != null && username.Equals(userName) && password.Equals(Password))
                {
                    HttpContext.Session.SetString("uname", username);
                    HttpContext.Session.SetString("id", user.AdminId.ToString());
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