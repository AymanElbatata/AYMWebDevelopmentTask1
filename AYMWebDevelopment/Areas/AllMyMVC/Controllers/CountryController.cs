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
    public class CountryController : Base123456Controller
    {
        // GET: AllMyMVC/Country
        AYMCOMPDataContext mydb = new AYMCOMPDataContext();

        string Link = APILinks.GetAPILINK(1);
        HttpClient h = new HttpClient();

        string API = "Country";
        private readonly IRepository<CountryTBL> _Repository;

        public CountryController(IRepository<CountryTBL> _Repository)
        {
            this._Repository = _Repository;
        }

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
                    var lst = _Repository.GetAllByNames("Country", txtENBTNSearch);

                    return Json(new { Result = "OK", Records = lst.Skip(jtStartIndex).Take(jtPageSize), TotalRecordCount = lst.Count() });
                }
                else
                {
                    var lst = _Repository.GetAll("Country");
                    return Json(new { Result = "OK", Records = lst.Skip(jtStartIndex).Take(jtPageSize), TotalRecordCount = lst.Count() });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }


        [HttpPost]
        public JsonResult CreateOne(CountryTBL model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    //check if  exist
                    h.BaseAddress = new Uri(Link);

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
                            var result = JsonConvert.DeserializeObject<CountryTBL>(lst1);
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
        public JsonResult UpdateOne(CountryTBL model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //check if  exist
                    h.BaseAddress = new Uri(Link);
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
                            var result = JsonConvert.DeserializeObject<CountryTBL>(lst1);
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
        public JsonResult DeleteOne(int CountryID)
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res1 = h.GetAsync(API + "?Id=" + CountryID).Result;
                var newobject = res1.Content.ReadAsAsync<CountryTBL>().Result;

                // Send row to be deleted
                if (newobject != null)
                {
                    string APISAuthPinCode = CurrentUserLoginData.getcurrentusrdataAuth(8);
                    string APISAuthPassword = CurrentUserLoginData.getcurrentusrdataAuth(9);

                    var res = h.PostAsJsonAsync(API + "?Del=1&APISAuthPinCode=" + APISAuthPinCode + "&APISAuthPassword=" + APISAuthPassword, newobject).Result;
                    var lst1 = res.Content.ReadAsAsync<string>().Result;
                    if (lst1 != "Canceled")
                    {
                        var result = JsonConvert.DeserializeObject<CountryTBL>(lst1);
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