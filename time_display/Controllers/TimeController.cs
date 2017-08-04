using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace time_display {

    public class TimeController : Controller {

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}