using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barangay_Health_Record.Controllers
{
    public class LogsController : Controller
    {
        // GET: Logs
        public ActionResult Index()
        {
            LogsDbLogic dbLogic = new LogsDbLogic();
            List<LogsModel> model = dbLogic.GetLogs.ToList();
            return View(model);
        }
    }
}