using HRManagementSystem.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRManagementSystem.DomainObjects
{
	public class EmailTypeModel
	{
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public bool Selected { get; set; } 


        private HRSystemEntities db = new HRSystemEntities();

        public List<EmailTypeModel> CompanyEmailTypeList(int companyId)
        {
            //return (from e in db.EmailTypes
            //        select new EmailTypeModel
            //        {
            //            Id = e.Id,
            //            Name = e.Name,
            //            Selected = false,
            //           // (from h in db.Holidays where h.fkCompanyId == companyid select h).ToList<Holiday>();
            //            //   selected = db.EmailManagements.ToList().Find(p => p.FkCompanyId == FkCompanyId && p.FkEmailTypeId == e.Id)
            //        }).ToList();


            var list = new List<EmailTypeModel>();
            foreach (var type in db.EmailTypes.ToList())
            {
                var typeList = new EmailTypeModel
                {
                    Id = type.Id,
                    Name = type.Name,
                };

                var emailRecord = db.EmailManagements.ToList().Find(p => p.FkCompanyId == companyId && p.FkEmailTypeId == type.Id);


                if (emailRecord!=null)
                {
                    typeList.Selected = emailRecord.IsAdminAllow;
                }
                else
                {
                    typeList.Selected = false;
                }

                list.Add(typeList);

                
            }
            return list.ToList();

        }



        public List<EmailTypeModel> EmailTypeList(string[] typeId)
        {
            var list = new List<EmailTypeModel>();
            foreach (var type in db.EmailTypes.ToList())
            {
                var typeList = new EmailTypeModel
                {
                    Id = type.Id,
                    Name = type.Name,
                };

                

                if (typeId.Contains(type.Id.ToString()))
                {
                    typeList.Selected = true;
                }


              //foreach (var id in typeId)
              //  {
              //      if (type.Id == Convert.ToInt32(id))
              //      {
              //          typeList.Selected = true;
              //      }
              //      else
              //      {
              //          typeList.Selected = false;
              //      }
              //  }

                list.Add(typeList);


            }
            return list.ToList();

        }


    }
}