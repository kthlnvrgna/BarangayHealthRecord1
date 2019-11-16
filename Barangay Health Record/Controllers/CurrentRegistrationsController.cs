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
        // GET: PatientDetails
        public ActionResult Index()
        {
            CurrentRegistrationsLogicLayer CurrentRegistrations = new CurrentRegistrationsLogicLayer();
            List<PatientsModel> CurrentPatientsList = CurrentRegistrations.PatientList.ToList();
            return View(CurrentPatientsList);
        }  

        [HttpGet]
        public ActionResult Details(int id)
        { 
            CurrentRegistrationsLogicLayer CurrentRegistrations = new CurrentRegistrationsLogicLayer();
            CRDetailsModel CheckUpDetails = CurrentRegistrations.GetCheckUpData(id);
            return View(CheckUpDetails);
        }
        [HttpPost]
        [ActionName("Details")]
        public ActionResult UpdatePatientCheckUpDetails(string FamilyRecord, string Medicines, string Allergies, string ChiefComplaint, string Consultation)
        {
            CRDetailsModel CRDetails = new CRDetailsModel()
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
                CurrentRegistrationsLogicLayer CurrentRegistrations = new CurrentRegistrationsLogicLayer();
                CurrentRegistrations.UdpatePatientCheckUpDetails(CRDetails);
                return View(CRDetails);
            }
            return View();
        }
    }
}