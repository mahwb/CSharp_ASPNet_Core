using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace portfolio
{
    public class HomeController: Controller
    {
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}