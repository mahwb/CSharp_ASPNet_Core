using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace dojo_survey
{
    public class SurveyController: Controller
    {
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Errors = new List<string>();
            return View();
        }

        [Route("result")]
        [HttpPost]
        public IActionResult Result(string name, string location, string language, string comment)
        {
            ViewBag.Errors = new List<string>();
            if (name == null) {
                ViewBag.Errors.Add("Name is empty.");
            }
            if (location == null) {
                ViewBag.Errors.Add("Location is empty.");
            }
            if (language == null) {
                ViewBag.Errors.Add("Language is empty.");
            }
            if (ViewBag.Errors.Count == 0) {
                ViewBag.name = name.First().ToString().ToUpper() + name.Substring(1);
                ViewBag.location = location;
                ViewBag.language = language;
                if (comment == null) {
                    ViewBag.comment = "No comment";
                } else {
                    ViewBag.comment = comment;
                }
                return View();
            }
            return View("Index");
        }
    }  
}