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
    public class CareerController : Base123456Controller
    {
        // GET: AllMyMVC/Career
        AYMCOMPDataContext mydb = new AYMCOMPDataContext();

        string Link = APILinks.GetAPILINK(1);
        string API = "Career";
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
                    var lst = res.Content.ReadAsAsync<List<CareerTBL>>().Result.OrderByDescending(x => x.DateOfMaking);
                    return Json(new { Result = "OK", Records = lst.Skip(jtStartIndex).Take(jtPageSize), TotalRecordCount = lst.Count() });
                }
                else
                {
                    h.BaseAddress = new Uri(Link);
                    var res = h.GetAsync(API).Result;
                    var lst = res.Content.ReadAsAsync<List<CareerTBL>>().Result.OrderByDescending(x => x.DateOfMaking);
                    return Json(new { Result = "OK", Records = lst.Skip(jtStartIndex).Take(jtPageSize), TotalRecordCount = lst.Count() });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }


        //[HttpPost]
        //public JsonResult UpdateOne(CareerTBL model, HttpPostedFileBase CandidateFileURL = null)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            h.BaseAddress = new Uri(Link);

        //            var myres = h.GetAsync(API + "?Id=" + model.CandidateID).Result;
        //            var mynewobject = myres.Content.ReadAsAsync<CareerTBL>().Result;
        //            model.CandidateEmail = mynewobject.CandidateEmail;
        //            model.CandidateIPAddress = mynewobject.CandidateIPAddress;
        //            model.CandidateName = mynewobject.CandidateName;
        //            model.DateOfMaking = mynewobject.DateOfMaking;

        //            if (model.CandidateFileURL == null && mynewobject.CandidateFileURL != null)
        //            {
        //                model.CandidateFileURL = mynewobject.CandidateFileURL;
        //            }

        //            string APISAuthPinCode = CurrentUserLoginData.getcurrentusrdataAuth(8);
        //            string APISAuthPassword = CurrentUserLoginData.getcurrentusrdataAuth(9);

        //            var res = h.PostAsJsonAsync(API + "?EditExists=1&APISAuthPinCode=" + APISAuthPinCode + "&APISAuthPassword=" + APISAuthPassword, model).Result;
        //            var lst1 = res.Content.ReadAsAsync<string>().Result;
        //            if (lst1 != "Canceled")
        //            {
        //                var result = JsonConvert.DeserializeObject<CareerTBL>(lst1);
        //                return Json(new { Result = "OK" });
        //            }
        //            else
        //            {
        //                return Json(new { Result = "ERROR", Message = "Canceled, The Operation was refused for authintication!." });
        //            }
        //        }
        //        else
        //        {
        //            return Json(new { Result = "ERROR", Message = "Sorry, there is a missing in the DB" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex });
        //    }
        //}


        [HttpPost]
        public JsonResult DeleteOne(int CandidateID)
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res1 = h.GetAsync(API + "?Id=" + CandidateID).Result;
                var model = res1.Content.ReadAsAsync<CareerTBL>().Result;

                // Send row to be deleted
                if (model != null)
                {
                    string APISAuthPinCode = CurrentUserLoginData.getcurrentusrdataAuth(8);
                    string APISAuthPassword = CurrentUserLoginData.getcurrentusrdataAuth(9);

                    var res = h.PostAsJsonAsync(API + "?Del=1&APISAuthPinCode=" + APISAuthPinCode + "&APISAuthPassword=" + APISAuthPassword, model).Result;
                    var lst1 = res.Content.ReadAsAsync<string>().Result;
                    if (lst1 != "Canceled")
                    {
                        var result = JsonConvert.DeserializeObject<CareerTBL>(lst1);
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