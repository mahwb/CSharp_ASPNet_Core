using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;
using Newtonsoft.Json;

namespace random_code
{
    public class CodeController: Controller
    {
        
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            int? count = HttpContext.Session.GetObjectFromJson<int>("count");
            if (count == null) {
                count = 0;
            }
            count += 1;
            HttpContext.Session.SetObjectAsJson("count", (int)count);
            ViewBag.code = Code();
            ViewBag.count = count;
            return View();
        }

        [Route("generate")]
        [HttpGet]
        public JsonResult Generate() {
            int? count = HttpContext.Session.GetObjectFromJson<int>("count");
            count += 1;
            HttpContext.Session.SetObjectAsJson("count", (int)count);
            var json = new {
                count = HttpContext.Session.GetObjectFromJson<int>("count"),
                code = Code(),
            };
            return Json(json);
        }

        [Route("clear")]
        [HttpGet]
        public JsonResult Clear()
        {   
            HttpContext.Session.SetObjectAsJson("count", 0);
            var json = new {
                count = HttpContext.Session.GetObjectFromJson<int>("count"),
                code = Code(),
            };
            return Json(json);
        }

        // helper class to generate code
        public string Code()
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string code = "";
            Random Rand = new Random();
            for(int i = 0; i < 14; i++)
            {
                code = code + characters[Rand.Next(0, characters.Length)];
            }
            return code;
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