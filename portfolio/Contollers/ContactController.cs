using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace portfolio
{
    public class ContactController: Controller
    {   
        [Route("contact")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}