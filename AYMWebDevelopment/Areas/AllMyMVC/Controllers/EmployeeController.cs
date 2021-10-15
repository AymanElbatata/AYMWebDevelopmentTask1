using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AYMWebDevelopment.Areas.AllMyAPI.Data;
using AYMWebDevelopment.Controllers;
using AYMWebDevelopment.Models;
using Newtonsoft.Json;

namespace AYMWebDevelopment.Areas.AllMyMVC.Controllers
{
    public class EmployeeController : Base123Controller
    {
        // GET: AllMyMVC/Employee
        AYMCOMPDataContext mydb = new AYMCOMPDataContext();

        string Link = APILinks.GetAPILINK(1);
        string API = "Employee";
        HttpClient h = new HttpClient();

        [HttpGet]
        public ActionResult Index()
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
        public JsonResult listAll(string txtENBTNSearch = null, int Searchtype = 0, DateTime? StartingDate = null, DateTime? EndingDate = null, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                if ((txtENBTNSearch != null && txtENBTNSearch != "" && !string.IsNullOrEmpty(txtENBTNSearch)) || (Searchtype > 0 && StartingDate != null && EndingDate != null))
                {
                    h.BaseAddress = new Uri(Link);
                    var res = h.GetAsync(API + "?StartSearchWord=" + txtENBTNSearch.Trim() + "&type=" + Searchtype + "&StartingDate=" + StartingDate + "&EndingDate=" + EndingDate).Result;
                    var lst = res.Content.ReadAsAsync<List<EmployeeTBL>>().Result.OrderByDescending(x => x.DateOfMaking);
                    return Json(new { Result = "OK", Records = lst.Skip(jtStartIndex).Take(jtPageSize), TotalRecordCount = lst.Count() });
                }
                else
                {
                    h.BaseAddress = new Uri(Link);
                    var res = h.GetAsync(API).Result;
                    var lst = res.Content.ReadAsAsync<List<EmployeeTBL>>().Result.OrderByDescending(x => x.DateOfMaking);
                    return Json(new { Result = "OK", Records = lst.Skip(jtStartIndex).Take(jtPageSize), TotalRecordCount = lst.Count() });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult CreateOne(EmployeeTBL model, HttpPostedFileBase PassportORIDIMGURL = null, HttpPostedFileBase ResumeFIleURL = null , HttpPostedFileBase DocumentFileURL = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.EmployeeTypeIDD == null || model.MaritalStatusIDD == null || model.GenderIDD == null ||
                        model.EducationIDD == null || model.ProfessionIDD == null || model.SalaryIDD == null ||
                        model.NationalityIDD == null || model.CountryIDD == null || model.GovernorateIDD == null ||
                        model.CityIDD == null || model.BranchIDD == null)
                    {
                        return Json(new { Result = "ERROR", Message = "Please Select All lists." });
                    }
                    //check if  exist
                    h.BaseAddress = new Uri(Link);

                    if (PassportORIDIMGURL != null && PassportORIDIMGURL.ContentLength > 0)
                    {
                        if (Path.GetExtension(PassportORIDIMGURL.FileName) == ".jpg" || Path.GetExtension(PassportORIDIMGURL.FileName) == ".jpeg"
                         || Path.GetExtension(PassportORIDIMGURL.FileName) == ".JPG" || Path.GetExtension(PassportORIDIMGURL.FileName) == ".JPEG"
                         || Path.GetExtension(PassportORIDIMGURL.FileName) == ".png" || Path.GetExtension(PassportORIDIMGURL.FileName) == ".bmp"
                         || Path.GetExtension(PassportORIDIMGURL.FileName) == ".PNG" || Path.GetExtension(PassportORIDIMGURL.FileName) == ".BMP")
                        {
                            string fullPath = Request.MapPath("~/Images/Employee/" + model.PassportORIDIMGURL);
                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
                            if (!Directory.Exists("~/Images/Employee"))
                            {
                                Directory.CreateDirectory(Server.MapPath("~/Images/Employee"));
                            }
                            string _FileName = "EMPIMG" + Guid.NewGuid() + Path.GetExtension(PassportORIDIMGURL.FileName);
                            string _path = Path.Combine(Server.MapPath("~/Images/Employee/"), _FileName);
                            PassportORIDIMGURL.SaveAs(_path);
                            model.PassportORIDIMGURL = _FileName;
                        }
                        else
                        {
                            return Json(new { Result = "ERROR", Message = "The file must be an image." });
                        }
                    }

                    //
                    if (ResumeFIleURL != null && ResumeFIleURL.ContentLength > 0)
                    {
                        if (Path.GetExtension(ResumeFIleURL.FileName) == ".pdf" || Path.GetExtension(ResumeFIleURL.FileName) == ".PDF")
                        {
                            string fullPath = Request.MapPath("~/Images/Employee/" + model.ResumeFIleURL);
                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
                            if (!Directory.Exists("~/Images/Employee"))
                            {
                                Directory.CreateDirectory(Server.MapPath("~/Images/Employee"));
                            }
                            string _FileName = "EMPCV" + Guid.NewGuid() + Path.GetExtension(ResumeFIleURL.FileName);
                            string _path = Path.Combine(Server.MapPath("~/Images/Employee/"), _FileName);
                            ResumeFIleURL.SaveAs(_path);
                            model.ResumeFIleURL = _FileName;
                        }
                        else
                        {
                            return Json(new { Result = "ERROR", Message = "The Resume must be a PDF File." });
                        }
                    }
                    //
                    //
                    if (DocumentFileURL != null && DocumentFileURL.ContentLength > 0)
                    {
                        if (Path.GetExtension(DocumentFileURL.FileName) == ".pdf" || Path.GetExtension(DocumentFileURL.FileName) == ".PDF")
                        {
                            string fullPath = Request.MapPath("~/Images/Employee/" + model.DocumentFileURL);
                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
                            if (!Directory.Exists("~/Images/Employee"))
                            {
                                Directory.CreateDirectory(Server.MapPath("~/Images/Employee"));
                            }
                            string _FileName = "EMPDocument" + Guid.NewGuid() + Path.GetExtension(DocumentFileURL.FileName);
                            string _path = Path.Combine(Server.MapPath("~/Images/Employee/"), _FileName);
                            DocumentFileURL.SaveAs(_path);
                            model.DocumentFileURL = _FileName;
                        }
                        else
                        {
                            return Json(new { Result = "ERROR", Message = "The Document must be a PDF File." });
                        }
                    }
                    //
                    model.AutoCode = "EMP" + MySPECIALGUID.GetUniqueKey(9);
                    model.EmployeePassword = HashMD5AYM.GenerateNewPasswordMD5(model.EmployeePassword);
                    //model.EmployeeIDDLastUpdate = Convert.ToInt32(CurrentUserLoginData.getcurrentusrdataAuth(1));
                    model.DateOfMaking = DateTime.Now;
                    var res1 = h.PostAsJsonAsync(API + "?CreatePermission=1", model).Result;
                    var reqexist = res1.Content.ReadAsAsync<int>().Result;
                    if (reqexist == 0)
                    {
                        string APISAuthPinCode = CurrentUserLoginData.getcurrentusrdataAuth(8);
                        string APISAuthPassword = CurrentUserLoginData.getcurrentusrdataAuth(9);

                        var res = h.PostAsJsonAsync(API + "?Addnew=1&APISAuthPinCode=" + APISAuthPinCode + "&APISAuthPassword=" + APISAuthPassword, model).Result;
                        var lst1 = res.Content.ReadAsAsync<string>().Result;
                        if (lst1 != "Canceled")
                        {
                            var result = JsonConvert.DeserializeObject<EmployeeTBL>(lst1);
                            return Json(new { Result = "OK", Record = result });
                        }
                        else
                        {
                            return Json(new { Result = "ERROR", Message = "Canceled, The Operation was refused for authintication!." });
                        }
                    }
                    else
                    {
                        return Json(new { Result = "ERROR", Message = "There is a record with the same name" });
                    }
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = "Sorry, there is a missing in the DB" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });

            }
        }

        [HttpPost]
        public JsonResult UpdateOne(EmployeeTBL model, HttpPostedFileBase PassportORIDIMGURL = null, HttpPostedFileBase ResumeFIleURL = null, HttpPostedFileBase DocumentFileURL = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.EmployeeTypeIDD == null || model.MaritalStatusIDD == null || model.GenderIDD == null ||
                        model.EducationIDD == null || model.ProfessionIDD == null || model.SalaryIDD == null ||
                        model.NationalityIDD == null || model.CountryIDD == null || model.GovernorateIDD == null ||
                        model.CityIDD == null || model.BranchIDD == null)
                    {
                        return Json(new { Result = "ERROR", Message = "Please Select All lists." });
                    }

                    h.BaseAddress = new Uri(Link);

                    // For Image and file
                    var myres = h.GetAsync(API + "?Id=" + model.EmployeeID).Result;
                    var mynewobject = myres.Content.ReadAsAsync<EmployeeTBL>().Result;
                    if (model.PassportORIDIMGURL == null && mynewobject.PassportORIDIMGURL != null)
                    {
                        model.PassportORIDIMGURL = mynewobject.PassportORIDIMGURL;
                    }
                    if (model.ResumeFIleURL == null && mynewobject.ResumeFIleURL != null)
                    {
                        model.ResumeFIleURL = mynewobject.ResumeFIleURL;
                    }
                    if (model.DocumentFileURL == null && mynewobject.DocumentFileURL != null)
                    {
                        model.DocumentFileURL = mynewobject.DocumentFileURL;
                    }
                    if (model.EmployeePassword != mynewobject.EmployeePassword)
                    {
                        model.EmployeePassword = HashMD5AYM.GenerateNewPasswordMD5(model.EmployeePassword);
                    }

                    model.AutoCode = mynewobject.AutoCode;
                    model.DateOfMaking = mynewobject.DateOfMaking;
                    //model.EmployeeIDDLastUpdate = Convert.ToInt32(CurrentUserLoginData.getcurrentusrdataAuth(1));
                    model.DateOfLastUpdate = DateTime.Now;

                    //check if  exist
                    if (PassportORIDIMGURL != null && PassportORIDIMGURL.ContentLength > 0)
                    {
                        if (Path.GetExtension(PassportORIDIMGURL.FileName) == ".jpg" || Path.GetExtension(PassportORIDIMGURL.FileName) == ".jpeg"
                         || Path.GetExtension(PassportORIDIMGURL.FileName) == ".JPG" || Path.GetExtension(PassportORIDIMGURL.FileName) == ".JPEG"
                         || Path.GetExtension(PassportORIDIMGURL.FileName) == ".png" || Path.GetExtension(PassportORIDIMGURL.FileName) == ".bmp"
                         || Path.GetExtension(PassportORIDIMGURL.FileName) == ".PNG" || Path.GetExtension(PassportORIDIMGURL.FileName) == ".BMP")
                        {
                            string fullPath = Request.MapPath("~/Images/Employee/" + model.PassportORIDIMGURL);
                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
                            if (!Directory.Exists("~/Images/Employee"))
                            {
                                Directory.CreateDirectory(Server.MapPath("~/Images/Employee"));
                            }
                            string _FileName = "EMPIMG" + Guid.NewGuid() + Path.GetExtension(PassportORIDIMGURL.FileName);
                            string _path = Path.Combine(Server.MapPath("~/Images/Employee/"), _FileName);
                            PassportORIDIMGURL.SaveAs(_path);
                            model.PassportORIDIMGURL = _FileName;
                        }
                        else
                        {
                            return Json(new { Result = "ERROR", Message = "The file must be an image." });
                        }
                    }

                    //
                    if (ResumeFIleURL != null && ResumeFIleURL.ContentLength > 0)
                    {
                        if (Path.GetExtension(ResumeFIleURL.FileName) == ".pdf" || Path.GetExtension(ResumeFIleURL.FileName) == ".PDF")
                        {
                            string fullPath = Request.MapPath("~/Images/Employee/" + model.ResumeFIleURL);
                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
                            if (!Directory.Exists("~/Images/Employee"))
                            {
                                Directory.CreateDirectory(Server.MapPath("~/Images/Employee"));
                            }
                            string _FileName = "EMPCV" + Guid.NewGuid() + Path.GetExtension(ResumeFIleURL.FileName);
                            string _path = Path.Combine(Server.MapPath("~/Images/Employee/"), _FileName);
                            ResumeFIleURL.SaveAs(_path);
                            model.ResumeFIleURL = _FileName;
                        }
                        else
                        {
                            return Json(new { Result = "ERROR", Message = "The Resume must be a PDF File." });
                        }
                    }
                    //
                    //
                    if (DocumentFileURL != null && DocumentFileURL.ContentLength > 0)
                    {
                        if (Path.GetExtension(DocumentFileURL.FileName) == ".pdf" || Path.GetExtension(DocumentFileURL.FileName) == ".PDF")
                        {
                            string fullPath = Request.MapPath("~/Images/Employee/" + model.DocumentFileURL);
                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
                            if (!Directory.Exists("~/Images/Employee"))
                            {
                                Directory.CreateDirectory(Server.MapPath("~/Images/Employee"));
                            }
                            string _FileName = "EMPDocument" + Guid.NewGuid() + Path.GetExtension(DocumentFileURL.FileName);
                            string _path = Path.Combine(Server.MapPath("~/Images/Employee/"), _FileName);
                            DocumentFileURL.SaveAs(_path);
                            model.DocumentFileURL = _FileName;
                        }
                        else
                        {
                            return Json(new { Result = "ERROR", Message = "The Document must be a PDF File." });
                        }
                    }

                    //
                    var res1 = h.PostAsJsonAsync(API + "?UpdatePermission=1", model).Result;
                    var reqexist = res1.Content.ReadAsAsync<int>().Result;
                    if (reqexist == 0)
                    {
                        string APISAuthPinCode = CurrentUserLoginData.getcurrentusrdataAuth(8);
                            string APISAuthPassword = CurrentUserLoginData.getcurrentusrdataAuth(9);

                            var res = h.PostAsJsonAsync(API + "?EditExists=1&APISAuthPinCode=" + APISAuthPinCode + "&APISAuthPassword=" + APISAuthPassword, model).Result;
                            var lst1 = res.Content.ReadAsAsync<string>().Result;
                            if (lst1 != "Canceled")
                            {
                                var result = JsonConvert.DeserializeObject<EmployeeTBL>(lst1);
                                return Json(new { Result = "OK" });
                            }
                            else
                            {
                                return Json(new { Result = "ERROR", Message = "Canceled, The Operation was refused for authintication!." });
                            }
                    }
                    else
                    {
                        return Json(new { Result = "ERROR", Message = "There is a record with the same name" });
                    }
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = "Sorry, there is a missing in the DB" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult DeleteOne(int EmployeeID)
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res1 = h.GetAsync(API + "?Id=" + EmployeeID).Result;
                var model = res1.Content.ReadAsAsync<EmployeeTBL>().Result;
                //model.EmployeeIDDLastUpdate = Convert.ToInt32(CurrentUserLoginData.getcurrentusrdataAuth(1));
                model.DateOfLastUpdate = DateTime.Now;

                // Send row to be deleted
                if (model != null)
                {
                    string APISAuthPinCode = CurrentUserLoginData.getcurrentusrdataAuth(8);
                    string APISAuthPassword = CurrentUserLoginData.getcurrentusrdataAuth(9);

                    var res = h.PostAsJsonAsync(API + "?Del=1&APISAuthPinCode=" + APISAuthPinCode + "&APISAuthPassword=" + APISAuthPassword, model).Result;
                    var lst1 = res.Content.ReadAsAsync<string>().Result;
                    if (lst1 != "Canceled")
                    {
                        var result = JsonConvert.DeserializeObject<EmployeeTBL>(lst1);
                        return Json(new { Result = "OK" });
                    }
                    else
                    {
                        return Json(new { Result = "ERROR", Message = "Canceled, The Operation was refused for authintication!." });
                    }
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = "Sorry, there is a missing in the DB" });

                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                mydb.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}