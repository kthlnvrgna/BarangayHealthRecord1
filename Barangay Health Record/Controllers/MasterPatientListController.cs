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
            List<PatientsModel> patients = patientConnection.PatientList.ToList();
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
            PatientsLogicLayer patientBlayer = new PatientsLogicLayer();
            PatientsModel patient = new PatientsModel(); 
            var firstName = patient.FirstName;
            var lastName = patient.LastName;
            var bDay = patient.BirthDate;

            var isNew = patientBlayer.PatientList.FirstOrDefault(x => ((x.FirstName == patient.FirstName) && (x.LastName == patient.LastName) && (x.BirthDate == patient.BirthDate) && (x.Sex == patient.Sex)));

            TryUpdateModel(patient); 

            if (ModelState.IsValid)
            {
                patientBlayer = new PatientsLogicLayer();
                patientBlayer.AddNewPatientRegistration(patient);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet] 
        public ActionResult Edit(int id)
        {
            PatientsLogicLayer patientBlayer = new PatientsLogicLayer();
            PatientsModel patientsModel = patientBlayer.PatientList.Single(px => px.PatientID == id);

            return View(patientsModel);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult UpdatePatientInfo(PatientsModel patients)
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