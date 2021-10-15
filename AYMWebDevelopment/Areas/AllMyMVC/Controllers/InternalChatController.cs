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
    [Authorize]
    public class InternalChatController : Base1234567Controller
    {
        // GET: AllMyMVC/InternalChat
        AYMCOMPDataContext mydb = new AYMCOMPDataContext();

        string Link = APILinks.GetAPILINK(1);
        string API = "InternalChat";
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
        public JsonResult listAll(string txtENBTNSearch = null, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                if (txtENBTNSearch != null && txtENBTNSearch != "" && !string.IsNullOrEmpty(txtENBTNSearch))
                {
                    h.BaseAddress = new Uri(Link);
                    var res = h.GetAsync(API + "?StartSearchWord=" + txtENBTNSearch.Trim()).Result;
                    var lst = res.Content.ReadAsAsync<List<InternalChatTBL>>().Result;
                    return Json(new { Result = "OK", Records = lst.Skip(jtStartIndex).Take(jtPageSize), TotalRecordCount = lst.Count() });
                }
                else
                {
                    h.BaseAddress = new Uri(Link);
                    var res = h.GetAsync(API).Result;
                    var lst = res.Content.ReadAsAsync<List<InternalChatTBL>>().Result;
                    return Json(new { Result = "OK", Records = lst.Skip(jtStartIndex).Take(jtPageSize), TotalRecordCount = lst.Count() });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        //[HttpPost]
        //public JsonResult CreateOne(InternalChatTBL model)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            //check if  exist
        //            h.BaseAddress = new Uri(Link);

        //            var res1 = h.PostAsJsonAsync(API + "?CreatePermission=1", model).Result;
        //            var reqexist = res1.Content.ReadAsAsync<int>().Result;
        //            if (reqexist == 0)
        //            {
        //                var res = h.PostAsJsonAsync(API + "?Addnew=1", model).Result;
        //                var newobject = res.Content.ReadAsAsync<InternalChatTBL>().Result;
        //                return Json(new { Result = "OK", Record = newobject });
        //            }
        //            else
        //            {
        //                return Json(new { Result = "ERROR", Message = "There is a record with the same name" });
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

        //[HttpPost]
        //public JsonResult UpdateOne(InternalChatTBL model)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            //check if  exist
        //            h.BaseAddress = new Uri(Link);
        //            var res1 = h.PostAsJsonAsync(API + "?UpdatePermission=1", model).Result;
        //            var reqexist = res1.Content.ReadAsAsync<int>().Result;
        //            if (reqexist == 0)
        //            {
        //                var res = h.PostAsJsonAsync(API + "?EditExists=1", model).Result;
        //                var newobject = res.Content.ReadAsAsync<InternalChatTBL>().Result;
        //                return Json(new { Result = "OK" });
        //            }
        //            else
        //            {
        //                return Json(new { Result = "ERROR", Message = "There is a record with the same name" });
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
        public JsonResult DeleteOne(int InternalChatID)
        {
            try
            {
                // find Required Object
                h.BaseAddress = new Uri(Link);
                var res1 = h.GetAsync(API + "?Id=" + InternalChatID).Result;
                var newobject = res1.Content.ReadAsAsync<InternalChatTBL>().Result;

                // Send row to be deleted
                if (newobject != null)
                {
                    string APISAuthPinCode = CurrentUserLoginData.getcurrentusrdataAuth(8);
                    string APISAuthPassword = CurrentUserLoginData.getcurrentusrdataAuth(9);

                    var res = h.PostAsJsonAsync(API + "?Del=1&APISAuthPinCode=" + APISAuthPinCode + "&APISAuthPassword=" + APISAuthPassword, newobject).Result;
                    var lst1 = res.Content.ReadAsAsync<string>().Result;
                    if (lst1 != "Canceled")
                    {
                        var result = JsonConvert.DeserializeObject<InternalChatTBL>(lst1);
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



        //
        [HttpPost]
        public JsonResult AddNewMessage(string EmployeeText = null)
        {
            try
            {
                string result = "";

                if (!string.IsNullOrEmpty(EmployeeText))
                {
                    h.BaseAddress = new Uri(Link);
                    int employee = Convert.ToInt32(CurrentUserLoginData.getcurrentusrdataAuth(1));
                    h.BaseAddress = new Uri(Link);
                    var newobject = new InternalChatTBL();
                    newobject.DateOfMaking = DateTime.Now;
                    newobject.EmployeeIDD = employee;
                    newobject.TextMessage = EmployeeText;
                    string APISAuthPinCode = CurrentUserLoginData.getcurrentusrdataAuth(8);
                    string APISAuthPassword = CurrentUserLoginData.getcurrentusrdataAuth(9);

                    var res = h.PostAsJsonAsync(API + "?Addnew=1&APISAuthPinCode=" + APISAuthPinCode + "&APISAuthPassword=" + APISAuthPassword, newobject).Result;
                    var newobject1 = res.Content.ReadAsAsync<string>().Result;
                    var myresult = JsonConvert.DeserializeObject<InternalChatTBL>(newobject1);

                    if (myresult.InternalChatID > 0)
                    {
                        result = "OK";
                    }
                    else
                    {
                        result = "NO";
                    }
                }
                else
                {
                    result = "NO";
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpGet]
        public JsonResult GetSenderAutoCode()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var model = new InternalChatModel1();
                model.EmployeeAutoCode = CurrentUserLoginData.getcurrentusrdataAuth(2);
                model.EmployeeTypeName = CurrentUserLoginData.getcurrentusrdataAuth(6);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult UpdateChatToday()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync(API + "?UpdateToday=1").Result;
                var lst = res.Content.ReadAsAsync<List<InternalChatModel2>>().Result;
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult UpdateChatByDates(DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync(API + "?UpdateChatByDates=1&DateFrom=" + DateFrom + "&DateTo=" + DateTo).Result;
                var lst = res.Content.ReadAsAsync<List<InternalChatModel2>>().Result;
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
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