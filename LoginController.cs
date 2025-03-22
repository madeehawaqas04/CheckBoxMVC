using HRManagementSystem.Models;
using System.Web.Mvc;
using HRManagementSystem.DomainObjects;
using HRManagementSystem.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Data;
using System.Net.Mail;
using System.Runtime.Remoting.Contexts;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Net;
using System.ComponentModel.Design;

namespace HRManagementSystem.Controllers
{

    public class LoginController : Controller
    {

        private HRSystemEntities db = new HRSystemEntities();

        //
        // GET: /Login/

        /// <summary>  
        /// Validate Captcha  
        /// </summary>  
        /// <param name="response"></param>  
        /// <returns></returns>  

        public ActionResult Index()
        {
            if (Request.Path.ToLower() == "/login/index" || Request.Path.ToLower() == "/login/" || Request.Path.ToLower() == "/login")
            {
                return Redirect("/");
            }
            return HttpNotFound();
        }

        public ActionResult AboutUs()
        {
            if (Request.Path.ToLower() == "/login/aboutus" || Request.Path.ToLower() == "/login/about-us")
            {
                return Redirect("/About-Us");
            }
            return HttpNotFound();
        }
        public ActionResult Feature()
        {
            if (Request.Path.ToLower() == "/login/feature")
            {
                return Redirect("/feature");
            }
            return HttpNotFound();
        }
        public ActionResult Pricing()
        {
            if (Request.Path.ToLower() == "/login/pricing")
            {
                return Redirect("/pricing");
            }
            return HttpNotFound();
        }
        public ActionResult ContactUs()
        {
            if (Request.Path.ToLower() == "/login/contactus" || Request.Path.ToLower() == "/login/contact-us")
            {
                return Redirect("/Contact-Us");
            }
            return HttpNotFound();
        }
        public ActionResult Resources()
        {
            if (Request.Path.ToLower() == "/login/resources")
            {
                return Redirect("/resource");
            }
            return HttpNotFound();
        }
        public ActionResult Blog()
        {
            if (Request.Path.ToLower() == "/login/blog")
            {
                return Redirect("/Blog");
            }
            return HttpNotFound();
        }
        public ActionResult Community()
        {
            if (Request.Path.ToLower() == "/login/community")
            {
                return Redirect("/Community");
            }
            return HttpNotFound();
        }
        public ActionResult HelpCenter()
        {
            if (Request.Path.ToLower() == "/login/helpcenter" || Request.Path.ToLower() == "/login/help-center")
            {
                return Redirect("/Help-Center");
            }
            return HttpNotFound();
        }
        public ActionResult Whoisitfor()
        {
            if (Request.Path.ToLower() == "/login/whoisitfor" || Request.Path.ToLower() == "/login/whos-it-for")
            {
                return Redirect("/Whos-it-for");
            }
            return HttpNotFound();
        }
        public ActionResult AttendanceRecords()
        {
            if (Request.Path.ToLower() == "/login/attendancerecords" || Request.Path.ToLower() == "/login/attendance-records")
            {
                return Redirect("/Attendance-Records");
            }
            return HttpNotFound();
        }
        public ActionResult EmployeeDatabase()
        {
            if (Request.Path.ToLower() == "/login/employeedatabase" || Request.Path.ToLower() == "/login/employee-database")
            {
                return Redirect("/Employee-Database");
            }
            return HttpNotFound();
        }
        public ActionResult HRPolicies()
        {
            if (Request.Path.ToLower() == "/login/hrpolicies" || Request.Path.ToLower() == "/login/hr-policies")
            {
                return Redirect("/HR-Policies");
            }
            return HttpNotFound();
        }
        public ActionResult PayrollSystem()
        {
            if (Request.Path.ToLower() == "/login/payrollsystem" || Request.Path.ToLower() == "/login/payroll-system")
            {
                return Redirect("/Payroll-System");
            }
            return HttpNotFound();
        }
        public ActionResult ESSPortal()
        {
            if (Request.Path.ToLower() == "/login/essportal" || Request.Path.ToLower() == "/login/ess-portal")
            {
                return Redirect("/ESS-Portal");
            }
            return HttpNotFound();
        }
        public ActionResult BlogDetail()
        {
            if (Request.Path.ToLower() == "/login/blogdetail" || Request.Path.ToLower() == "/login/what-is-a-biometric-attendance-system")
            {
                return Redirect("/What-is-a-Biometric-Attendance-System");
            }
            return HttpNotFound();
        }


        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken] 
        public JsonResult Index(forform model)
        {
            if (ModelState.IsValid)
            {
                string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ip))
                {
                    ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }

                string name = model.Name;
                string email = model.Email;
                string mess = model.Message;
                string phone = model.Phone;
                string html = @"<html>
                      <body>
                     <p>Date & Time : " + DateTime.Now + ",</p>" +
                        "<p>IP : " + ip + ",</p>" +
                          "<p>Customer Name: " + name + ",</p>" +
                          "<p>Customer Email: " + email + ",</p>" +
                        "<p>Customer Phone: " + phone + "</p>" +
                          "<p>Customer Message: " + mess + "</p>"
                        + "<p>Thank You,<br>"+ CSessionObjects.sCompanyName +"</br></p>"
                        + "</body>"
                        + "</html>";
                try
                {
                    string msgmail = SendMail("mail.timetrackpk.com", "noreply@timetrackpk.com", System.Configuration.ConfigurationManager.AppSettings["ToAdminEmail"], "TimeTrackPK Contact Us form Posted by [" + name + "]", html, "");
                }
                catch (Exception ex)
                {
                    return Json(ex.Message, JsonRequestBehavior.AllowGet);
                }

