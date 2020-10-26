using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cut_URL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cut_URL.Controllers
{
    public class HomeController : Controller
    {
        private IUserRepository _repository;
        public HomeController(IUserRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        //public ActionResult Index()
        //{
        //    return View(_repository.GetUsers());
        //}
        //public ActionResult Details(int id)
        //{
        //    User user = _repository.Get(id);
        //    if (user != null)
        //        return View(user);
        //    return NotFound();
        //}
        [HttpPost]
        public ActionResult Create(User user)
        {
            _repository.Create(user);
            return RedirectToAction("Index");
        }

        //public ActionResult Edit(int id)
        //{
        //    User user = _repository.Get(id);
        //    if (user != null)
        //        return View(user);
        //    return NotFound();
        //}

        //[HttpPost]
        //public ActionResult Edit(Url url)
        //{
        //    _repository.UpdateUrlTransitionsQuantity(url);
        //    return RedirectToAction("Index");
        //}

        //[HttpGet]
        //[ActionName("Delete")]
        //public ActionResult ConfirmDelete(int id)
        //{
        //    User user = _repository.Get(id);
        //    if (user != null)
        //        return View(user);
        //    return NotFound();
        //}
        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    _repository.Delete(id);
        //    return RedirectToAction("Index");
        //}
    }
}
