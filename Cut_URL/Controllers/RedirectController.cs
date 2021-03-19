using Cut_URL.Business_Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cut_URL.Controllers
{
    public class RedirectController : Controller
    {
        private ICutUrlLogic _urlLogic;
        public RedirectController(ICutUrlLogic urlLogic)
        {
            _urlLogic = urlLogic;
        }
        [Route("/{shortUrl}")]
        [HttpGet]
        public IActionResult Index(string shortUrl)
        {
            try
            {
                string token = HttpContext.Request.Headers["user-token"];

                if(token == null)
                {
                    return StatusCode(404, Json(new { error = "token not found." }));
                }

                string longUrl = _urlLogic.GetLongUrlFromShort(shortUrl, token);

                return Redirect(longUrl);
            }
            catch(GetLongUrlException ex)
            {
                return StatusCode(500, Json(new { error = ex.Message }));
            }
        }
    }
}
