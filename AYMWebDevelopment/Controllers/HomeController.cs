using AYMWebDevelopment.Areas.AllMyAPI.Data;
using AYMWebDevelopment.Models;
using AYMWebDevelopment.Models.ModelDTO;
using CaptchaMvc.HtmlHelpers;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace AYMWebDevelopment.Controllers
{
    public class HomeController : Controller
    {
        AYMCOMPDataContext mydb = new AYMCOMPDataContext();
        string Link = APILinks.GetAPILINK(1);
        private readonly IRepository<EmployeeTBL> EmpRepository;

        public HomeController(IRepository<EmployeeTBL> EmpRepository/*, IRepository<InternalChatTBL> InternalChatRepository*/)
        {
            this.EmpRepository = EmpRepository;
            //this.InternalChatRepository = InternalChatRepository;
        }

        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                //var x = UnityConfig.RegisterComponents(); // *******

                var lst = EmpRepository.GetAll("Employee");
                //EmployeeTBL aym = new EmployeeTBL();
                //aym.AutoCode = "ay123456";
                //var lst2 = EmpRepository.AddNewRow("Employee", aym);
                //var lst2 = InternalChatRepository.GetAll("InternalChat");

                //HttpClient h = new HttpClient();
                //h.BaseAddress = new Uri(Link);
                //var res = h.GetAsync("Employee").Result;
                //var lst = res.Content.ReadAsAsync<List<EmployeeTBL>>().Result;

                //HttpClient h = new HttpClient();
                //h.BaseAddress = new Uri(Link);
                //var res = h.GetAsync("Employee").Result;
                //var lst = res.Content.ReadAsAsync<List<EmployeeTBL>>().Result.OrderByDescending(x => x.DateOfMaking);
                //List<EmployeeDTO> mynewlst = AutoMapper.Mapper.Map<List<EmployeeDTO>>(lst);

                //List<EmployeeTBL> mynewlst2 = AutoMapper.Mapper.Map<List<EmployeeTBL>>(mynewlst);
                if (lst.Count() > 0)
                {
                    //ViewBag.count = lst.Count();

                    return View();
                }
                else
                {
                    Session["myerrordesc"] = "Application Cannot Connect to Database, Please contact the developer ASAP.";
                    return RedirectToAction("ErrorDesc");
                }

            }
            catch (Exception ex)
            {
                Session["myerrordesc"] = ex;
                return RedirectToAction("ErrorDesc");
            }
        }


        // Login
        #region LOGIN & LOGOUT
        [HttpGet]
        [AllowAnonymous]
        [CheckSessionUsronline]
        public ActionResult LoginPage(int recopass = 0)
        {
            try
            {
                if (recopass == 1)
                {
                    ViewBag.Successful = "Reset Password has been sent to you, Please check your email address.";
                }
                else if (recopass == 2)
                {
                    ViewBag.Successful = "Your Password has been changed successfully.";
                }
                return View();
            }
            catch (Exception ex)
            {
                Session["myerrordesc"] = ex;
                return RedirectToAction("ErrorDesc");
            }
        }


        [HttpPost]
        [CheckSessionUsronline]
        [ValidateAntiForgeryToken]
        public ActionResult LoginPage(ForLoginData aym, string ReturnUrl)
        {
            try
            {
                // Code for validating the Captcha  
                if (this.IsCaptchaValid("Validate your captcha"))
                {
                    HttpClient h = new HttpClient();
                    h.BaseAddress = new Uri(Link);
                    string newpassword = HashMD5AYM.GenerateNewPasswordMD5(aym.loginpassword);
                    bool user = userexists(aym.loginemail, newpassword);
                    if (user)
                    {
                        //.
                        string API = "Employee";
                        var res = h.GetAsync(API + "?LoginEmail=" + aym.loginemail + "&Password=" + newpassword + "&type=2").Result;
                        var lst = res.Content.ReadAsAsync<EmployeeTBL>().Result;
                        if (lst.IsStopped)
                        {
                            ViewBag.message = "This account was stopped by Admin, Please contact them.";
                            return View(aym);
                        }
                        string MySession = startlogin(lst.EmployeeID);
                        FormsAuthentication.SetAuthCookie(MySession, false);

                        string decodedUrl = "";
                        if (!string.IsNullOrEmpty(ReturnUrl))
                            decodedUrl = Server.UrlDecode(ReturnUrl);

                        //Login logic...

                        if (Url.IsLocalUrl(decodedUrl))
                        {
                            return Redirect(decodedUrl);
                        }
                        else
                        {
                            return RedirectToAction("MyProfile", "Cpanel", new { Area = "AllMyMVC" });
                        }

                    }
                    else
                    {
                        ViewBag.message = "There is no account with this details";
                        return View(aym);
                    }
                }
                else
                {
                    ViewBag.message = "Please Retry captcha!.";
                    return View(aym);
                }

            }
            catch (Exception ex)
            {
                Session["myerrordesc"] = ex;
                return RedirectToAction("ErrorDesc");
            }

        }

        //
        private bool userexists(string email, string password)
        {

            HttpClient h = new HttpClient();
            h.BaseAddress = new Uri(Link);

            string API = "Employee";
            var res = h.GetAsync(API + "?LoginEmail=" + email + "&Password=" + password + "&type=1").Result;
            var lst = res.Content.ReadAsAsync<bool>().Result;

            if (lst)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //

        //
        private string startlogin(int EmployeeID)
        {
            HttpClient h = new HttpClient();
            h.BaseAddress = new Uri(Link);

            string APISAuthPinCode = CurrentUserLoginData.getcurrentusrdataAuth(8);
            string APISAuthPassword = CurrentUserLoginData.getcurrentusrdataAuth(9);
            EmployeeLoginTBL mymodel = new EmployeeLoginTBL();
            mymodel.EmployeeIDD = EmployeeID;
            mymodel.LoginTime = DateTime.Now;
            mymodel.LogoutTime = null;
            HttpRequestBase request1 = Request;
            mymodel.LoginIPAddress = GetClientMacAndIP.GetClientIpAddress(request1);
            mymodel.SessionUnique = MySPECIALGUID.GenerateGUID(151, 101);
            string API2 = "EmployeeLogin";
            var res2 = h.PostAsJsonAsync(API2 + "?Addnew=1&APISAuthPinCode=" + APISAuthPinCode + "&APISAuthPassword=" + APISAuthPassword, mymodel).Result;
            var lst1 = res2.Content.ReadAsAsync<string>().Result;
            var result = JsonConvert.DeserializeObject<EmployeeLoginTBL>(lst1);
            return result.SessionUnique;
        }
        //


        [HttpGet]
        [Authorize]
        public ActionResult LogOut()
        {
            try
            {
                HttpClient h = new HttpClient();
                h.BaseAddress = new Uri(Link);

                string APISAuthPinCode = CurrentUserLoginData.getcurrentusrdataAuth(8);
                string APISAuthPassword = CurrentUserLoginData.getcurrentusrdataAuth(9);

                string currentsession = CurrentUserLoginData.getcurrentusrdataAuth(7);

                if (currentsession != null && currentsession != "0")
                {
                    string API = "EmployeeLogin";
                    var res = h.GetAsync(API + "?sessionUnique=" + currentsession + "&type=1").Result;
                    var session = res.Content.ReadAsAsync<EmployeeLoginTBL>().Result;

                    session.LogoutTime = DateTime.Now;

                    var res2 = h.PostAsJsonAsync(API + "?EditExists=1&APISAuthPinCode=" + APISAuthPinCode + "&APISAuthPassword=" + APISAuthPassword, session).Result;
                    var lst1 = res2.Content.ReadAsAsync<string>().Result;
                    var result = JsonConvert.DeserializeObject<EmployeeLoginTBL>(lst1);
                }

                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Home", new { Area = "" });
            }
            catch (Exception ex)
            {
                Session["myerrordesc"] = ex;
                return RedirectToAction("ErrorDesc");
            }

        }
        //
        #endregion

        [HttpGet]
        public ActionResult Projects()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Session["myerrordesc"] = ex;
                return RedirectToAction("ErrorDesc");
            }
        }

        //[HttpGet]
        //public ActionResult Clients()
        //{
        //    try
        //    {
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        Session["myerrordesc"] = ex;
        //        return RedirectToAction("ErrorDesc");
        //    }
        //}

        [HttpGet]
        public ActionResult AboutUS()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Session["myerrordesc"] = ex;
                return RedirectToAction("ErrorDesc");
            }
        }

        [HttpGet]
        public ActionResult Motivation()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Session["myerrordesc"] = ex;
                return RedirectToAction("ErrorDesc");
            }
        }

        #region Careers
        [HttpGet]
        public ActionResult Careers(int recopass = 0)
        {
            try
            {
                if (recopass == 1)
                {
                    ViewBag.Successful = "Your C.V/Resume has been sent to us, We will contact you according to our needs.";
                }
                return View();
            }
            catch (Exception ex)
            {
                Session["myerrordesc"] = ex;
                return RedirectToAction("ErrorDesc");
            }
        }

        [HttpPost]
        public ActionResult Careers(string candidatename = null, string candidateemail = null, string candidateposition = null, HttpPostedFileBase Image1 = null)
        {
            try
            {
                    if (!string.IsNullOrEmpty(candidatename) && !string.IsNullOrEmpty(candidateemail) && !string.IsNullOrEmpty(candidateposition) && Image1 != null && Image1.ContentLength > 0)
                    {
                        CareerTBL myobject = new CareerTBL();

                        if (Path.GetExtension(Image1.FileName) == ".pdf" || Path.GetExtension(Image1.FileName) == ".PDF")
                        {
                            string fullPath = Request.MapPath("~/Images/Candidate/" + myobject.CandidateFileURL);
                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
                            if (!Directory.Exists("~/Images/Candidate"))
                            {
                                Directory.CreateDirectory(Server.MapPath("~/Images/Candidate"));
                            }
                            string _FileName = "Candidate" + Guid.NewGuid() + Path.GetExtension(Image1.FileName);
                            string _path = Path.Combine(Server.MapPath("~/Images/Candidate/"), _FileName);
                            Image1.SaveAs(_path);
                            myobject.CandidateFileURL = _FileName;
                        }

                    myobject.AutoCode = "CAND" + MySPECIALGUID.GetUniqueKey(9);
                    myobject.DateOfMaking = DateTime.Now;
                        myobject.CandidateName = candidatename;
                        myobject.CandidateEmail = candidateemail;
                        myobject.CandidatePosition = candidateposition;
                        HttpRequestBase request1 = Request;
                        myobject.CandidateIPAddress = GetClientMacAndIP.GetClientIpAddress(request1);

                        string API = "Career";
                        HttpClient h = new HttpClient();
                        h.BaseAddress = new Uri(Link);
                        string APISAuthPinCode = CurrentUserLoginData.getcurrentusrdataAuth(8);
                        string APISAuthPassword = CurrentUserLoginData.getcurrentusrdataAuth(9);

                        var res = h.PostAsJsonAsync(API + "?Addnew=1&APISAuthPinCode=" + APISAuthPinCode + "&APISAuthPassword=" + APISAuthPassword, myobject).Result;
                        var newobject = res.Content.ReadAsAsync<string>().Result;
                        var result2 = JsonConvert.DeserializeObject<CareerTBL>(newobject);

                        string emailto = "ayman.fathy.elbatata@gmail.com";
                        string msgsubject = "Technical Support Department, A New Message-Careers, (SalmaSoft W/D System)";
                        string msgcontent = "Hello " + "Ayman ELbatata" + ", " + " Due to your request, Someone Sent a new CV/Resume to our Website. \n \n His/Her Name is: " + myobject.CandidateName + "\n \n His/Her Email is: " + myobject.CandidateEmail + "\n \n His/Her Position: " + myobject.CandidatePosition + "\n \n Date of Making : " + myobject.DateOfMaking;
                        SendMessageToEmail.sendemailtosomeone(emailto, msgsubject, msgcontent,Image1);

                        return RedirectToAction("Careers", "Home", new { Area = "", recopass = 1 });
                    }

                ViewBag.message = "Error, Please try again!.";
                return View();
            }
            catch (Exception ex)
            {
                Session["myerrordesc"] = ex;
                return RedirectToAction("ErrorDesc");
            }
        }
        #endregion

        #region Contact US
        [HttpGet]
        public ActionResult ContactUS()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Session["myerrordesc"] = ex;
                return RedirectToAction("ErrorDesc");
            }
        }


        [HttpPost]
        public ActionResult ContactUS(ContactUS mymodel)
        {
            try
            {
                HttpClient h = new HttpClient();
                h.BaseAddress = new Uri(Link);
                string APISAuthPinCode = CurrentUserLoginData.getcurrentusrdataAuth(8);
                string APISAuthPassword = CurrentUserLoginData.getcurrentusrdataAuth(9);
                string result = null;

                string API = "ContactUS";
                ContactUSTBL model = new ContactUSTBL();
                model.ContactName = mymodel.Name;
                model.ContactEmail = mymodel.Email;
                model.ContactMessage = mymodel.Message;
                model.DateOfMaking = DateTime.Now;
                HttpRequestBase request1 = Request;
                model.ContactIPAddress = GetClientMacAndIP.GetClientIpAddress(request1);
                var res = h.PostAsJsonAsync(API + "?Addnew=1&APISAuthPinCode=" + APISAuthPinCode + "&APISAuthPassword=" + APISAuthPassword, model).Result;
                var newobject = res.Content.ReadAsAsync<string>().Result;
                var result2 = JsonConvert.DeserializeObject<ContactUSTBL>(newobject);

                string emailto = "ayman.fathy.elbatata@gmail.com";
                string msgsubject = "Technical Support Department, A New Message-ContactUS, (SalmaSoft W/D System)";
                string msgcontent = "Hello " + "Ayman ELbatata" + ", " + " Due to your request, Someone Sent a new message to our Website. \n \n His/Her Name is: " + model.ContactName +  "\n \n His/Her Email is: " + model.ContactEmail + "\n \n His/Her Message: " + model.ContactMessage + "\n \n Date of Making : " + model.DateOfMaking;
                SendMessageToEmail.sendemailtosomeone(emailto, msgsubject, msgcontent);

                result = "Your Message has been sent successfully.";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Session["myerrordesc"] = ex;
                return RedirectToAction("ErrorDesc");
            }
        }
        #endregion

        #region Reset Password & Recover Password

        // RESET PASSWORD BY EMAIL
        [HttpGet]
        [AllowAnonymous]
        [CheckSessionUsronline]
        public ActionResult Resetpassword()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [CheckSessionUsronline]
        [ValidateAntiForgeryToken]
        public ActionResult Resetpassword(ForLoginDataEmail myemail)
        {
            try
            {
                if (this.IsCaptchaValid("Validate your captcha"))
                {
                    if (ModelState.IsValid)
                    {
                        HttpClient h = new HttpClient();
                        h.BaseAddress = new Uri(Link);

                        string ReqAPI = "Employee";
                        var res = h.GetAsync(ReqAPI + "?RecoverEmployeeEmail=" + myemail.loginemail).Result;
                        var EMPDetail = res.Content.ReadAsAsync<EmployeeTBL>().Result;

                        if (EMPDetail == null)
                        {
                            ViewBag.message = "This Email address is not recored in our System, Please Try again!.";
                            return View(myemail);
                        }
                        else if (EMPDetail.IsDeleted == true)
                        {
                            ViewBag.message = "Oh Sorry, This Account has been deleted and we cannot Recover its Password!.";
                            return View(myemail);
                        }
                        else
                        {
                            string APISAuthPinCode = CurrentUserLoginData.getcurrentusrdataAuth(8);
                            string APISAuthPassword = CurrentUserLoginData.getcurrentusrdataAuth(9);

                            var newautocode = new RecoverPasswordTBL();
                            newautocode.RecoverPasswordAutoCode = MySPECIALGUID.GetUniqueKey(50);
                            newautocode.EmployeeIDD = EMPDetail.EmployeeID;
                            newautocode.DateOfMaking = DateTime.Now;

                            string API2 = "RecoverPassword";
                            var res2 = h.PostAsJsonAsync(API2 + "?Addnew=1&APISAuthPinCode=" + APISAuthPinCode + "&APISAuthPassword=" + APISAuthPassword, newautocode).Result;
                            var lst1 = res2.Content.ReadAsAsync<string>().Result;
                            var result = JsonConvert.DeserializeObject<RecoverPasswordTBL>(lst1);

                            string emailto = EMPDetail.EmployeeEmail;
                            string msgsubject = "Technical Support Department, Recovering Your Password, (SalmaSoft W/D System)";
                            string msgcontent = "Hello " + EMPDetail.EmployeeFullName + ", " + " Due to your request, You Asked to recover your password for your account which in our Website. \n \n Your Email is: " + EMPDetail.EmployeeEmail + "\n \n To Reset Your Password, Go to this Link: \n http://aymanelbatata.vr.lt/AYMWebDevelopment/Home/RecoverPassword?AutoCode=" + result.RecoverPasswordAutoCode + "\n \n This Link Is valid for next " + (24 - result.DateOfMaking.Value.Hour) + " hour only. \n \n Now you can come back to our website and Set a new Password easily.";
                            SendMessageToEmail.sendemailtosomeone(emailto, msgsubject, msgcontent);
                            return RedirectToAction("LoginPage", "Home", new { Area = "", recopass = 1 });
                        }
                    }
                    else
                    {
                        ViewBag.message = "Error, Please try again!.";
                        return View(myemail);
                    }
                }
                else
                {
                    ViewBag.message = "Please Retry captcha!.";
                    return View(myemail);
                }
            }
            catch (Exception ex)
            {
                Session["myerrordesc"] = ex;
                return RedirectToAction("ErrorDesc");
            }

        }
      

        // RECOVER PASSWORD
        [HttpGet]
        [AllowAnonymous]
        [CheckSessionUsronline]
        public ActionResult RecoverPassword(string AutoCode)
        {
            try
            {
                HttpClient h = new HttpClient();
                h.BaseAddress = new Uri(Link);
                var res1 = h.GetAsync("RecoverPassword" + "?EMPAutoCode=" + AutoCode).Result;
                var newobject1 = res1.Content.ReadAsAsync<RecoverPasswordTBL>().Result;
                if (newobject1 != null)
                {
                    if (DatesComparison.DateEqualsORLessThanDate2((DateTime)DateTime.Now, (DateTime)DateTime.Now, (DateTime)newobject1.DateOfMaking))
                    {
                        ViewBag.AutoCode = AutoCode;
                    }
                    else
                    {
                        ViewBag.expire = "This Session has been ended or was deleted, Please Reset your password again!.";
                    }
                    return View();
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                Session["myerrordesc"] = ex;
                return RedirectToAction("ErrorDesc");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [CheckSessionUsronline]
        [ValidateAntiForgeryToken]
        public ActionResult RecoverPassword(EMPChangePassword1 model, string AutoCode)
        {
            try
            {
                if (this.IsCaptchaValid("Validate your captcha"))
                {
                    if (ModelState.IsValid)
                    {
                        HttpClient h = new HttpClient();
                        h.BaseAddress = new Uri(Link);
                        var res1 = h.GetAsync("RecoverPassword" + "?EMPAutoCode=" + AutoCode).Result;
                        var newobject1 = res1.Content.ReadAsAsync<RecoverPasswordTBL>().Result;

                        var res = h.GetAsync("Employee" + "?Id=" + newobject1.EmployeeIDD).Result;
                        var newobject = res.Content.ReadAsAsync<EmployeeTBL>().Result;
                        newobject.EmployeePassword = HashMD5AYM.GenerateNewPasswordMD5(model.PasswordConfirm);
                        string APISAuthPinCode = CurrentUserLoginData.getcurrentusrdataAuth(8);
                        string APISAuthPassword = CurrentUserLoginData.getcurrentusrdataAuth(9);

                        var res2 = h.PostAsJsonAsync("Employee" + "?EditExists=1&APISAuthPinCode=" + APISAuthPinCode + "&APISAuthPassword=" + APISAuthPassword, newobject).Result;
                        var lst1 = res2.Content.ReadAsAsync<string>().Result;
                        var result = JsonConvert.DeserializeObject<EmployeeTBL>(lst1);

                        var res3 = h.PostAsJsonAsync("RecoverPassword" + "?Del=1&APISAuthPinCode=" + APISAuthPinCode + "&APISAuthPassword=" + APISAuthPassword, newobject1).Result;
                        var lst3 = res3.Content.ReadAsAsync<string>().Result;
                        var result3 = JsonConvert.DeserializeObject<RecoverPasswordTBL>(lst3);


                        return RedirectToAction("LoginPage", "Home", new { Area = "", recopass = 2 });
                    }
                    else
                    {
                        ViewBag.AutoCode = AutoCode;
                        ViewBag.message = "Error, Please Try again!";
                        return View(model);
                    }
                }
                else
                {
                    ViewBag.AutoCode = AutoCode;
                    ViewBag.message = "Please Retry captcha!.";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                Session["myerrordesc"] = ex;
                return RedirectToAction("ErrorDesc");
            }
        }

        #endregion


        // Error View
        // DB Connection
        [HttpGet]
        public ActionResult ErrorDesc()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Session["myerrordesc"] = ex;
                return RedirectToAction("ErrorDesc");
            }
        }

        // Error View
        [HttpGet]
        public ActionResult InvalidNotAllowed()
        {
            return PartialView("_ErrorNotAllowed");
        }

        //No View
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                mydb.Dispose();
            }
            base.Dispose(disposing);
        }
        //


        //
    }
}
