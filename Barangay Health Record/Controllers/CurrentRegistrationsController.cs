using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;

namespace Barangay_Health_Record.Controllers
{
    public class CurrentRegistrationsController : Controller
    {
        private CurrentRegistrationsDBLogic _dbLogic;
        public ActionResult Index()
        {
            _dbLogic = new CurrentRegistrationsDBLogic();
            List<PatientsModel> Model = _dbLogic.PatientList.ToList();
            return View(Model);
        }   
        public ActionResult Details(int id, string regnum)
        {
            _dbLogic = new CurrentRegistrationsDBLogic();
            CurrentRegistrationsModel Model = _dbLogic.GetCheckUpData(id, regnum);

            if (id == -2)
                ViewBag.Details = ("Details");
            return View(Model);
        }

        [HttpPost]
        [ActionName("Details")]
        public ActionResult SaveCheckUpDetails( string RegNum, string FamilyRecord, string Medicines, string Allergies, string ChiefComplaint, string Consultation, string Treatment, string Diagnosis)
        {
            CurrentRegistrationDetailsModel Model = new CurrentRegistrationDetailsModel(); 
            Model.RegNum = RegNum;
            Model.FamilyRecord = FamilyRecord;
            Model.Medicines = Medicines;
            Model.Allergies = Allergies;
            Model.ChiefComplaint = ChiefComplaint;
            Model.Consultation = Consultation;
            Model.Treatment = Treatment;
            Model.Diagnosis = Diagnosis;

            _dbLogic = new CurrentRegistrationsDBLogic();
            _dbLogic.UdpatePatientCheckUpDetails(Model); 

            CurrentRegistrationsModel ViewModel = _dbLogic.GetCheckUpData(-1,Model.RegNum);

            ViewBag.Message = ("Saved!");
            return View(ViewModel);
        } 

        public ActionResult Discharge(string ID)
        {
            _dbLogic = new CurrentRegistrationsDBLogic();
            _dbLogic.DischargePatient(ID);
            return RedirectToAction("Index");
        }
    }
}