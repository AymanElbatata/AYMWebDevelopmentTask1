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
    public class ContactUSController : Base123456Controller
    {
        // GET: ContactUS
        AYMCOMPDataContext mydb = new AYMCOMPDataContext();

        string Link = APILinks.GetAPILINK(1);
        string API = "ContactUS";
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
                    var lst = res.Content.ReadAsAsync<List<ContactUSTBL>>().Result.OrderByDescending(x => x.DateOfMaking);
                    return Json(new { Result = "OK", Records = lst.Skip(jtStartIndex).Take(jtPageSize), TotalRecordCount = lst.Count() });
                }
                else
                {
                    h.BaseAddress = new Uri(Link);
                    var res = h.GetAsync(API).Result;
                    var lst = res.Content.ReadAsAsync<List<ContactUSTBL>>().Result.OrderByDescending(x => x.DateOfMaking);
                    return Json(new { Result = "OK", Records = lst.Skip(jtStartIndex).Take(jtPageSize), TotalRecordCount = lst.Count() });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }


        [HttpPost]
        public JsonResult UpdateOne(ContactUSTBL model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    h.BaseAddress = new Uri(Link);

                    var myres = h.GetAsync(API + "?Id=" + model.ContactID).Result;
                    var mynewobject = myres.Content.ReadAsAsync<ContactUSTBL>().Result;
                    model.ContactEmail = mynewobject.ContactEmail;
                    model.ContactIPAddress = mynewobject.ContactIPAddress;
                    model.ContactName = mynewobject.ContactName;
                    model.DateOfMaking = mynewobject.DateOfMaking;
                    model.ContactMessage = mynewobject.ContactMessage;
                    model.EmployeeIDDLastUpdate = Convert.ToInt32(CurrentUserLoginData.getcurrentusrdataAuth(1));
                    model.DateOfLastUpdate = DateTime.Now;

                    string APISAuthPinCode = CurrentUserLoginData.getcurrentusrdataAuth(8);
                        string APISAuthPassword = CurrentUserLoginData.getcurrentusrdataAuth(9);

                        var res = h.PostAsJsonAsync(API + "?EditExists=1&APISAuthPinCode=" + APISAuthPinCode + "&APISAuthPassword=" + APISAuthPassword, model).Result;
                        var lst1 = res.Content.ReadAsAsync<string>().Result;
                        if (lst1 != "Canceled")
                        {
                            var result = JsonConvert.DeserializeObject<ContactUSTBL>(lst1);
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

        [HttpPost]
        public JsonResult DeleteOne(int ContactID)
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res1 = h.GetAsync(API + "?Id=" + ContactID).Result;
                var model = res1.Content.ReadAsAsync<ContactUSTBL>().Result;
                model.EmployeeIDDLastUpdate = Convert.ToInt32(CurrentUserLoginData.getcurrentusrdataAuth(1));
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
                        var result = JsonConvert.DeserializeObject<ContactUSTBL>(lst1);
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