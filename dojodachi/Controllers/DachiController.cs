using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Newtonsoft.Json;

namespace dojodachi
{
    public class DachiController: Controller {
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetObjectFromJson<Dachi>("Dachi") == null) {
                HttpContext.Session.SetObjectAsJson("Dachi", new Dachi());
            };
            ViewBag.Dachi = HttpContext.Session.GetObjectFromJson<Dachi>("Dachi");
            ViewBag.Message = "I'm your new DojoDachi! Take care of me!";
            ViewBag.Playing = "true";
            ViewBag.Reaction = "cute";
            ViewBag.Error = "false";
            return View();
        }

        [Route("restart")]
        [HttpGet]
        public IActionResult Restart()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [Route("process")]
        [HttpPost]
        public IActionResult Process(string action)
        {
            Dachi getdachi = HttpContext.Session.GetObjectFromJson<Dachi>("Dachi");
            Random rand = new Random();
            ViewBag.Playing = "true";
            ViewBag.Error = "false";            
            switch(action) {
                case "feed":
                    if (getdachi.Meals > 0) {
                        getdachi.Meals -= 1;
                        if (rand.Next(0,4) > 0 ) {
                            getdachi.Fullness += rand.Next(5,11);
                            ViewBag.Reaction = "happy";
                            ViewBag.Message = "Fed."; 
                        } else {
                            ViewBag.Reaction = "meh";
                            ViewBag.Message = "Did not like the food."; 
                        }
                    } else {
                        ViewBag.Reaction = "confused";
                        ViewBag.Message = "Not enough meals.";
                        ViewBag.Error = "true";
                    }
                    break;
                case "play":
                    if (getdachi.Energy >= 5 ) {
                        getdachi.Energy -= 5;
                        if (rand.Next(0,4) > 0) {
                            getdachi.Happiness += rand.Next(5,11);
                            ViewBag.Reaction = "heart";
                            ViewBag.Message = "Played.";
                        } else {
                            ViewBag.Reaction = "meh";
                            ViewBag.Message = "Did not want to play.";
                        }
                    } else {
                        ViewBag.Reaction = "confused";
                        ViewBag.Message = "Not enough energy.";
                        ViewBag.Error = "true";
                    }
                    break;
                case "work":
                    if (getdachi.Energy >= 5) {
                        getdachi.Energy -= 5;
                        getdachi.Meals += rand.Next(1,4);
                        ViewBag.Reaction = "crying";
                        ViewBag.Message = "Worked.";
                    } else {
                        ViewBag.Reaction = "confused";
                        ViewBag.Message = "Not enough energy.";
                        ViewBag.Error = "true";
                    }
                    break;
                case "sleep":
                    getdachi.Energy += 15;
                    getdachi.Happiness -= 5;
                    getdachi.Fullness -= 5;
                    ViewBag.Reaction = "sleep";
                    ViewBag.Message = "Slept.";
                    break;
                default:
                    ViewBag.Reaction = "sad";
                    ViewBag.Message = "What did you tell me to do?";
                    break;
            }
            if(getdachi.Fullness < 1 || getdachi.Happiness < 1)
            {
                ViewBag.Reaction = "muted";
                ViewBag.Message = "Your DojoDachi has died!";
                ViewBag.Error = "false";
                ViewBag.Playing = "false";
            }
            else if(getdachi.Fullness > 99 && getdachi.Happiness > 99)
            {
                ViewBag.Reaction = "kiss";
                ViewBag.Message = "Congratulations! You win!";
                ViewBag.Error = "true";
                ViewBag.Playing = "false";
            }
            HttpContext.Session.SetObjectAsJson("Dachi", getdachi);
            ViewBag.Dachi = getdachi;
            return View("Index");
        }
    }

    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}