                return Json("1", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }

        }

        public string SendMail(string smtpclient, string from, string to, string subject, string body, string cc)
        {
            try
            {
                MailMessage mail = new MailMessage(from, to);
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = System.Configuration.ConfigurationManager.AppSettings["Smtp_Server"];
                client.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["IMail_Auth_User"], System.Configuration.ConfigurationManager.AppSettings["IMail_Auth_Password"]);
                //client. 
                mail.Subject = subject;// "this is a test email.";
                mail.Body = body;

                mail.IsBodyHtml = true;// true;
                if (cc != "")
                {
                    mail.CC.Add(cc);
                }
                client.Send(mail);
                return "Mail Send";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        [HttpGet]
        public ActionResult CompanyLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CompanyLogin(FormCollection form)
        {
            HRSystemEntities db = new HRSystemEntities();
            LoginModel Objlogin = new LoginModel();
            LoginLog objlog = new LoginLog();
            //            if (form[Constant.COMPANY_LOGIN_ID].Contains('@') == false)
            //            {
            //                if (Objlogin.validateEmp(form[Constant.COMPANY_LOGIN_ID], form[Constant.PASSWORD]))
            //                {
            //                    string CompID = "";
            //                    string CompLogo = "";
            //                    string EmpId = form[Constant.COMPANY_LOGIN_ID];
            //                    string EmpPass = form[Constant.PASSWORD];
            //                    var data = (from u in db.Users
            //                                join ca in db.CompanyAccounts on
            //             u.FkMachineId equals ca.FkCompanyId
            //                                where u.EnrollNumber == EmpId
            //&& u.Password == EmpPass
            //                                select new
            //                                { u.UserId, u.Name, u.designation, ca.CompanyLoginId, ca }).ToList();
            //                    //CompID = data[0].
            //                    CompanyUserRights objCompanyUserRights = new CompanyUserRights();
            //                    Int32 CompanyID = 0;
            //                    string compname = Objlogin.getCompanyName(data[0].ca.CompanyLoginId.ToString(), ref CompID, ref CompLogo);
            //                    CompanyID = Convert.ToInt32(CompID);
            //                    Objlogin.getCompanyUserRights(CompanyID, ref objCompanyUserRights);
            //                    ViewBag.Message = Constant.WELCOME + compname.ToString() + Constant.HOME;
            //                    CSessionObjects.sCompanyName = compname;
            //                    CSessionObjects.sCompanyID = CompID;
            //                    CSessionObjects.sCompanyLogo = CompLogo;
            //                    CSessionObjects.sEmpID = EmpId;
            //                    CSessionObjects.sEmpName = data[0].Name.ToString();
            //                    if (data[0].designation != null && data[0].designation != "")
            //                    {
            //                        CSessionObjects.sEmpdes = data[0].designation.ToString();
            //                    }


            //                    //  CSessionObjects.lCompanyUserRights = objCompanyUserRights;
            //                    Session["sessionCompanyUserRights"] = objCompanyUserRights;
            //                    Session["sessionCompanyLoginID"] = form[Constant.COMPANY_LOGIN_ID].ToString();
            //                    Session["sessionEmpId"] = EmpId;
            //                    Session["sessionName"] = data[0].Name.ToString();


            //                    CSessionObjects.sCompanyLoginID = form[Constant.COMPANY_LOGIN_ID].ToString();
            //                    return RedirectToAction("EmployeeHome");
            //                }
            //                else
            //                {
            //                    ViewBag.error = Constant.USERID_INCORRECT;
            //                    return View();
            //                }

            //            }
            //            else if (form[Constant.COMPANY_LOGIN_ID].Contains('@') == true)
            //            {
            //                if (Objlogin.ValidateCompany(form[Constant.COMPANY_LOGIN_ID], form[Constant.PASSWORD]))
            //                {
            //                    string CompID = "";
            //                    string CompLogo = "";
            //                    CompanyUserRights objCompanyUserRights = new CompanyUserRights();
            //                    Int32 CompanyID = 0;
            //                    string compname = Objlogin.getCompanyName(form[Constant.COMPANY_LOGIN_ID].ToString(), ref CompID, ref CompLogo);
            //                    CompanyID = Convert.ToInt32(CompID);
            //                    Objlogin.getCompanyUserRights(CompanyID, ref objCompanyUserRights);
            //                    ViewBag.Message = Constant.WELCOME + compname.ToString() + Constant.HOME;
            //                    CSessionObjects.sCompanyName = compname;
            //                    CSessionObjects.sCompanyID = CompID;
            //                    CSessionObjects.sCompanyLogo = CompLogo;
            //                    //  CSessionObjects.lCompanyUserRights = objCompanyUserRights;
            //                    Session["sessionCompanyUserRights"] = objCompanyUserRights;
            //                    Session["sessionCompanyLoginID"] = form[Constant.COMPANY_LOGIN_ID].ToString();
            //                    CSessionObjects.sCompanyLoginID = form[Constant.COMPANY_LOGIN_ID].ToString();

            //                    return RedirectToAction("Home");
            //                }
            //                else
            //                {
            //                    ViewBag.error = Constant.USERID_INCORRECT;
            //                    return View();
            //                }

            //        }
            //            else
            //            {
            //                ViewBag.error = "Please chose the type of login";
            //                return View();
            //    }

            string loginemail = form[Constant.COMPANY_LOGIN_ID];
            var emailverifydata = (from u in db.CompanyAccounts where u.CompanyLoginId == loginemail select u).ToList();
            if (emailverifydata.Count > 0)
            {
                if (Objlogin.ValidateCompany(form[Constant.COMPANY_LOGIN_ID], form[Constant.PASSWORD]))
                {
                    string CompID = "";
                    string CompLogo = "";
                    CompanyUserRights objCompanyUserRights = new CompanyUserRights();
                    Int32 CompanyID = 0;
                    string compname = Objlogin.getCompanyName(form[Constant.COMPANY_LOGIN_ID].ToString(), ref CompID, ref CompLogo);
                    CompanyID = Convert.ToInt32(CompID);
                    Objlogin.getCompanyUserRights(CompanyID, ref objCompanyUserRights);
                    ViewBag.Message = Constant.WELCOME + compname.ToString() + Constant.HOME;
                    CSessionObjects.sCompanyName = compname;
                    CSessionObjects.sCompanyID = CompID;
                    CSessionObjects.sCompanyLogo = CompLogo;
                    //  CSessionObjects.lCompanyUserRights = objCompanyUserRights;
                    Session["sessionCompanyUserRights"] = objCompanyUserRights;
                    Session["sessionCompanyLoginID"] = form[Constant.COMPANY_LOGIN_ID].ToString();
                    Session["userverify"] = "admin";
                    CSessionObjects.sCompanyLoginID = form[Constant.COMPANY_LOGIN_ID].ToString();

                    if (form["token"] != "")
                    {
                        string baseUrl = "http://" + Request.Url.Host;
                        string reciepturl = baseUrl + Url.Action("View", "ShowPaySlip") + "?token=" + form["token"];
                        return Redirect(reciepturl);
                    }
                    else
                    {
                        DateTime date = DateTime.Now;
                        string ipAddress = GetIpAddress();
                        objlog.CreateLoginLog(form[Constant.COMPANY_LOGIN_ID], compname, CompanyID, date, ipAddress, "Company Admin");

                        return RedirectToAction("Home");
                    }
                }
                else
                {
                    ViewBag.error = Constant.USERID_INCORRECT;

                    if (form["token"] != "")
                    {
                        string baseUrl = "http://" + Request.Url.Host;
                        string reciepturl = baseUrl + Url.Action("CompanyLogin", "Login") + "?token=" + form["token"];
                        return Redirect(reciepturl);
                    }
                    else
                    {
                        return View();
                    }
                }
            }
            else
            {
                if (Objlogin.validateEmp(form[Constant.COMPANY_LOGIN_ID], form[Constant.PASSWORD]))
                {
                    string CompID = "";
                    string CompLogo = "";
                    string EmpId = form[Constant.COMPANY_LOGIN_ID];
                    string EmpPass = form[Constant.PASSWORD];
                    var data = (from u in db.Users
                                join ca in db.CompanyAccounts on
                                u.FkMachineId equals ca.FkCompanyId
                                where u.workemail == EmpId
                                    && u.Password == EmpPass
                                select new
                                { u.UserId, u.Name, u.designation, ca.CompanyLoginId, ca, u.EnrollNumber }).ToList();
                    //CompID = data[0].
                    CompanyUserRights objCompanyUserRights = new CompanyUserRights();
                    EmpId = data[0].EnrollNumber;
                    Int32 CompanyID = 0;
                    string compname = Objlogin.getCompanyName(data[0].ca.CompanyLoginId.ToString(), ref CompID, ref CompLogo);
                    CompanyID = Convert.ToInt32(CompID);
                    Objlogin.getCompanyUserRights(CompanyID, ref objCompanyUserRights);
                    ViewBag.Message = Constant.WELCOME + compname.ToString() + Constant.HOME;
                    CSessionObjects.sCompanyName = compname;
                    CSessionObjects.sCompanyID = CompID;
                    CSessionObjects.sCompanyLogo = CompLogo;
                    CSessionObjects.sEmpID = EmpId;
                    CSessionObjects.sEmpName = data[0].Name.ToString();
                    if (data[0].designation != null && data[0].designation != "")
                    {
                        CSessionObjects.sEmpdes = data[0].designation.ToString();
                    }


                    //  CSessionObjects.lCompanyUserRights = objCompanyUserRights;
                    Session["sessionCompanyUserRights"] = objCompanyUserRights;
                    Session["sessionCompanyLoginID"] = form[Constant.COMPANY_LOGIN_ID].ToString();
                    Session["sessionEmpId"] = EmpId;
                    Session["sessionName"] = data[0].Name.ToString();
                    Session["userverify"] = "employee";
                    Session["userid"] = data[0].UserId;

                    CSessionObjects.sCompanyLoginID = form[Constant.COMPANY_LOGIN_ID].ToString();

                    if (form["token"] != "")
                    {
                        string baseUrl = "http://" + Request.Url.Host;
                        string reciepturl = baseUrl + Url.Action("View", "ShowPaySlip") + "?token=" + form["token"];
                        return Redirect(reciepturl);
                    }
                    else
                    {
                        DateTime date = DateTime.Now;
                        string ipAddress = GetIpAddress();
                        objlog.CreateLoginLog(form[Constant.COMPANY_LOGIN_ID], compname, CompanyID, date, ipAddress,"Employee");

                        return RedirectToAction("EmployeeHome");
                    }
                }
                else
                {
                    ViewBag.error = Constant.USERID_INCORRECT;
                    if (form["token"] != "")
                    {
                        string baseUrl = "http://" + Request.Url.Host;
                        string reciepturl = baseUrl + Url.Action("CompanyLogin", "Login") + "?token=" + form["token"];
                        return Redirect(reciepturl);
                    }
                    else
                    {
                        return View();
                    }
                }
            }
        }
        private string GetIpAddress()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (System.Net.IPAddress.TryParse(ip, out var ipAddress) && ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
            {

                ip = ipAddress.MapToIPv4().ToString();
            }

            return ip;
        }
        [SessionExpired]
        public ActionResult Home(bool? showabsent, int? location = 0)
        {
            int comid =Convert.ToInt32(CSessionObjects.sCompanyID);
            ViewBag.ShowAbsent = false;
            // ViewBag.Showalllocation = location == 0 ?  true: false;
            if (showabsent != null)
            {
                if (showabsent == true)
                { ViewBag.ShowAbsent = true; }
            }
            DateTime newdt;
            string dt = null;
            UserAttendanceUpdated ua = new UserAttendanceUpdated();
            UserAttendanceModel ObjUserAttendance = new UserAttendanceModel();
            List<sp_Attendance_Result> res = new List<sp_Attendance_Result>();
            if (string.IsNullOrWhiteSpace(dt))
            {
                newdt = DateTime.Now.Date;
                dt = newdt.ToString("dd/MM/yyyy");
            }
            else if (DateTime.TryParseExact(dt, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out newdt))
            {
            }
            else
            {
                ViewBag.lblDate = Constant.SELECT_DATE;
                goto A;
            }
            ViewBag.ShowDate1 = dt.ToString();
            res = ua.GetUserAttendances(newdt, newdt, null, null, null, Convert.ToInt32(CSessionObjects.sCompanyID), location == 0 ? null : location, Convert.ToBoolean(showabsent == null ? false : showabsent));
            //var LastUpdatedDate = db.Locations
            //.Where(l => l.companyid == comid)
            //.Select(l => l.LastSyncDate)
            //.FirstOrDefault();
            //if (LastUpdatedDate.HasValue)
            //{
            //    ViewBag.LastUpdatedDate = LastUpdatedDate.Value.ToString("dd MMMM yyyy hh:mm tt");
            //}
            //else
            //{
            //    ViewBag.LastUpdatedDate = "Not sync yet.";
            //}
            // Retrieve Name and LastSyncDate from the Locations table based on companyid
            var locationInfocom = db.Locations
                .Where(l => l.companyid == comid)
                .Select(l => new
                {
                    Name = l.name,
                    LastSyncDate = l.LastSyncDate
                })
                .ToList();

            if (locationInfocom.Any(info => info.LastSyncDate.HasValue))
            {
                // At least one non-null LastSyncDate is found
                var formattedInfo = locationInfocom
                    .Select(info => $"{info.Name}: {info.LastSyncDate?.ToString("dd MMMM yyyy hh:mm tt") ?? "Not sync yet."}")
                    .ToList();

                ViewBag.LastUpdatedDatesCom = formattedInfo;
            }
            else
            {
                // No LastSyncDate is found
                ViewBag.LastUpdatedDate = new List<string> { "Not sync yet." };
            }
            if (location == 0|| location==null)
            {
                //var LastUpdatedDate = db.Companies.Find(Convert.ToInt32(CSessionObjects.sCompanyID)).LastSyncDate;
                //if (LastUpdatedDate.HasValue)
                //{
                //    ViewBag.LastUpdatedDates = LastUpdatedDate.Value.ToString("dd MMMM yyyy hh:mm tt");
                //}
                //else
                //{
                //    ViewBag.LastUpdatedDates= "Not sync yet.";
                //}
                var locationInfocomtest = db.Locations
              .Where(l => l.companyid == comid)
              .Select(l => new
              {
                  Name = l.name,
                  LastSyncDate = l.LastSyncDate
              })
              .ToList();

                if (locationInfocomtest.Any(info => info.LastSyncDate.HasValue))
                {
                    // At least one non-null LastSyncDate is found
                    var formattedInfo = locationInfocomtest
                        .Select(info => $"{info.Name}: {info.LastSyncDate?.ToString("dd MMMM yyyy hh:mm tt") ?? "Not sync yet."}")
                        .ToList();

                    ViewBag.LastUpdatedDate = formattedInfo;
                }
                else
                {
                    // No LastSyncDate is found
                    ViewBag.LastUpdatedDate = new List<string> { "Not sync yet." };
                }
            }
            else
            {
                var locationInfo = db.Locations
                .Where(l => l.companyid == comid && l.id==location)
                .Select(l => new
                {
                    Name = l.name,
                    LastSyncDate = l.LastSyncDate
                })
                .ToList();

                if (locationInfo.Any(info => info.LastSyncDate.HasValue))
                {
                    // At least one non-null LastSyncDate is found
                    var formattedInfo = locationInfo
                        .Select(info => $"{info.Name}: {info.LastSyncDate?.ToString("dd MMMM yyyy hh:mm tt") ?? "Not sync yet."}")
                        .FirstOrDefault();

                    ViewBag.LastUpdatedDate = formattedInfo;
                }
                else
                {
                    // No LastSyncDate is found
                    ViewBag.LastUpdatedDate = new List<string> { "Not sync yet." };
                }
            }
           
            var userlist = ua.Userlist(null, null, null, Convert.ToInt32(CSessionObjects.sCompanyID));

            var PCount = (from x in res where x.status_ == "P" || x.status_ == "Late" select x).ToList().Count.ToString();
            var ACount = (from c in userlist
                          where !(from r in res
                                  where r.status_ != "Absent"
                                  select r.EnrollNumber).ToArray().Contains(c.EnrollNumber)
                          select c.Name ).ToList().Count.ToString();
            ViewBag.SummaryDate = newdt.ToString();

            ViewBag.day = newdt.DayOfWeek.ToString();
            ViewBag.Date = newdt.Day + "' " + newdt.ToString("MMMM") + " " + newdt.Year;
            CSessionObjects.sAbsentNames = new System.Collections.ArrayList((from c in userlist
                                                                             where !(from r in res
                                                                                     where r.status_ != "Absent"
                                                                                     select r.EnrollNumber).ToArray().Contains(c.EnrollNumber)
                                                                             select c.Name
                                                                             ).ToList());
            ViewBag.TotalEmployee = userlist.Count;
            ViewBag.Present = PCount;
            ViewBag.Absent = ACount;
            if (res.Count > 0)
            {
                CSessionObjects.sPresentNames = new System.Collections.ArrayList((from data in res where data.status_ == "P" || data.status_ == "Late" select data.Name).ToList());
                CSessionObjects.sPresentTime = new System.Collections.ArrayList((from data in res where data.status_ == "P" || data.status_ == "Late" select data.CheckedIn.Value.ToString("hh:mm tt")).ToList());

                var _totalCount = userlist.Count;
                var _presentPercent = Math.Round((Double.Parse(PCount) * 100) / _totalCount, 2);
                var _absentPercent = Math.Round((Double.Parse(ACount) * 100) / _totalCount, 2);
                var _late_count = (from x in res where x.status_ == "Late" select x).ToList().Count;
                var _on_time_count = (from x in res where x.status_ == "P" select x).ToList().Count;
                var _onTimePercent = Math.Round((_on_time_count * 100) / Double.Parse(PCount), 2);
                var _latePercent = Math.Round((_late_count * 100) / Double.Parse(PCount), 2);

                ViewBag.PercentPresent = _presentPercent.ToString();
                ViewBag.PercentAbsent = _absentPercent.ToString();
                ViewBag.PercentOnTime = _onTimePercent.ToString();
                ViewBag.PercentLate = _latePercent.ToString();

                ViewBag.AttendanceScript = "<script> var config1 = { " +
                    "type: 'pie', " +
                    "data: " +
                    "{ " +
                    "datasets: [{ " +
                    "data: [ " +
                    ViewBag.PercentPresent + ", " +
                    ViewBag.PercentAbsent + "" +
                    "], " +
                    "backgroundColor: [ " +
                     "\"#33bf1b\", " +
                    "\"#d25b47\"" +
                    "], }], " +
                    "labels: [ " +
                    "\"Present\", " +
                    "\"Absent\"" +
                    "] }, " +
                    "options: {" +
                    "title:" +
                    "{" +
                        "display: true," +
                        "text: 'Attendance'" +
                    "}" +
                    "}" +
                    "}; </script>";

                ViewBag.AttendanceScript += "<script> var config2 = { " +
                    "type: 'pie', " +
                    "data: " +
                    "{ " +
                    "datasets: [{ " +
                    "data: [ " +
                    ViewBag.PercentOnTime + ", " +
                    ViewBag.PercentLate + "" +
                    "], " +
                    "backgroundColor: [ " +
                     "\"#dadada\", " +
                    "\"#a0a0a0\"" +
                    "], }], " +
                    "labels: [ " +
                    "\"On Time\", " +
                    "\"Late\"" +
                    "] }, " +
                    "options: {" +
                    "title:" +
                    "{" +
                        "display: true," +
                        "text: 'Punctuality'" +
                    "}" +
                    "}" +
                    "}; " +
                    "window.onload = function() { " +
                    "var ctx1 = document.getElementById(\"pieChart1\").getContext(\"2d\"); " +
                    "window.myPie1 = new Chart(ctx1, config1); " +
                    "var ctx2 = document.getElementById(\"pieChart2\").getContext(\"2d\"); " +
                    "window.myPie2 = new Chart(ctx2, config2); " +
                    "};</script>";
            }
        A:
            string day = newdt.Day.ToString();
            string dayExt = getDayOfMonthSuffix(newdt.Day);
            string Month = newdt.ToString("MMMM");
            string Year = newdt.Year.ToString();
            ViewBag.HeadingDate = day + dayExt + " " + Month + ", " + Year;
            if (db.Locations.AsEnumerable().Where(x => x.companyid == Convert.ToInt32(CSessionObjects.sCompanyID)).ToList().Count() > 1)
            {

                List<SelectListItem> list = ObjUserAttendance.getLocation();
                list.Insert(0, new SelectListItem() { Text = "ALL", Value = "0" });
                ViewBag.DropDownValues = new SelectList(list, "Value", "Text", location);
            }
            List<SelectListItem> locationlist = ObjUserAttendance.getLocation();
            ViewBag.locationList = locationlist;
            return View("Home", res);

        }

        [SessionExpired]
        public ActionResult EmployeeHome()
        {
            HRSystemEntities db = new HRSystemEntities();
            UserAttendanceModel ObjUserAttendance = new UserAttendanceModel();
            List<SelectListItem> listItem = null;

            DateTime newdts;
            DateTime newdtE;
            //ViewBag.DropDownValues = new SelectList(listItem, "Value", "Text");

            string sDate = DateTime.Now.AddDays(-7).Date.ToString("dd/MM/yyyy");

            if (DateTime.TryParseExact(sDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out newdts)) { }
            string eDate = DateTime.Now.ToString("dd/MM/yyyy");
            if (DateTime.TryParseExact(eDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out newdtE)) { }

            if (newdts > newdtE)
            {
                ViewBag.lblDateRange = Constant.SELECT_DATERANGE;
                return View("ViewDetail");
            }


            ViewBag.AttendStart = sDate;
            ViewBag.AttendEnd = eDate;
            if (newdtE > DateTime.Now)
            {
                eDate = DateTime.Now.Date.ToShortDateString();
                newdtE = DateTime.Now;
            }

            List<AttendanceDetailsModel> attendanceListByEmployee = null;
            if (ModelState.IsValid)
            {



                UserModel Ouser = new UserModel();
                string enroll = CSessionObjects.sEmpID;//Ouser.GetUserEnroll(Convert.ToInt32(CSessionObjects.sEmpID));
                string name = CSessionObjects.sEmpName;//Ouser.GetUserName(Convert.ToInt32(CSessionObjects.sEmpID));
                string sDateToShow = sDate;// frm[2].ToString().Split(new string[] { " " }, StringSplitOptions.None)[0];
                string eDateToShow = eDate;// frm[3].ToString().Split(new string[] { " " }, StringSplitOptions.None)[0];
                string nodata = "";
                attendanceListByEmployee = ObjUserAttendance.GetUserAttendanceList(enroll, newdts.ToString("dd/MM/yyyy"), newdtE.ToString("dd/MM/yyyy"), ref nodata);
                if (nodata != "")
                {
                    ViewBag.Error = nodata;
                }
                else
                {
                    //Showing Heading and selected Employee,Date


                    //string Sday = Convert.ToDateTime(sDateToShow).Day.ToString();
                    //string SdayExt = getDayOfMonthSuffix(newdts.Day);
                    //string SMonth = Convert.ToDateTime(sDateToShow).ToString("MMMM");
                    //string SYear = Convert.ToDateTime(sDateToShow).Year.ToString();

                    //End date

                    //string Eday = Convert.ToDateTime(eDateToShow).Day.ToString();
                    //string EdayExt = getDayOfMonthSuffix(newdtE.Day);

                    //string EMonth = Convert.ToDateTime(eDateToShow).ToString("MMMM");
                    //string EYear = Convert.ToDateTime(eDateToShow).Year.ToString();

                    ViewBag.EmpName = name;
                    ViewBag.EnrollNo = enroll;
                    ViewBag.Startdate = newdts.Day + getDayOfMonthSuffix(newdts.Day) + " " + newdts.ToString("MMMM") + ", " + newdts.Year;
                    ViewBag.Enddate = newdtE.Day + getDayOfMonthSuffix(newdtE.Day) + " " + newdtE.ToString("MMMM") + ", " + newdtE.Year;
                    // ViewBag.TotalDays = DateTime.ParseExact(eDate, "dd/MM/yyyy", null).Subtract(DateTime.ParseExact(sDate, "dd/MM/yyyy", null)).TotalDays + 1;
                    ViewBag.TotalDays = newdtE.Subtract(newdts).TotalDays + 1;// DateTime.ParseExact(new, "dd/MM/yyyy", null).Subtract(DateTime.ParseExact(sDate, "dd/MM/yyyy", null)).TotalDays + 1;
                    int holidayscount = (from x in attendanceListByEmployee where x.IsHoliday == 1 select x.IsHoliday).ToList().Count;
                    if (attendanceListByEmployee.Count() > 0)
                    {
                        ViewBag.TotalWorkingDays = ViewBag.TotalDays - holidayscount;
                        int natioanlholidayscount = (from x in attendanceListByEmployee where x.IsHoliday == 1 && (x.HolidayType == "NonRecurring" || x.HolidayType == "Yearly") select x.IsHoliday).ToList().Count;
                        int weeklyoff = (from x in attendanceListByEmployee where x.IsHoliday == 1 && (x.HolidayType == "Weekly") select x.IsHoliday).ToList().Count;
                        ViewBag.NationalHolidays = natioanlholidayscount;
                        ViewBag.WeeklyOff = weeklyoff;
                    }
                    int fkcompanyid = Convert.ToInt32(CSessionObjects.sCompanyID);

                    int userid = (from y in db.Users where y.EnrollNumber == enroll && y.FkMachineId == fkcompanyid select y.UserId).SingleOrDefault();
                    ViewBag.ID = userid;
                    ViewBag.Phone = (from x in attendanceListByEmployee select x.Phone).FirstOrDefault();

                    if (attendanceListByEmployee.Count > 0)
                    {
                        var _total_count = (from x in attendanceListByEmployee where x.IsHoliday == 0 select x).ToList().Count;
                        var _late_count = (from x in attendanceListByEmployee where x.Late == "Late" && x.Time_In != "---" select x).ToList().Count;
                        var _on_time_cout = (from x in attendanceListByEmployee where x.Late == "On Time" && x.Time_In != "---" select x).ToList().Count;
                        var _present_count = _late_count + _on_time_cout;
                        var _absent_count = (from x in attendanceListByEmployee where x.Time_In == "---" && x.Time_Out == "---" && x.IsHoliday == 0 select x).ToList().Count;
                        var _presentPercent = Math.Round((Double.Parse(_present_count.ToString()) * 100) / _total_count, 2);
                        var _absentPercent = Math.Round((Double.Parse(_absent_count.ToString()) * 100) / _total_count, 2);
                        var _onTimePercent = Math.Round((_on_time_cout * 100) / Double.Parse(_total_count.ToString()), 2);
                        var _latePercent = Math.Round((_late_count * 100) / Double.Parse(_total_count.ToString()), 2);
                        var sickleavecount = (from x in attendanceListByEmployee where x.Time_In == "---" && x.Time_Out == "---" && x.IsHoliday == 0 && x.status == 1 select x).ToList().Count;
                        var casualleavecount = (from x in attendanceListByEmployee where x.Time_In == "---" && x.Time_Out == "---" && x.IsHoliday == 0 && x.status == 2 select x).ToList().Count;
                        var absentleavecount = (from x in attendanceListByEmployee where x.Time_In == "---" && x.Time_Out == "---" && x.IsHoliday == 0 && x.status == 0 select x).ToList().Count;
                        var annualleavecount = (from x in attendanceListByEmployee where x.Time_In == "---" && x.Time_Out == "---" && x.IsHoliday == 0 && x.status == 3 select x).ToList().Count;

                        ViewBag.SickLeaveCount = sickleavecount;
                        ViewBag.CasualLeaveCount = casualleavecount;
                        ViewBag.AbsentLeavecount = _absent_count - absentleavecount;
                        ViewBag.AnnualLeavecount = annualleavecount;
                        ViewBag.Late = _late_count.ToString();
                        ViewBag.OnTime = _on_time_cout.ToString();
                        ViewBag.Present = _present_count.ToString();
                        ViewBag.Absent = (_absent_count - (sickleavecount + casualleavecount + annualleavecount)).ToString();
                        ViewBag.PercentPresent = _presentPercent.ToString();
                        ViewBag.PercentAbsent = _absentPercent.ToString();
                        ViewBag.PercentOnTime = _onTimePercent.ToString();
                        ViewBag.PercentLate = _latePercent.ToString();

                        ViewBag.AttendanceScript = "<script> var config1 = { " +
                        "type: 'pie', " +
                        "data: " +
                        "{ " +
                        "datasets: [{ " +
                        "data: [ " +
                        _presentPercent + ", " +
                        _absentPercent + "" +
                        "], " +
                        "backgroundColor: [ " +
                         "\"#33bf1b\", " +
                        "\"#d25b47\"" +
                        "], }], " +
                        "labels: [ " +
                        "\"Present\", " +
                        "\"Absent\"" +
                        "] }, " +
                        "options: {" +
                        "title:" +
                        "{" +
                            "display: true," +
                            "text: 'Attendance'" +
                        "}" +
                        "}" +
                        "}; </script>";

                        ViewBag.AttendanceScript += "<script> var config2 = { " +
                            "type: 'pie', " +
                            "data: " +
                            "{ " +
                            "datasets: [{ " +
                            "data: [ " +
                            _onTimePercent + ", " +
                            _latePercent + "" +
                            "], " +
                            "backgroundColor: [ " +
                             "\"#dadada\", " +
                            "\"#a0a0a0\"" +
                            "], }], " +
                            "labels: [ " +
                            "\"On Time\", " +
                            "\"Late\"" +
                            "] }, " +
                            "options: {" +
                            "title:" +
                            "{" +
                                "display: true," +
                                "text: 'Punctuality'" +
                            "}" +
                            "}" +
                            "}; " +
                            "window.onload = function() { " +
                            "var ctx1 = document.getElementById(\"pieChart1\").getContext(\"2d\"); " +
                            "window.myPie1 = new Chart(ctx1, config1); " +
                            "var ctx2 = document.getElementById(\"pieChart2\").getContext(\"2d\"); " +
                            "window.myPie2 = new Chart(ctx2, config2); " +
                            "};</script>";
                    }


                }
                //ViewBag.ShowDate = sDateToShow + " - " + eDateToShow;
                ViewBag.ShowSDate = sDateToShow;
                ViewBag.ShowEDate = eDateToShow;
                // ViewBag.DropDownValues = new SelectList(listItem, "Value", "Text", CSessionObjects.sEmpName.ToString());




                ViewBag.LeaveScript = LeaveTypeModel.GetLeaveTypeScript();
                return View("EmployeeHome", attendanceListByEmployee);
            }

            return View("EmployeeHome");
        }

        public string getDayOfMonthSuffix(int n)
        {

            if (n >= 11 && n <= 13)
            {
                return "th";
            }
            switch (n % 10)
            {
                case 1: return "st";
                case 2: return "nd";
                case 3: return "rd";
                default: return "th";
            }
        }
        public ActionResult AdminDirectLogin(int id = 0)
        {
            LoginModel Objlogin = new LoginModel();

            string complogin = "";
            Company comp = Objlogin.getCompanyByID(id, ref complogin);
            if (complogin != Constant.COMP_ACC_NOT_EXISTS)
            {
                ViewBag.Message = Constant.WELCOME + comp.CompanyName.ToString() + Constant.HOME;
                CSessionObjects.sCompanyName = comp.CompanyName;
                CSessionObjects.sCompanyID = id.ToString();
                CSessionObjects.sCompanyLogo = comp.Logo;
                Session["sessionCompanyLoginID"] = complogin.ToString();
                CSessionObjects.sCompanyLoginID = complogin.ToString();
                Session["userverify"] = "admin";
                CompanyUserRights objCompanyUserRights = new CompanyUserRights();
                Objlogin.getCompanyUserRights(id, ref objCompanyUserRights);
                Session["sessionCompanyUserRights"] = objCompanyUserRights;

                return RedirectToAction("Home");
            }
            else
            {
                return RedirectToAction("View", "Company", new { error = 1 });
            }
        }

        [SessionExpired]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [SessionExpired]
        public ActionResult ChangePassword(ChangePasswordModel changePassword)
        {
            if (ModelState.IsValid)
            {
                LoginModel Objlogin = new LoginModel();
                string Ret_result = "";
                string viewbagerror = "";
                var userid = CSessionObjects.sCompanyLoginID.ToString();
                int companyid = Convert.ToInt32(CSessionObjects.sCompanyID);
                //if (Session["userverify"].ToString() == "employee")
                if(Session["userverify"] != null)
                {
                    User data = (from u in db.Users where u.workemail == userid && u.FkMachineId == companyid select u).SingleOrDefault();

                    data.Password = changePassword.NewPassword.ToString();
                    db.Entry(data).State = EntityState.Modified;
                    db.SaveChanges();
                    viewbagerror = Constant.PASSWORD_CHANGED;
                }
                else
                {
                    viewbagerror = Objlogin.AssignChangePasswordValues(changePassword, ref Ret_result);
                }


                ViewBag.result = Ret_result;
                ViewBag.error = viewbagerror;

            }

            return View();
        }


        public ActionResult SignOut()
        {
            CSessionObjects.RemoveAll();
            return RedirectToAction("CompanyLogin");
        }


        public ActionResult unauthorize()
        {
            return View();
        }

        [SessionExpired]
        [HttpGet]
        public ActionResult Notification()
        {
            return View(GetNotificationData());
        }
        public EmailSettingModel GetNotificationData()
        {
            int companyid = Convert.ToInt32(CSessionObjects.sCompanyID);

            var EmailSettingData = (from u in db.EmailSettings where u.FkCompanyId == companyid select u).ToList();
            var CompanyData = (from c in db.Companies where c.CompanyId == companyid select c).ToList();

            EmailSettingModel objmodel = new EmailSettingModel();

            if (CompanyData.Count > 0)
            {
                objmodel.FkCompanyId = CompanyData[0].CompanyId;
                objmodel.IsAdminAllowEmail = CompanyData[0].IsAdminAllowEmail;
                objmodel.IsAttendanceSummary = CompanyData[0].AttendanceSummary;
                objmodel.IsPayrollEmail = CompanyData[0].PayrollEmail;
                objmodel.IsLeaveEmail = CompanyData[0].LeaveEmail;
                objmodel.IsLateComerEmail = CompanyData[0].LateComerEmail;
            }


            if (EmailSettingData.Count > 0)
            {
                objmodel.FkCompanyId = EmailSettingData[0].FkCompanyId;
                objmodel.AttendanceSummaryEmail = EmailSettingData[0].AttendanceSummaryEmail;
                objmodel.PayrollEmail = EmailSettingData[0].PayrollEmail;
                objmodel.LeaveEmail = EmailSettingData[0].LeaveEmail;
                objmodel.LateComerEmail = EmailSettingData[0].LateComerEmail;
            }

            return objmodel;

        }




        [SessionExpired]

        [HttpPost]
        public ActionResult SaveNotificationData(string Type, string Email, bool IsEmailAllow)
        {
            try
            {
                int companyid = Convert.ToInt32(CSessionObjects.sCompanyID);
                EmailSettingModel Obj = new EmailSettingModel();

                Obj.Save(Type, Email, IsEmailAllow, companyid);

                ViewBag.result = Type + " Setting Saved Successfully";

                // return RedirectToAction("Notification");
                //return View("Notification", GetNotificationData());
                return Json(new { success = true, message = "Notification saved successfully" });
            }
            catch (Exception ex)
            {
                ViewBag.error = Type + " setting not saved, please contact administrator " + ex.ToString();
                return Json(new { success = false, message = "Failed to saved Notification" });
            }
            

        }

        //[HttpPost]
        //[SessionExpired]
        //public ActionResult Notification(string PayrollEmail)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        int companyid = Convert.ToInt32(CSessionObjects.sCompanyID);
        //        AccountEmailModel Obj = new AccountEmailModel();

        //    }

        //    return View();
        //}


        [SessionExpired]
        public ActionResult AccountEmail()
        {
            int companyid = Convert.ToInt32(CSessionObjects.sCompanyID);
            var data = (from u in db.AccountEmails where u.fkcompanyid == companyid select u).ToList();
            AccountEmailModel objmodel = new AccountEmailModel();
            if (data.Count > 0)
            {
                objmodel.fkcompanyid = data[0].fkcompanyid;
                objmodel.id = data[0].id;
                objmodel.leaveemail = data[0].leaveemail;
                objmodel.payrollemail = data[0].payrollemail;

            }
            return View(objmodel);
        }

        [HttpPost]
        [SessionExpired]
        public ActionResult AccountEmail(AccountEmailModel accountemail)
        {
            if (ModelState.IsValid)
            {
                int companyid = Convert.ToInt32(CSessionObjects.sCompanyID);
                AccountEmailModel Obj = new AccountEmailModel();

                var data = (from u in db.AccountEmails where u.fkcompanyid == companyid select u).ToList();
                string Ret_result = "";
                string viewbagerror = "";
                if (data.Count > 0)
                {
                    Obj.id = data[0].id;
                    Obj.fkcompanyid = data[0].fkcompanyid;
                    Obj.leaveemail = accountemail.leaveemail;
                    Obj.payrollemail = accountemail.payrollemail;
                    Obj.PostEdit(Obj);
                    Ret_result = "Email successfully updated";
                }
                else
                {
                    Obj.PostCreate(accountemail);
                    Ret_result = "Email successfully added";
                }




                ViewBag.result = Ret_result;
                ViewBag.error = viewbagerror;
            }

            return View();
        }
        public ActionResult LoginLogs()
        {
            List<LoginLog> loginLogs;

            using (var db = new HRSystemEntities())
            {
                var query = from log in db.LoginLogs
                            join company in db.Companies on log.CompanyID equals company.CompanyId orderby log.DateTime descending  
                            select new
                            {
                                CompanyName = company.CompanyName,
                                LoginEmail = log.LoginEmail,
                                DateTime = log.DateTime,
                                IP = log.IP,
                                UserRole =log.UserRole
                               
                            };

                loginLogs = query.AsEnumerable().Select(x => new LoginLog
                {
                    CompanyName = x.CompanyName,
                    LoginEmail = x.LoginEmail,
                    DateTime = x.DateTime,
                    IP=x.IP,
                    UserRole=x.UserRole
                  
                }).ToList();

                var uniqueCompanyNames = loginLogs.Select(log => log.CompanyName).Distinct().ToList();

                ViewBag.CompanyList = uniqueCompanyNames;
            }

            return View(loginLogs);
        }


    }

}
