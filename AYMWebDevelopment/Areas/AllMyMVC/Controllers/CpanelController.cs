using AYMWebDevelopment.Controllers;
using AYMWebDevelopment.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using AYMWebDevelopment.Areas.AllMyAPI.Data;

namespace AYMWebDevelopment.Areas.AllMyMVC.Controllers
{
    [Authorize]
    public class CpanelController : Base1234567Controller
    {
        string Link = APILinks.GetAPILINK(1);
        HttpClient h = new HttpClient();

        // GET: AllMyMVC/Cpanel
        [HttpGet]
        //[CheckSession2AddEditDelete]
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

        [HttpGet]
        public ActionResult MyProfile(int Updates = 0)
        {
            try
            {
                if (Updates > 0)
                {
                    ViewBag.Successful = "Well, Your profile has been updated successfully.";
                }
                return View();
            }
            catch (Exception ex)
            {
                Session["myerrordesc"] = ex;
                return RedirectToAction("ErrorDesc");
            }

        }


        // Edit Profile
        [HttpGet]
        public ActionResult EditPassword()
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
        [ValidateAntiForgeryToken]
        public ActionResult EditPassword(EMPChangePassword1 model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    h.BaseAddress = new Uri(Link);
                    int EMPID = Convert.ToInt32(CurrentUserLoginData.getcurrentusrdataAuth(1));
                    var res = h.GetAsync("Employee" + "?Id=" + EMPID).Result;
                    var newobject = res.Content.ReadAsAsync<EmployeeTBL>().Result;
                    newobject.EmployeePassword = HashMD5AYM.GenerateNewPasswordMD5(model.PasswordConfirm);
                    string APISAuthPinCode = CurrentUserLoginData.getcurrentusrdataAuth(8);
                    string APISAuthPassword = CurrentUserLoginData.getcurrentusrdataAuth(9);

                    var res2 = h.PostAsJsonAsync("Employee" + "?EditExists=1&APISAuthPinCode=" + APISAuthPinCode + "&APISAuthPassword=" + APISAuthPassword, newobject).Result;
                    var lst1 = res2.Content.ReadAsAsync<string>().Result;
                    var result = JsonConvert.DeserializeObject<EmployeeTBL>(lst1);
                        return RedirectToAction("MyProfile", "Cpanel", new { Area = "AllMyMVC", Updates = 1 });
                }
                else
                {
                    ViewBag.message = "Error, Please Try again!";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                Session["myerrordesc"] = ex;
                return RedirectToAction("ErrorDesc");
            }
        }
        //


        [HttpPost]
        public JsonResult Get_Project_ADDEDIT()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("Project" + "?ADDEDIT=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult Get_Project_List()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("Project" + "?Listonly=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }


        [HttpPost]
        public JsonResult Get_ProjectStatus_ADDEDIT()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("ProjectStatus" + "?ADDEDIT=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult Get_ProjectStatus_List()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("ProjectStatus" + "?Listonly=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }


        [HttpPost]
        public JsonResult Get_ProjectPrice_ADDEDIT()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("ProjectPrice" + "?ADDEDIT=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult Get_ProjectPrice_List()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("ProjectPrice" + "?Listonly=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }



