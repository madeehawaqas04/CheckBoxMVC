using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRManagementSystem.Models;
using HRManagementSystem.DomainObjects;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.ComponentModel.Design;

namespace HRManagementSystem.Controllers
{

   
    public class EmailManagementController : Controller
    {
        private HRSystemEntities db = new HRSystemEntities();
        CompanyModel objCompany = new CompanyModel();
        EmailTypeModel objEmailType = new EmailTypeModel();
        EmailManagementModel objEmailManage = new EmailManagementModel();

        [SessionExpiredForAdmin]
        public ActionResult Index()
        {
            return View(GetData());
        }


        [SessionExpired]
        public ActionResult EmailSetting()
        {
            return View(GetData());
        }

      
        public List<EmailManagementModel> GetData() {

            var emailManagements = objEmailManage.Get();
            List<string> companyNames = new List<string>();

            foreach (var em in emailManagements)
            {
                string companyName = GetCompanyName((int)em.FkCompanyId);

                companyNames.Add(companyName);
            }
            
            ViewBag.CompanyNames = companyNames;

            var uniqueCompanyNames = companyNames.Distinct().ToList();

            ViewBag.CompanyList = uniqueCompanyNames;

            return emailManagements;

        }

        public string GetCompanyName(int companyid)
        {
            var companyName = db.Companies
           .Where(company => company.CompanyId == companyid)
           .Select(company => company.CompanyName)
           .FirstOrDefault();
            return companyName;
        }

        [SessionExpiredForAdmin]
        [HttpGet]
        public ActionResult Create()
        {
            EmailManagementModel model = new EmailManagementModel();
            model.Company = objCompany.CompanyList();
            int companyId = Convert.ToInt32(model.Company.First().Value);
            model.EmailTypes = objEmailType.CompanyEmailTypeList(companyId);

            return View(model);
        }


        public ActionResult ChangeEmailStatus(int id = 0)
        {
            ViewBag.Message = Constant.CONFIRM_STATUS;
            ViewBag.emId = id;

            return View("Index", GetData());
        }

        public ActionResult ChangeCompEmailStatus(int id = 0)
        {
            ViewBag.Message = Constant.CONFIRM_STATUS;
            ViewBag.emId = id;

            return View("EmailSetting", GetData());
        }

        [SessionExpiredForAdmin]
        public ActionResult ConfirmChangeStatus(int emId = 0)
        {
            objEmailManage.ChangeAdminEStatus(emId);
            ViewBag.Result = Constant.STATUS_CHANGED;
            return RedirectToAction("Index");
        }

        [SessionExpired]
        public ActionResult ConfirmCompChangeStatus(int id = 0)
        {
            objEmailManage.ChangeCompanyEStatus(id);
            ViewBag.Result = Constant.STATUS_CHANGED;
            return RedirectToAction("EmailSetting");
        }

        [HttpPost]
        [SessionExpiredForAdmin]
        public ActionResult Create(EmailManagementModel model, FormCollection form, string[] EmailTypes)
        {

            if (model.EmailTypes.Count == 0)
            {
                model.EmailTypes = objEmailType.EmailTypeList(EmailTypes);
            }
            
            //model.EmailTypes = objEmailType.EmailTypeList(EmailTypes);
            objEmailManage.Save(model);

            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult GetEmailType(string CompanyId)
        {

            var EmailTypes = objEmailType.CompanyEmailTypeList(Convert.ToInt32(CompanyId));

            return Json(EmailTypes, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }



    }
}