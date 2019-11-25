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
        public ActionResult CreateNewUser()
        {
            NewUserDBLogic dbLogic = new NewUserDBLogic(); 
            NewUserModel model = new NewUserModel();

            if (ModelState.IsValid && model != null)
            {
                dbLogic.AddNewUser(model);
                return RedirectToAction("Index");
            } 

            ViewBag.Message = ("Patient already has an existing record!");  
            return View();
        }

    }
}