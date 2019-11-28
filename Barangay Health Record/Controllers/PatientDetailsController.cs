using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barangay_Health_Record.Controllers
{
    public class PatientDetailsController : Controller
    {
        // GET: PatientDetails
        public ActionResult Index(int id)
        {
            PatientsDBLogic dbLogic = new PatientsDBLogic();
            PatientsModel model = new PatientsModel();

            model = dbLogic.PatientList.Single(px => px.PatientID == Convert.ToInt32(id));

            if (model != null)
            {
                Session.Add("PxPatientID", model.PatientID); 
                Session.Add("PxRegnum", model.RegNum);
                Session.Add("PxFname", model.FirstName);
                Session.Add("PxMname", model.MiddleName);
                Session.Add("PxLname", model.LastName);
                Session.Add("PxSex", model.Sex);
                Session.Add("PxBirthDate", model.BirthDate);
                Session.Add("PxAddress", model.Address);
                Session.Add("PxCivilStatus", model.CivilStatus);
                Session.Add("PxNationality", model.Nationality);
                Session.Add("PxReligion", model.Religion);
                Session.Add("PxCategory", model.Category);  
            }

            PatientDetailsDBLogic listDbLogic = new PatientDetailsDBLogic();
            List<PatientDetailsModel> listModel = listDbLogic.PatientDetails(id.ToString()).ToList();
            return View(listModel);
        }  
    }
}