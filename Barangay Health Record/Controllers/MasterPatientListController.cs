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
            TryUpdateModel(patient);

            if (ModelState.IsValid)
            {
                PatientsLogicLayer patientBlayer = new PatientsLogicLayer();
                patientBlayer.AddNewPatientRegistration(patient);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet] 
        public ActionResult Edit(int id)
        {
            PatientsLogicLayer patientBlayer = new PatientsLogicLayer();
            Patients patientsModel = patientBlayer.PatientList.Single(px => px.PatientID == id);

            return View(patientsModel);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult UpdatePatientInfo(Patients patients)
        {
            if(ModelState.IsValid)
            {
                PatientsLogicLayer patientBLayer = new PatientsLogicLayer();
                //patients.PatientID = Convert.ToInt32(Request.Form["PatientID"].ToString());
                patientBLayer.UpdatePatientInfo(patients);

                return RedirectToAction("Index"); 
            } 
            return View(patients);
        }

    }
}