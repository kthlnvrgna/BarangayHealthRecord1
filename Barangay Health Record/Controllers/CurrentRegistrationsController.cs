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
            List<PatientsModel> CurrentPatientsList = CurrentRegistrations.PatientList.ToList();
            return View(CurrentPatientsList);
        }  

        [HttpGet]
        public ActionResult Details(int id)
        { 
            CurrentRegistrationsDBLogic CurrentRegistrations = new CurrentRegistrationsDBLogic();
            CurrentRegistrationsModel CheckUpDetails = CurrentRegistrations.GetCheckUpData(id);
            return View(CheckUpDetails);
        }
        [HttpPost]
        [ActionName("Details")]
        public ActionResult UpdatePatientCheckUpDetails(string FamilyRecord, string Medicines, string Allergies, string ChiefComplaint, string Consultation)
        {
            CurrentRegistrationsModel CRDetails = new CurrentRegistrationsModel()
            {
                FamilyRecord = FamilyRecord,
                Medicines = Medicines,
                Allergies = Allergies,
                ChiefComplaint = ChiefComplaint,
                Consultation = Consultation
            };

            TryUpdateModel(CRDetails);

            if(ModelState.IsValid)
            {
                CurrentRegistrationsDBLogic CurrentRegistrations = new CurrentRegistrationsDBLogic();
                CurrentRegistrations.UdpatePatientCheckUpDetails(CRDetails);
                return View(CRDetails);
            }
            return View();
        }
    }
}