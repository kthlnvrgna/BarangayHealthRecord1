using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barangay_Health_Record.Controllers
{
    public class CurrentUserController : Controller
    {
        // GET: CurrentUser
        private CurrentUserDBLogic _dbLogic;
        private CurrentUserModel _model;
        public ActionResult Index()
        {
            _dbLogic = new CurrentUserDBLogic();
            _model = new CurrentUserModel();
            _model = _dbLogic.UserInfo(Session["UserID"].ToString()); 
            return View(_model); 
        }

        public ActionResult UserDetails()
        {
            _dbLogic =  new CurrentUserDBLogic();
            _model = new CurrentUserModel();
            _model = _dbLogic.UserInfo(Session["UserID"].ToString());
            return View(_model);
        }
        [HttpPost]
        [ActionName("UserDetails")]
        public ActionResult UpdateUserDetails(string FirstName, string LastName, string UserName, string Password)
        {
            _dbLogic = new CurrentUserDBLogic();
            _model = new CurrentUserModel();

            _model.FirstName = FirstName;
            _model.LastName = LastName;
            _model.UserName = UserName;
            _model.Password = Password;

            if(string.IsNullOrWhiteSpace(FirstName) ||
                string.IsNullOrWhiteSpace(LastName) ||
                string.IsNullOrWhiteSpace(UserName) ||
                string.IsNullOrWhiteSpace(Password)
                )
            {
                if(string.IsNullOrWhiteSpace(FirstName))
                {
                    ModelState.AddModelError("FirstName", "First name is required!"); 
                }
                if (string.IsNullOrWhiteSpace(LastName))
                {
                    ModelState.AddModelError("LastName", "Last name is required!");
                }
                if (string.IsNullOrWhiteSpace(UserName))
                {
                    ModelState.AddModelError("UserName", "Username is required!");
                }
                if (string.IsNullOrWhiteSpace(Password))
                {
                    ModelState.AddModelError("Password", "Password is required!");
                }

                return View();
            }

            NewUserDBLogic userCheckDBLogic = new NewUserDBLogic();
             
            if(_dbLogic.UserNameCorrespondsUserID(Session["UserID"].ToString(),_model.UserName) == false)
            {
                if(userCheckDBLogic.CheckExistingUser(_model.UserName))
                { 
                    _model = new CurrentUserModel();
                    _model = _dbLogic.UserInfo(Session["UserID"].ToString());
                    ModelState.AddModelError("UserName", "Username is already taken!");
                    return View();
                }
            }

            _dbLogic.UpdateUserInfo(_model, Session["UserID"].ToString()); 

            LogsDbLogic logs = new LogsDbLogic();
            logs.SaveLogs(3, Session["UserID"].ToString(), "First name: " + _model.FirstName + " Last name: " + _model.LastName + " Username: " + _model.UserName + " Password: " +_model.Password);

            _model = new CurrentUserModel();
            _model = _dbLogic.UserInfo(Session["UserID"].ToString());

            CurrentUserDBLogic currentUserDBLogic = new CurrentUserDBLogic();
            CurrentUserModel SessionModel = new CurrentUserModel();
            SessionModel = currentUserDBLogic.UserInfo(_model.UserID);

            if (SessionModel != null)
            {
                Session.Add("UFname", SessionModel.FirstName);
                Session.Add("ULname", SessionModel.LastName);
            }


            ViewBag.Success = ("1");
            return View(_model);
        }
    }
}