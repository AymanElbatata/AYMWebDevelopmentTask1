using AYMWebDevelopment.Areas.AllMyAPI.Data;
using AYMWebDevelopment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Security;

namespace AYMWebDevelopment.Controllers
{
    public static class CurrentUserLoginData
    {
        static string Link = APILinks.GetAPILINK(1);

        private static AYMCOMPDataContext mydb = new AYMCOMPDataContext ();


        public static string getcurrentusrdataAuth(int type)
        {
            string result = "0";
            if (type == 8)
            {
                result = "AYMADMINTest";
            }
            else if (type == 9)
            {
                result = "APISPW@777js";
            }
            string currentsession = HttpContext.Current.User.Identity.Name;
            if (currentsession == "")
            {
                return result;
            }

            string API = "EmployeeLogin";
            HttpClient h = new HttpClient();
            h.BaseAddress = new Uri(Link);
            var res = h.GetAsync(API + "?sessionUnique=" + currentsession + "&type=2").Result;
            var usr = res.Content.ReadAsAsync<EmployeeModel1>().Result;

            // User ID
            if (type == 1 && usr.EmployeeID > 0)
            {
                 result = usr.EmployeeID.ToString();
            }
            // User Email
            else if (type == 2 && usr.EmployeeID > 0)
            {
                result = usr.AutoCode;
            }
            // User Name
            else if (type == 3 && usr.EmployeeID > 0)
            {
                result = usr.EmployeeEmail;
            }            
            else if (type == 4 && usr.EmployeeID > 0)
            {
                result = usr.EmployeeFullName;
            }
            // User Type
            else if (type == 5 && usr.EmployeeID > 0)
            {
                result = usr.EmployeeTypeNumber.ToString();
            } 
            else if (type == 6 && usr.EmployeeID > 0)
            {
                result = usr.EmployeeTypeName.ToString();
            }
            else if (type == 7 && usr.EmployeeID > 0)
            {
                result = currentsession;
            }
            return result;
        }


    }
}