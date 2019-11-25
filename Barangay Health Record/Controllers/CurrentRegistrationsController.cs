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
        public ActionResult Index()
        {
            CurrentRegistrationsDBLogic CurrentRegistrations = new CurrentRegistrationsDBLogic();
            List<PatientsModel> Model = CurrentRegistrations.PatientList.ToList();
            return View(Model);
        }  

        [HttpGet]
        public ActionResult Details(int id, string regnum)
        { 
            CurrentRegistrationsDBLogic CurrentRegistrations = new CurrentRegistrationsDBLogic();
            CurrentRegistrationsModel Model = CurrentRegistrations.GetCheckUpData(id, regnum);
            return View(Model);
        }

        [HttpPost]
        [ActionName("Details")]
        public ActionResult SaveCheckUpDetails( string RegNum, string FamilyRecord, string Medicines, string Allergies, string ChiefComplaint, string Consultation)
        {
            CurrentRegistrationDetailsModel Model = new CurrentRegistrationDetailsModel(); 
            Model.RegNum = RegNum;
            Model.FamilyRecord = FamilyRecord;
            Model.Medicines = Medicines;
            Model.Allergies = Allergies;
            Model.ChiefComplaint = ChiefComplaint;
            Model.Consultation = Consultation;

            CurrentRegistrationsDBLogic dbLogic = new CurrentRegistrationsDBLogic();
            dbLogic.UdpatePatientCheckUpDetails(Model); 

            return View();
        } 
    }
}