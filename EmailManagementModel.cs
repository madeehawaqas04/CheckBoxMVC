using HRManagementSystem.Models;
using HRManagementSystem.Session;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace HRManagementSystem.DomainObjects
{
	public class EmailManagementModel
	{
        public int Id { get; set; }

        public int FkCompanyId { get; set; }

        public int FkEmailTypeId { get; set; }
        public bool IsCampanyAllowEmail { get; set; }
        public bool IsAdminAllow { get; set; }

        public String ComanyName { get; set; }
        public String EmailType { get; set; }
        //public IEnumerable<System.Web.Mvc.SelectListItem> EmailTypes { get; set; }

        public List<EmailTypeModel> EmailTypes { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Company { get; set; }

        private HRSystemEntities db = new HRSystemEntities();


        public bool Save(EmailManagementModel model)
        {
            try
            {
                foreach (var item in model.EmailTypes)
                {
                    var emailRecord = db.EmailManagements.ToList().Find(p => p.FkCompanyId == model.FkCompanyId && p.FkEmailTypeId == item.Id);

                    if (emailRecord == null)
                    {

                        // updatedHobby.IsSelected = hobby.IsSelected;
                        EmailManagement obj = new EmailManagement
                        {
                            FkCompanyId = model.FkCompanyId,
                            FkEmailTypeId = item.Id,
                            IsCampanyAllowEmail = model.IsCampanyAllowEmail,
                            IsAdminAllow = item.Selected

                        };
                        db.EmailManagements.Add(obj);
                        db.SaveChanges();
                    }
                    else {

                        emailRecord.IsAdminAllow = item.Selected;
                        db.Entry(emailRecord).State = EntityState.Modified;
                        db.SaveChanges();
                      

                    }

                }

                
                return true;
            }
            catch
            {
                return false;
            }
        }


        public List<EmailManagementModel> Get()
        {
            int id = 0;
            //var u = CSessionObjects.sCompanyID;
            if (CSessionObjects.sCompanyID!=null && CSessionObjects.sCompanyID != "") {
               id = Convert.ToInt32(CSessionObjects.sCompanyID);
            }
            
            if (id == 0)
            {
                var result = (from x in db.EmailManagements
                              join Company in db.Companies on x.FkCompanyId equals Company.CompanyId
                              join EmailType in db.EmailTypes on x.FkEmailTypeId equals EmailType.Id
                              select new EmailManagementModel()
                              {
                                  Id = x.Id,
                                  FkCompanyId = x.FkCompanyId,
                                  FkEmailTypeId = x.FkEmailTypeId,
                                  IsCampanyAllowEmail = x.IsCampanyAllowEmail,
                                  IsAdminAllow = x.IsAdminAllow,
                                  ComanyName = Company.CompanyName,
                                  EmailType = EmailType.Name,
                              }).ToList();

                return result;
            }
            else
            {
                var result = (from x in db.EmailManagements.Where(x => x.FkCompanyId == id && x.IsAdminAllow == true)
                              join Company in db.Companies on x.FkCompanyId equals Company.CompanyId
                              join EmailType in db.EmailTypes on x.FkEmailTypeId equals EmailType.Id
                              select new EmailManagementModel()
                              {
                                  Id = x.Id,
                                  FkCompanyId = x.FkCompanyId,
                                  FkEmailTypeId = x.FkEmailTypeId,
                                  IsCampanyAllowEmail = x.IsCampanyAllowEmail,
                                  IsAdminAllow = x.IsAdminAllow,
                                  ComanyName = Company.CompanyName,
                                  EmailType = EmailType.Name,
                              }).ToList();

                return result;
            }

        }

        public void ChangeAdminEStatus(int Id)
        {
            EmailManagement email = db.EmailManagements.Where(x => x.Id == Id).SingleOrDefault();
            bool status = Convert.ToBoolean(email.IsAdminAllow);
            if (status == true)
            {
                email.IsAdminAllow = false;
            }
            else
            {
                email.IsAdminAllow = true;
            }
            
            db.Entry(email).State = EntityState.Modified;
            db.SaveChanges();
        }



        public void ChangeCompanyEStatus(int Id)
        {
            EmailManagement email = db.EmailManagements.Where(x => x.Id == Id).SingleOrDefault();
            bool status = Convert.ToBoolean(email.IsCampanyAllowEmail);
            if (status == true)
            {
                email.IsCampanyAllowEmail = false;
            }
            else
            {
                email.IsCampanyAllowEmail = true;
            }

            db.Entry(email).State = EntityState.Modified;
            db.SaveChanges();
        }



    }
}