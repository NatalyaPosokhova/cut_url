using Cut_URL.Business_Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;

namespace Cut_URL.Controllers
{
    public class HomeController : Controller
    {
        private IUserManagement _userManagement;
        public HomeController(IUserManagement userManagement)
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

            var response = new HttpResponseMessage();
            response.Headers.Location = new Uri(token.ToString());

            return response;
        }
    }
}