        [HttpPost]
        public JsonResult Get_Salary_ADDEDIT()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("Salary" + "?ADDEDIT=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult Get_Salary_List()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("Salary" + "?Listonly=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult Get_Branch_ADDEDIT()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("Branch" + "?ADDEDIT=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult Get_Branch_List()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("Branch" + "?Listonly=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult Get_Resource_ADDEDIT()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("Resource" + "?ADDEDIT=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult Get_Resource_List()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("Resource" + "?Listonly=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult Get_Client_ADDEDIT()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("Client" + "?ADDEDIT=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult Get_Client_List()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("Client" + "?Listonly=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult Get_ProjectType_ADDEDIT()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("ProjectType" + "?ADDEDIT=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult Get_ProjectType_List()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("ProjectType" + "?Listonly=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult Get_ExpenseRevenueType_ADDEDIT()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("ExpenseRevenueType" + "?ADDEDIT=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult Get_ExpenseRevenueType_List()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("ExpenseRevenueType" + "?Listonly=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        //[HttpPost]
        //public JsonResult Get_City_ADDEDIT()
        //{
        //    try
        //    {
        //        h.BaseAddress = new Uri(Link);
        //        var res = h.GetAsync("City" + "?ADDEDIT=1").Result;
        //        var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
        //        return Json(new { Result = "OK", Options = lst });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex });
        //    }
        //}

        [HttpPost]
        public JsonResult Get_City_List()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("City" + "?Listonly=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }


        //[HttpPost]
        //public JsonResult Get_Governorate_ADDEDIT()
        //{
        //    try
        //    {
        //        h.BaseAddress = new Uri(Link);
        //        var res = h.GetAsync("Governorate" + "?ADDEDIT=1").Result;
        //        var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
        //        return Json(new { Result = "OK", Options = lst });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex });
        //    }
        //}

        [HttpPost]
        public JsonResult Get_Governorate_List()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("Governorate" + "?Listonly=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }


        [HttpPost]
        public JsonResult Get_Country_ADDEDIT()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("Country" + "?ADDEDIT=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult Get_Country_List()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("Country" + "?Listonly=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }


        [HttpPost]
        public JsonResult Get_Education_ADDEDIT()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("Education" + "?ADDEDIT=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult Get_Education_List()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("Education" + "?Listonly=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }


        [HttpPost]
        public JsonResult Get_Gender_ADDEDIT()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("Gender" + "?ADDEDIT=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult Get_Gender_List()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("Gender" + "?Listonly=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }


        [HttpPost]
        public JsonResult Get_MaritalStatus_ADDEDIT()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("MaritalStatus" + "?ADDEDIT=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult Get_MaritalStatus_List()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("MaritalStatus" + "?Listonly=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }


        [HttpPost]
        public JsonResult Get_Nationality_ADDEDIT()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("Nationality" + "?ADDEDIT=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult Get_Nationality_List()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("Nationality" + "?Listonly=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult Get_Profession_ADDEDIT()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("Profession" + "?ADDEDIT=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult Get_Profession_List()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("Profession" + "?Listonly=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }


        [HttpPost]
        public JsonResult Get_EmployeeType_ADDEDIT()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("EmployeeType" + "?ADDEDIT=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult Get_EmployeeType_List()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("EmployeeType" + "?Listonly=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult Get_Employee_ADDEDIT()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("Employee" + "?ADDEDIT=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }

        [HttpPost]
        public JsonResult Get_Employee_List()
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                var res = h.GetAsync("Employee" + "?Listonly=1").Result;
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex });
            }
        }


        [HttpPost]
        public JsonResult Get_Governorate_AddEditBYID(int CountryIDS = 0)
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                if (CountryIDS > 0)
                {
                    var res = h.GetAsync("Governorate" + "?ByCountryID=" + CountryIDS).Result;
                    var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                    return Json(new { Result = "OK", Options = lst });
                }
                else
                {
                    var res = h.GetAsync("Governorate" + "?ADDEDIT=1").Result;
                    var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                    return Json(new { Result = "OK", Options = lst });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        [HttpPost]
        public JsonResult Get_City_AddEditBYID(int GovernorateIDS = 0)
        {
            try
            {
                h.BaseAddress = new Uri(Link);
                if (GovernorateIDS > 0)
                {
                    var res = h.GetAsync("City" + "?ByGovernorateID=" + GovernorateIDS).Result;
                    var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                    return Json(new { Result = "OK", Options = lst });
                }
                else
                {
                    var res = h.GetAsync("City" + "?ADDEDIT=1").Result;
                    var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                    return Json(new { Result = "OK", Options = lst });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //
    }
}