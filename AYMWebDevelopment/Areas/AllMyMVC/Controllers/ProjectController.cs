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
    public class ProjectController : Base123456Controller
    {
        // GET: AllMyMVC/Project

        AYMCOMPDataContext mydb = new AYMCOMPDataContext();

        string Link = APILinks.GetAPILINK(1);
        string API = "Project";
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
                    var lst = res.Content.ReadAsAsync<List<ProjectTBL>>().Result.OrderByDescending(x => x.DateOfMaking);
                    return Json(new { Result = "OK", Records = lst.Skip(jtStartIndex).Take(jtPageSize), TotalRecordCount = lst.Count() });
                }
                else
                {
                    h.BaseAddress = new Uri(Link);
                    var res = h.GetAsync(API).Result;
                    var lst = res.Content.ReadAsAsync<List<ProjectTBL>>().Result.OrderByDescending(x => x.DateOfMaking);
                    return Json(new { Result = "OK", Records = lst.Skip(jtStartIndex).Take(jtPageSize), TotalRecordCount = lst.Count() });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult CreateOne(ProjectTBL model, HttpPostedFileBase ProjectDetailsFileURL = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.ProjectTypeIDD == null || model.ProjectPriceIDD == null || model.ProjectStatusIDD == null || model.EmployeeIDDManager == null)
                    {
                        return Json(new { Result = "ERROR", Message = "Please Select All lists." });
                    }
                    //check if  exist
                    h.BaseAddress = new Uri(Link);

                    //
                    if (ProjectDetailsFileURL != null && ProjectDetailsFileURL.ContentLength > 0)
                    {
                        if (Path.GetExtension(ProjectDetailsFileURL.FileName) == ".pdf" || Path.GetExtension(ProjectDetailsFileURL.FileName) == ".PDF")
                        {
                            string fullPath = Request.MapPath("~/Images/Project/" + model.ProjectDetailsFileURL);
                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
                            if (!Directory.Exists("~/Images/Project"))
                            {
                                Directory.CreateDirectory(Server.MapPath("~/Images/Project"));
                            }
                            string _FileName = "Project" + Guid.NewGuid() + Path.GetExtension(ProjectDetailsFileURL.FileName);
                            string _path = Path.Combine(Server.MapPath("~/Images/Project/"), _FileName);
                            ProjectDetailsFileURL.SaveAs(_path);
                            model.ProjectDetailsFileURL = _FileName;
                        }
                        else
                        {
                            return Json(new { Result = "ERROR", Message = "The Resume must be a PDF File." });
                        }
                    }
                    //
                    model.EmployeeIDDLastUpdate = Convert.ToInt32(CurrentUserLoginData.getcurrentusrdataAuth(1));
                    model.AutoCode = "PRJ" + MySPECIALGUID.GetUniqueKey(9);
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
                            var result = JsonConvert.DeserializeObject<ProjectTBL>(lst1);
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
        public JsonResult UpdateOne(ProjectTBL model, HttpPostedFileBase ProjectDetailsFileURL = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.ProjectTypeIDD == null || model.ProjectPriceIDD == null || model.ProjectStatusIDD == null || model.EmployeeIDDManager == null)
                    {
                        return Json(new { Result = "ERROR", Message = "Please Select All lists." });
                    }

                    h.BaseAddress = new Uri(Link);

                    // For Image and file
                    var myres = h.GetAsync(API + "?Id=" + model.ProjectID).Result;
                    var mynewobject = myres.Content.ReadAsAsync<ProjectTBL>().Result;

                    if (model.ProjectDetailsFileURL == null && mynewobject.ProjectDetailsFileURL != null)
                    {
                        model.ProjectDetailsFileURL = mynewobject.ProjectDetailsFileURL;
                    }
                    model.AutoCode = mynewobject.AutoCode;
                    model.DateOfMaking = mynewobject.DateOfMaking;
                    model.EmployeeIDDLastUpdate = Convert.ToInt32(CurrentUserLoginData.getcurrentusrdataAuth(1));
                    model.DateOfLastUpdate = DateTime.Now;
                    //check if  exist
                    if (ProjectDetailsFileURL != null && ProjectDetailsFileURL.ContentLength > 0)
                    {
                        if (Path.GetExtension(ProjectDetailsFileURL.FileName) == ".pdf" || Path.GetExtension(ProjectDetailsFileURL.FileName) == ".PDF")
                        {
                            string fullPath = Request.MapPath("~/Images/Project/" + model.ProjectDetailsFileURL);
                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
                            if (!Directory.Exists("~/Images/Project"))
                            {
                                Directory.CreateDirectory(Server.MapPath("~/Images/Project"));
                            }
                            string _FileName = "Project" + Guid.NewGuid() + Path.GetExtension(ProjectDetailsFileURL.FileName);
                            string _path = Path.Combine(Server.MapPath("~/Images/Project/"), _FileName);
                            ProjectDetailsFileURL.SaveAs(_path);
                            model.ProjectDetailsFileURL = _FileName;
                        }
                        else
                        {
                            return Json(new { Result = "ERROR", Message = "The Resume must be a PDF File." });
                        }
                    }

                    //
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
                            var result = JsonConvert.DeserializeObject<ProjectTBL>(lst1);
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
        public JsonResult DeleteOne(int ProjectID)
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res1 = h.GetAsync(API + "?Id=" + ProjectID).Result;
                var newobject = res1.Content.ReadAsAsync<ProjectTBL>().Result;
                newobject.EmployeeIDDLastUpdate = Convert.ToInt32(CurrentUserLoginData.getcurrentusrdataAuth(1));
                newobject.DateOfLastUpdate = DateTime.Now;

                // Send row to be deleted
                if (newobject != null)
                {
                    string APISAuthPinCode = CurrentUserLoginData.getcurrentusrdataAuth(8);
                    string APISAuthPassword = CurrentUserLoginData.getcurrentusrdataAuth(9);

                    var res = h.PostAsJsonAsync(API + "?Del=1&APISAuthPinCode=" + APISAuthPinCode + "&APISAuthPassword=" + APISAuthPassword, newobject).Result;
                    var lst1 = res.Content.ReadAsAsync<string>().Result;
                    if (lst1 != "Canceled")
                    {
                        var result = JsonConvert.DeserializeObject<ExpenseRevenueTBL>(lst1);
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