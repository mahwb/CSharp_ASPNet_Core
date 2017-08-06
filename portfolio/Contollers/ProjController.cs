using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace portfolio
{
    public class ProjController: Controller
    {
        [Route("projects")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}