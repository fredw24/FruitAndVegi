using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FruitAndVegi.Models;
using FruitAndVeg.Models;

namespace FruitAndVegi.Controllers
{
    public class HomeController : Controller
    {
         private MyContext dbContext;

        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            List<FruitsAndVegetable> AllFruitsAndVeg = dbContext.FruitsAndVegetable
                .OrderByDescending(d => d.Created_At)
                .ToList();

            ViewBag.user = AllFruitsAndVeg;

            return View();
        }
        
        [Route("/new")]
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost("/new/create")]
        public IActionResult Create(FruitsAndVegetable fruitsAndVeg)
        {
            if (ModelState.IsValid)
            {           
                dbContext.Add(fruitsAndVeg);
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View("New");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetFood(int id){
            
            FruitsAndVegetable oneFood = dbContext.FruitsAndVegetable.FirstOrDefault(f => f.FruitsAndVegID == id);
            ViewBag.food = oneFood;

            return View("Display");

        }
        [HttpPost("{id}/delete")]
        public IActionResult DeleteFood(int id){
            FruitsAndVegetable RetrievedUser = dbContext.FruitsAndVegetable.SingleOrDefault(food => food.FruitsAndVegID == id);
            
            dbContext.FruitsAndVegetable.Remove(RetrievedUser);
 
            dbContext.SaveChanges();

            return RedirectToAction("Index");



        }

        [HttpPost("edit/{id}")]
        public IActionResult GetEdit(int id){
            FruitsAndVegetable oneFood = dbContext.FruitsAndVegetable.FirstOrDefault(food => food.FruitsAndVegID == id);
            ViewBag.edit = oneFood;
            return View("Edit");
        }

        [HttpPost("edit/{id}/edit")]
        public IActionResult Edit(int id, FruitsAndVegetable food)
        {
            if (ModelState.IsValid)
            {           
                FruitsAndVegetable RetrievedUser = dbContext.FruitsAndVegetable.FirstOrDefault(d => d.FruitsAndVegID == id);
                RetrievedUser.Name = food.Name;
                RetrievedUser.Type = food.Type;
                RetrievedUser.Tastiness = food.Tastiness;
                RetrievedUser.Calories = food.Calories;
                RetrievedUser.Description = food.Description;
                RetrievedUser.Updated_At = DateTime.Now;

                dbContext.SaveChanges();

                return RedirectToAction("GetFood", id);
            }
            else
            {
                FruitsAndVegetable oneFood = dbContext.FruitsAndVegetable.FirstOrDefault(d => d.FruitsAndVegID == id);
                ViewBag.edit = oneFood;
                return View("Edit");
            }
        }

    }
}
