using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barangay_Health_Record.Controllers
{
    public class AllUsersController : Controller
    {
        // GET: AllUsers
        public ActionResult Index()
        { 
            AllUsersDBLogic dbLogic = new AllUsersDBLogic(); 
            List<AllUsersModel> modelLi = dbLogic.GetAllUsers.ToList();
            ViewBag.AllUsers = ("1");
            return View(modelLi);
        }
    }
}