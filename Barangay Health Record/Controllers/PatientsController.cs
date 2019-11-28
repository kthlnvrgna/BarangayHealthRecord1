using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LogicLayer;

namespace Barangay_Health_Record.Controllers
{
    public class PatientsController : Controller
    {
        PatientsDBLogic _dbLogic;
        PatientsModel _model;
        // GET: MasterPatientList 
        public ActionResult Index(string fName, string lName, string sex, DateTime? bday)
        {
            PatientsDBLogic _dbLogic = new PatientsDBLogic();

            List<PatientsModel> PatientsLi = _dbLogic.PatientList.ToList();
            if (fName == null && lName == null && sex == null && bday == null)
            {
                return View(PatientsLi);
            }

            //Patient = patientConnection.PatientList.Where(px => px.FirstName == fName && px.LastName == lName && px.Sex == sex && px.BirthDate == bday); 

            var patient = _dbLogic.PatientList.Where(px => px.FirstName == fName && px.LastName == lName && px.Sex == sex && px.BirthDate == bday);
            PatientsLi = patient.ToList();
            return View(PatientsLi);
        }
        [HttpGet]
        public ActionResult RegisterNewPatient()
        {
            return View();
        }
        [ActionName("RegisterNewPatient")]
        [HttpPost]
        public ActionResult RegisterNewPatientPost()
        {
            _model = new PatientsModel();
            _dbLogic = new PatientsDBLogic();

            TryUpdateModel(_model);

            if (string.IsNullOrWhiteSpace(_model.FirstName) ||
                string.IsNullOrWhiteSpace(_model.LastName) ||
                string.IsNullOrWhiteSpace(_model.Sex) ||
                _model.BirthDate == null ||
                string.IsNullOrWhiteSpace(_model.Address) ||
                string.IsNullOrWhiteSpace(_model.CivilStatus) ||
                string.IsNullOrWhiteSpace(_model.Nationality) ||
                string.IsNullOrWhiteSpace(_model.Religion) ||
                string.IsNullOrWhiteSpace(_model.Category))
            {
                if (string.IsNullOrWhiteSpace(_model.FirstName))
                    ModelState.AddModelError("FirstName", "First name is required!");
                if (string.IsNullOrWhiteSpace(_model.LastName))
                    ModelState.AddModelError("LastName", "Last name is required!");
                if (string.IsNullOrWhiteSpace(_model.Sex))
                    ModelState.AddModelError("Sex", "Sex is required!");
                if (_model.BirthDate == null )
                    ModelState.AddModelError("BirthDate", "Birthday is required!");
                if (string.IsNullOrWhiteSpace(_model.Address))
                    ModelState.AddModelError("Address", "Address is required!");
                if (string.IsNullOrWhiteSpace(_model.CivilStatus))
                    ModelState.AddModelError("CivilStatus", "Civil status is required!");
                if (string.IsNullOrWhiteSpace(_model.Nationality))
                    ModelState.AddModelError("Nationality", "Nationality is required!");
                if (string.IsNullOrWhiteSpace(_model.Religion))
                    ModelState.AddModelError("Religion", "Religion is required!");
                if (string.IsNullOrWhiteSpace(_model.Category))
                    ModelState.AddModelError("Category", "Patient type is required!");

                return View();
            }

                var isExisting = _dbLogic.PatientList.Any(px => (px.FirstName == _model.FirstName) && (px.LastName == _model.LastName) && (px.BirthDate == _model.BirthDate));

            if (ModelState.IsValid && isExisting == false)
            {

                _dbLogic = new PatientsDBLogic();
                _dbLogic.AddNewPatientRegistration(_model);
                _dbLogic.InsertPatientAdmission(null);
                _dbLogic.InsertInitialCheckUpDetails(null);
                _dbLogic.IncPxRxNum();

                LogsDbLogic logs = new LogsDbLogic();
                logs.SaveLogs(4, Session["UserID"].ToString(), "First name: " + _model.FirstName + " Last name: " + _model.LastName + " Birthday: " + _model.BirthDate+ " Sex: " + _model.Sex);

                string patientID = _dbLogic.GetPxRxNum("PxID");
                logs = new LogsDbLogic();
                logs.SaveLogs(5, Session["UserID"].ToString(), "For patient registration number: " + (Convert.ToInt32(patientID) - 1).ToString());

                return RedirectToAction("Index");
            }

            ViewBag.Message = ("Patient already has an existing record!");
            //return RedirectToAction("Index", new { fName = patient.FirstName, lName = patient.LastName, sex = patient.Sex, bday = patient.BirthDate });
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            _dbLogic = new PatientsDBLogic();
            _model = _dbLogic.PatientList.Single(px => px.PatientID == id);

            return View(_model);
        }

        [ActionName("Edit")]
        [HttpPost]
        public ActionResult UpdatePatientInfo(PatientsModel Model)
        {
            if(string.IsNullOrWhiteSpace(Model.FirstName) || 
                string.IsNullOrWhiteSpace(Model.LastName) ||
                string.IsNullOrWhiteSpace(Model.Address) ||
                string.IsNullOrWhiteSpace(Model.CivilStatus) ||
                string.IsNullOrWhiteSpace(Model.Nationality) ||
                string.IsNullOrWhiteSpace(Model.Religion))
            {
                if (string.IsNullOrWhiteSpace(Model.FirstName))
                    ModelState.AddModelError("FirstName", "First name is required!"); 
                if (string.IsNullOrWhiteSpace(Model.LastName))
                    ModelState.AddModelError("LastName", "Last name is required!");
                if (string.IsNullOrWhiteSpace(Model.Address))
                    ModelState.AddModelError("Address", "Address is required!");
                if (string.IsNullOrWhiteSpace(Model.CivilStatus))
                    ModelState.AddModelError("CivilStatus", "Civil status is required!");
                if (string.IsNullOrWhiteSpace(Model.Nationality))
                    ModelState.AddModelError("Nationality", "Nationality is required!");
                if (string.IsNullOrWhiteSpace(Model.Religion))
                    ModelState.AddModelError("Religion", "Religion is required!");

                return View();
            }

            if (ModelState.IsValid)
            {
                _dbLogic = new PatientsDBLogic();
                _dbLogic.UpdatePatientInfo(Model);

                LogsDbLogic logs = new LogsDbLogic();
                StringBuilder remarks = new StringBuilder();
                 
                remarks.Append("First name: " + Model.FirstName);
                remarks.Append(" Last name: " + Model.LastName); 
                remarks.Append(" Address: " + Model.Address);
                remarks.Append(" Civil status: " + Model.CivilStatus);
                remarks.Append(" Nationality: " + Model.Nationality);
                remarks.Append(" Religion: " + Model.Religion); 

                logs.SaveLogs(6, Session["UserID"].ToString(), remarks.ToString());

                return RedirectToAction("Index");
            }
            return View(Model);
        }

        public ActionResult Details()
        {
            return View();
        }
        public ActionResult ReAdmit(string id)
        {
            _dbLogic = new PatientsDBLogic();

            _dbLogic.InsertPatientAdmission(id);
            _dbLogic.InsertInitialCheckUpDetails(id);
            _dbLogic.IncRxNum();
            return RedirectToAction("Index", "CurrentRegistrations");
        } 

    }
}