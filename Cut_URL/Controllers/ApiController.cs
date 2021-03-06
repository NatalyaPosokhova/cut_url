using Cut_URL.Business_Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;

namespace Cut_URL.Controllers
{
    public class ApiController : Controller
    {
        private IUserManagement _userManagement;
        private ICutUrlLogic _cutUrlLogic;
        public ApiController(IUserManagement userManagement, ICutUrlLogic cutUrlLogic)
        {
            _userManagement = userManagement;
            _cutUrlLogic = cutUrlLogic;
        }
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Post function for registering new user.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>Response with Token in location header.
        /// 200 - Ok.
        /// 409 - User Already Existed. 
        /// 500 - Database error.</returns>
        [HttpPost]
        public JsonResult NewUser(string login, string password)
        {
            var token = _userManagement.RegisterNewUser(login, password);

            HttpResponseMessage response = new HttpResponseMessage();
            response.Headers.Location = new Uri(token.ToString());

            return Json("");
        }

        /// <summary>
        /// Post function for Login user.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>Response with Token in location header.
        /// 200 - Ok.
        /// 404 - User is not found or incorrect password.
        /// 423 - User is blocked.
        /// 500 - Database error.
        /// </returns>
        [HttpPost]
        public JsonResult LoginUser(string login, string password)
        {
            Guid token = _userManagement.LoginUser(login, password);

            HttpResponseMessage response = new HttpResponseMessage();
            response.Headers.Location = new Uri(token.ToString());

            return Json("");
        }

        /// <summary>
        /// Creates short url from long.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="longUrl"></param>
        /// <returns>Short url with Token in location header.
        /// 200 - Ok.
        /// 401 - Unauthorized user.
        /// 500 - Database error.
        /// </returns>
        public JsonResult CreateShortcutUrl(string token, string longUrl)
        {
            string shortUrl = _cutUrlLogic.CreateShortUrlFromLong(longUrl, token);

            HttpResponseMessage response = new HttpResponseMessage();
            response.Headers.Location = new Uri(token.ToString());

            return Json(shortUrl);
        }
    }
}
