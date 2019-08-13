using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http; // don't forget this for session 
using Microsoft.AspNetCore.Mvc;
using DojoDachi.Models;

namespace DojoDachi.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index(){
            if(HttpContext.Session.GetInt32("happiness") == null){
                HttpContext.Session.SetInt32("happiness", 20);
                HttpContext.Session.SetInt32("fullness", 20);
                HttpContext.Session.SetInt32("meals", 3);
                HttpContext.Session.SetInt32("energy", 50);
            }
            ViewBag.happy = HttpContext.Session.GetInt32("happiness");
            ViewBag.full = HttpContext.Session.GetInt32("fullness");
            ViewBag.meals = HttpContext.Session.GetInt32("meals");
            ViewBag.energy = HttpContext.Session.GetInt32("energy");

            if(ViewBag.happy == 0 || ViewBag.full == 0){
                ViewBag.message = "You lose...((( and Yor Cat no longer with us...";
                ViewBag.restart = true;
            }
            else if(ViewBag.happy >= 100 && ViewBag.full >=100 && ViewBag.energy >= 100){
                ViewBag.message = "Your Won!";
                ViewBag.restart = true;
            }
            else{
                ViewBag.message = TempData["message"];
            }
            return View("Index");
        }
        [HttpGet("/feed")]
        public IActionResult Feed(){
            int meals = (int)HttpContext.Session.GetInt32("meals");
            if( meals > 0){
                int fullness = (int)HttpContext.Session.GetInt32("fullness");
                Random rand = new Random();
                int randNum = rand.Next(5,11);
                int chanceNoLike = rand.Next(4);
                if(chanceNoLike != 1){
                    fullness += randNum;
                    TempData["message"] = $"Consumed one meal to gain {randNum} fullness";
                }else{
                    TempData["message"] = "Your Cat did not like that!(((";
                }
                --meals;
                HttpContext.Session.SetInt32("meals", meals);
                HttpContext.Session.SetInt32("fullness", fullness);
                TempData["message"] = $"Consumed one meal to gain {randNum} fullness";
            }else{
                TempData["message"] = "You don't have meals left";
            }
            return RedirectToAction("Index");
        }
        [HttpGet("/play")]
        public IActionResult Play(){
            int energy = (int)HttpContext.Session.GetInt32("energy");
            int happy = (int)HttpContext.Session.GetInt32("happiness");
            if(energy>=5){
                Random rand = new Random();
                int randhappy = rand.Next(5,11);
                int chanceNoLike = rand.Next(4);
                    if(chanceNoLike !=1){
                        happy += randhappy;
                        TempData["message"] = $"You played with your Cat: +{randhappy} happiness, -5 energy";
                    }
                    else{
                        TempData["message"] = "Your Cat did not like that!(((";
                    }
                    energy -=5;
                    HttpContext.Session.SetInt32("energy", energy);
                    HttpContext.Session.SetInt32("happiness", happy);
                }else{
                    TempData["message"] = "Your Cat does not have enought energy to play";
                }
            return RedirectToAction("Index");
        }
        [HttpGet("/work")]
        public IActionResult Work(){
            int energy = (int)HttpContext.Session.GetInt32("energy");
            int meals = (int)HttpContext.Session.GetInt32("meals");
            Random rand = new Random();    
            if(energy >= 5){
                energy -=5;
                meals +=rand.Next(1,4);
                TempData["message"] = $"Your Cat is working, all mouses in 5 miles radius are destroyed! Ern {meals} meals!";
            }
            else{
                TempData["message"] = "Your Cat does not have enough energy to work";
            }
            HttpContext.Session.SetInt32("energy", energy);
            HttpContext.Session.SetInt32("meals", meals);
            return RedirectToAction("Index");
        }
        [HttpGet("/sleep")]
        public IActionResult Sleep(){
            int energy = (int)HttpContext.Session.GetInt32("energy");
            int happy = (int)HttpContext.Session.GetInt32("happiness");
            int fullness = (int)HttpContext.Session.GetInt32("fullness");
            energy +=15;
            happy -=5;
            fullness -=5;
            TempData["message"] = "Your cat sleep enough to get 15 points of energy, but lost 5 points of happiness and fulness.";
            HttpContext.Session.SetInt32("energy", energy);
            HttpContext.Session.SetInt32("happiness", happy);
            HttpContext.Session.SetInt32("fullness", fullness);
            return RedirectToAction("Index");
        }
        [HttpGet("restart")]
        public IActionResult Restart(){
            HttpContext.Session.Clear();
            TempData["message"] = "";
            return RedirectToAction("Index");
        }
        
    }
}
