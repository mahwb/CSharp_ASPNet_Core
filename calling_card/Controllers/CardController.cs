using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace calling_card
{
    public class CardController : Controller
    {
        // First Name
        // Last Name
        // Age
        // Favorite Color

        [HttpGet]
        [Route("card/{fname}/{lname}/{age}/{color}")]
        public JsonResult CallingCard(string fname, string lname, int age, string color)
        {
            var card = new
            {
                first_name = fname,
                last_name = lname,
                age = age,
                favorite_color = color,
            };
            return Json(card);
        }
    }
}