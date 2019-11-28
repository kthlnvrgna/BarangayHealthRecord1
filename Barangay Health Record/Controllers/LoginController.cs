using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barangay_Health_Record.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        } 
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Login(LoginModel Model)
        {
            LoginDBLogic dbLogic = new LoginDBLogic();

            if(string.IsNullOrWhiteSpace(Model.UserName) && string.IsNullOrWhiteSpace(Model.Password))
            {
                ModelState.AddModelError("Password", "Provide required data!");
                return View();
            }
            else
            { 
                var userID = dbLogic.FindUser(Model); 
                
                if (userID != null)
                {
                    Session.Add("UserID", userID);

                    CurrentUserDBLogic currentUserDBLogic = new CurrentUserDBLogic();
                    CurrentUserModel SessionModel = new CurrentUserModel();
                    SessionModel = currentUserDBLogic.UserInfo(userID);

                    if(SessionModel != null)
                    {
                        Session.Add("UFname", SessionModel.FirstName);
                        Session.Add("ULname", SessionModel.LastName);
                    }

                    LogsDbLogic logs = new LogsDbLogic();
                    logs.SaveLogs(1, Session["UserID"].ToString(), null);
                    return RedirectToAction("Index", "Patients");
                } 
            }

            ModelState.AddModelError("Password", "Username or password is incorrect!");
            return View(Model);
        }
    }
}