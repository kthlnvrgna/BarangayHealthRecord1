using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;

namespace Barangay_Health_Record.Controllers
{
    public class MasterPatientListController : Controller
    {
        // GET: MasterPatientList
        public ActionResult Index()
        {
            PatientsLogicLayer patientConnection = new PatientsLogicLayer();
            List<Patients> patients = patientConnection.PatientList.ToList();
            return View(patients);
        }
        [HttpGet]//Will only respond if create/register new is triggered
        public ActionResult RegisterNewPatient()
        {
            return View();
        }
        [HttpPost]
        [ActionName("RegisterNewPatient")]
        public ActionResult RegisterNewPatientPost()
        {

            Patients patient = new Patients();
            UpdateModel(patient);  
            PatientsLogicLayer patientBlayer = new PatientsLogicLayer();
            patientBlayer.AddNewPatientRegistration(patient);
            return RedirectToAction("Index"); 
        }

    }
}