using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;

namespace Barangay_Health_Record.Controllers
{
    public class PatientInfoController : Controller
    {
        // GET: PatientInfo
        public ActionResult Index(int id)
        {
            
            return View();
        }
    }
}