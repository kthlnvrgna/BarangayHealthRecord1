using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;

namespace Barangay_Health_Record.Controllers
{
    public class NewUserController : Controller
    {
        // GET: NewUser
        [HttpGet]
        public ActionResult Index()
        { 
            return View();
        }

        [ActionName("Index")]
        [HttpPost]
        public ActionResult CreateNewUser(NewUserModel model)
        {
            NewUserDBLogic dbLogic = new NewUserDBLogic();

            if(dbLogic.CheckExistingUser(model.UserName))
            { 
                ModelState.AddModelError("UserName", "Username already taken.");
                return View(model);
            }

            //TryValidateModel(model);

            //if (ModelState.IsValid && model != null)
            //{
            //    dbLogic.AddNewUser(model);
            //    return RedirectToAction("Index", "Login");
            //}

            if (string.IsNullOrWhiteSpace(model.UserName) ||
                string.IsNullOrWhiteSpace(model.Password) ||
                string.IsNullOrWhiteSpace(model.FirstName) ||
                string.IsNullOrWhiteSpace(model.LastName) ||
                string.IsNullOrWhiteSpace(model.Sex) ||
                string.IsNullOrWhiteSpace(model.UserType) ||
                model.BirthDate == null)
            {
                if (string.IsNullOrWhiteSpace(model.UserName))
                    ModelState.AddModelError("UserName", "Username is required.");

                if (string.IsNullOrWhiteSpace(model.Password))
                    ModelState.AddModelError("Password", "Password is required.");

                if (string.IsNullOrWhiteSpace(model.FirstName))
                    ModelState.AddModelError("FirstName", "First name is required.");

                if (string.IsNullOrWhiteSpace(model.LastName))
                    ModelState.AddModelError("LastName", "Last name is required.");

                if (string.IsNullOrWhiteSpace(model.Sex))
                    ModelState.AddModelError("Sex", "Sex is required.");

                if (string.IsNullOrWhiteSpace(model.UserType))
                    ModelState.AddModelError("UserType", "User type is required.");

                if (model.BirthDate == null)
                    ModelState.AddModelError("BirthDate", "Birthday is required.");

                return View(model);
            }

            dbLogic.AddNewUser(model); 

            ViewBag.Message = ("Account Created!");
            return View();
        } 
    }
}