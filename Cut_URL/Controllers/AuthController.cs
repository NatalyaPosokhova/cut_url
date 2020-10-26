using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cut_URL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cut_URL.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            //var test = Request.Body;

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult Register(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var check = _db.Users.FirstOrDefault(s => s.Email == user.Email);
        //        if (check == null)
        //        {
        //            user.Password = GetMD5(user.Password);
        //            _db.Configuration.ValidateOnSaveEnabled = false;
        //            _db.Users.Add(user);
        //            _db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ViewBag.error = "Email already exists";
        //            return View();
        //        }
        //    }
        //    return View();
        //}
        ////GET: Register

        //public ActionResult Register()
        //{
        //    return View();
        //}

        ////POST: Register
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Register(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var check = _db.Users.FirstOrDefault(s => s.Email == user.Email);
        //        if (check == null)
        //        {
        //            user.Password = GetMD5(user.Password);
        //            _db.Configuration.ValidateOnSaveEnabled = false;
        //            _db.Users.Add(user);
        //            _db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ViewBag.error = "Email already exists";
        //            return View();
        //        }
        //    }
        //    return View();
        //}
        //public ActionResult Login()
        //{
        //    return View();
        //}



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(string email, string password)
        //{
        //    if (ModelState.IsValid)
        //    {


        //        var f_password = GetMD5(password);
        //        var data = _db.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
        //        if (data.Count() > 0)
        //        {
        //            //add session
        //            Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
        //            Session["Email"] = data.FirstOrDefault().Email;
        //            Session["idUser"] = data.FirstOrDefault().idUser;
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ViewBag.error = "Login failed";
        //            return RedirectToAction("Login");
        //        }
        //    }
        //    return View();
        //}


        ////Logout
        //public ActionResult Logout()
        //{
        //    Session.Clear();//remove session
        //    return RedirectToAction("Login");
        //}



        ////create a string MD5
        //public static string GetMD5(string str)
        //{
        //    MD5 md5 = new MD5CryptoServiceProvider();
        //    byte[] fromData = Encoding.UTF8.GetBytes(str);
        //    byte[] targetData = md5.ComputeHash(fromData);
        //    string byte2String = null;

        //    for (int i = 0; i < targetData.Length; i++)
        //    {
        //        byte2String += targetData[i].ToString("x2");

        //    }
        //    return byte2String;
        //}
    }
}
