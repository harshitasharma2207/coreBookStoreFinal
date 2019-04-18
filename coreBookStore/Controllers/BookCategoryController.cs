using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreBookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace coreBookStore.Controllers
{
    public class BookCategoryController : Controller
    {
        BookStoreDbContext context = new BookStoreDbContext();
        public ViewResult Index()
        {
            var caty = context.BookCategories.ToList();
            return View(caty);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind("BookCategoryName,BookCategoryDescription,BookCategoryImage")]BookCategory c1)
        {
            if (ModelState.IsValid)
            {
                context.BookCategories.Add(c1);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(c1);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            BookCategory caty = context.BookCategories.Find(id);

            return View(caty);
        }
        [HttpPost]
        public ActionResult Delete(int id, BookCategory c1)
        {
            var caty = context.BookCategories.Where(x => x.BookCategoryId == id).SingleOrDefault();
            context.BookCategories.Remove(caty);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            BookCategory caty = context.BookCategories.Where(x => x.BookCategoryId == id).SingleOrDefault();


            return View(caty);
        }
        [HttpPost]
        public ActionResult Edit([Bind("BookCategoryName,BookCategoryDescription,BookCategoryImage")]BookCategory c1)
        {
            if (ModelState.IsValid)
            {
                BookCategory caty = context.BookCategories.Where(x => x.BookCategoryId == c1.BookCategoryId).SingleOrDefault();
            context.Entry(caty).CurrentValues.SetValues(c1);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
         return View(c1);
        }
        public ActionResult Details(int id)
        {
            BookCategory caty = context.BookCategories.Where(x => x.BookCategoryId == id).SingleOrDefault();
            context.SaveChanges();
            return View(caty);
        }
    }
}