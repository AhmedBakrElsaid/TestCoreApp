﻿using Microsoft.AspNetCore.Mvc;
using TestCoreApp.Data;
using TestCoreApp.Models;

namespace TestCoreApp.Controllers
{
    public class ItemsController : Controller
    {
        public ItemsController(AppDbContext db)
        {
            _db = db;
        }

        private readonly AppDbContext _db;
        public IActionResult Index()
        {
            IEnumerable<Item> itemsList = _db.Items.ToList();
            return View(itemsList);
        }

        //GET
        public IActionResult New ()
        {
            return View();
        }

        //POST 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New (Item item)
        {
            if(item.Name == "100")
            {
                ModelState.AddModelError("Name", "Name can't equal 100");
            }
            if (ModelState.IsValid)
            {
                _db.Items.Add(item);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }

        }
    }
}
