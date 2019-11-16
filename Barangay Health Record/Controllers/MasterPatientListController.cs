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

            TryUpdateModel(patient); 

            var isExisting = patientBlayer.PatientList.Any(px => (px.FirstName == patient.FirstName) && (px.LastName == patient.LastName) && (px.BirthDate == patient.BirthDate));

            if (ModelState.IsValid && isExisting == false )
            {
                patientBlayer = new PatientsLogicLayer();
                patientBlayer.AddNewPatientRegistration(patient);
                var currentPxID = patientBlayer.PatientList.Where(px => (px.FirstName == patient.FirstName) && (px.LastName == patient.LastName) && (px.BirthDate == patient.BirthDate)).FirstOrDefault()?.PatientID;
                patientBlayer.InsertPatientAdmission(currentPxID.ToString());
                var currentPxRegnum = patientBlayer.GetPatientRegnum(currentPxID.ToString());
                patientBlayer.InsertInitialCheckUpDetails(currentPxID.ToString(), currentPxRegnum.ToString());
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