using Cut_URL.Business_Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;

namespace Cut_URL.Controllers
{
    public class ApiController : Controller
    {
        private IUserManagement _userManagement;
        public ApiController(IUserManagement userManagement)
        {
            _userManagement = userManagement;
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
        public HttpResponseMessage NewUser(string login, string password)
        {
            var token = _userManagement.RegisterNewUser(login, password);

            HttpResponseMessage response = new HttpResponseMessage();
            response.Headers.Location = new Uri(token.ToString());

            return response;
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
        public HttpResponseMessage LoginUser(string login, string password)
        {
            var token = _userManagement.LoginUser(login, password);

            HttpResponseMessage response = new HttpResponseMessage();
            response.Headers.Location = new Uri(token.ToString());

            return response;
        }
    }
}
