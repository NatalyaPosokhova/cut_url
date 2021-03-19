using Cut_URL.Business_Logic;
using Cut_URL.Data;
using Cut_URL.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Cut_URL.Controllers
{
    [Route("/api/{action}")]
    public class ApiController : Controller
    {
        private IUserManagement _userManagement;
        private ICutUrlLogic _cutUrlLogic;
        public ApiController(IUserManagement userManagement, ICutUrlLogic cutUrlLogic)
        {
            _userManagement = userManagement;
            _cutUrlLogic = cutUrlLogic;
        }
        [HttpGet]
        public string Index()
        {
            return "Hello";
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
       
        public IActionResult NewUser([FromBody]UserData user)
        {
            try
            {
                var token = _userManagement.RegisterNewUser(user.login, user.password);
                return Json(new { token = token });
            }
            catch (UnableRegisterUserException ex)
            {
                return StatusCode(409, Json(new { error = ex.Message }));
            }
            catch (UserManageException ex)
            {
                return StatusCode(500, Json(ex));
            }
        }

        /// <summary>
        /// Post function for Login user.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>Response with Token in location header.
        /// 200 - Ok.
        /// 404 - User is not found or incorrect password.
        /// 423 - User is blocked.TODO
        /// 500 - Database error.
        /// </returns>
        [HttpPost]
        public IActionResult LoginUser([FromBody] UserData user)
        {
            try
            {
                Guid token = _userManagement.LoginUser(user.login, user.password);
                return Json(new { token = token });
            }
            catch(UnableToLoginUserException ex)
            {
                return StatusCode(404, Json(ex)); 
            }
            catch (IncorrectPasswordException ex)
            {
                return StatusCode(404, Json(ex));
            }
            catch (UserManageException ex)
            {
                return StatusCode(500, Json(ex));
            }
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
        [HttpPost]
        public IActionResult CreateShortcutUrl([FromBody] ShortcutUrlData data)
        {
            try
            {
                string shortUrl = _cutUrlLogic.CreateShortUrlFromLong(data.LongUrl, data.UserId);
                return Json(shortUrl);
            }
            catch (CreateShortUrlException ex)
            {
                return StatusCode(500, Json(ex));
            }
        }

        /// <summary>
        /// Gets all user data on urls.
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Returns all user urls, hits, date creation.</returns>
        /// 200 - Ok.
        /// 401 - Unauthorized user.
        /// 500 - Database error.
        [HttpPost]
        public IActionResult GetAllUrlsByUser([FromBody] UserData data)
        {
            try
            {
                IEnumerable<ShortcutUrlData> allUserUrls = _cutUrlLogic.GetAllUserInformation(data.token);
                var convertedJson = JsonConvert.SerializeObject(allUserUrls, Formatting.Indented);
                return Json(allUserUrls);
            }
            catch (CreateShortUrlException ex)
            {
                return StatusCode(500, Json(ex));
            }
        }

        //[HttpGet("{shortUrl}")]
        //public JsonResult GetUrlTransition(string token)
        //{

        //}

        //[HttpPost]
        //public JsonResult PostUrlTransition(string token)
        //{

        //}
    }
}
