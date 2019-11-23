using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;

namespace Barangay_Health_Record.Controllers
{
    public class PatientsController : Controller
    {
        // GET: MasterPatientList
        public ActionResult Index(string fName, string lName, string sex, DateTime? bday)
        {
            PatientsDBLogic patientConnection = new PatientsDBLogic();

            List<PatientsModel> PatientsLi= patientConnection.PatientList.ToList();
            if(fName == null && lName == null && sex == null && bday == null)
            {
                return View(PatientsLi); 
            }

            //Patient = patientConnection.PatientList.Where(px => px.FirstName == fName && px.LastName == lName && px.Sex == sex && px.BirthDate == bday); 

            var patient = patientConnection.PatientList.Where(px => px.FirstName == fName && px.LastName == lName && px.Sex == sex && px.BirthDate == bday);
            PatientsLi = patient.ToList();
            return View(PatientsLi);
        }
        [HttpGet]
        public ActionResult RegisterNewPatient()
        { 
            return View();
        }
        [HttpPost]
        [ActionName("RegisterNewPatient")]
        public ActionResult RegisterNewPatientPost()
        {
            PatientsDBLogic patientBlayer = new PatientsDBLogic();
            PatientsModel patient = new PatientsModel(); 

            TryUpdateModel(patient); 

            var isExisting = patientBlayer.PatientList.Any(px => (px.FirstName == patient.FirstName) && (px.LastName == patient.LastName) && (px.BirthDate == patient.BirthDate));

            if (ModelState.IsValid && isExisting == false )
            {

                patientBlayer = new PatientsDBLogic();
                patientBlayer.AddNewPatientRegistration(patient); 
                patientBlayer.InsertPatientAdmission(); 
                patientBlayer.InsertInitialCheckUpDetails(); 
                patientBlayer.IncPxRxNum();

                return RedirectToAction("Index");
            }

            ViewBag.Message = ("Patient already has an existing record!");
            return RedirectToAction("Index", new { fName = patient.FirstName, lName = patient.LastName, sex = patient.Sex, bday = patient.BirthDate });
            //return View();
        }

        [HttpGet] 
        public ActionResult Edit(int id)
        {
            PatientsDBLogic patientBlayer = new PatientsDBLogic();
            PatientsModel patientsModel = patientBlayer.PatientList.Single(px => px.PatientID == id);

            return View(patientsModel);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult UpdatePatientInfo(PatientsModel patients)
        {
            if(ModelState.IsValid)
            {
                PatientsDBLogic patientBLayer = new PatientsDBLogic();
                patientBLayer.UpdatePatientInfo(patients);

                return RedirectToAction("Index"); 
            } 
            return View(patients);
        }

    }
}