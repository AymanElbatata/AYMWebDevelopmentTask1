using AYMWebDevelopment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace AYMWebDevelopment.Controllers
{
    public interface IRepository<T>
    {
        List<T> GetAll(string APINAME);
        List<T> GetAllByNames(string APINAME, string txtENBTNSearch);
        T AddNewRow(string APINAME, object T);
        T EditRow(string APINAME, object T);
        T DeleteRow(string APINAME, object T);
    }

    public class GenericRepository<T>: IRepository<T> where T : class
    {
        //private AYMCOMPDataContext mydb = new AYMCOMPDataContext();

        public List<T> GetAll(string APINAME)
        {
            string Link = APILinks.GetAPILINK(1);
            HttpClient h = new HttpClient();
            h.BaseAddress = new Uri(Link);
            var res = h.GetAsync(APINAME).Result;
            var mylst = res.Content.ReadAsAsync<List<T>>().Result;
            //var mylst = mydb.Set<T>().ToList();
            return mylst;
        }

        public List<T> GetAllByNames(string APINAME, string txtENBTNSearch)
        {
            string Link = APILinks.GetAPILINK(1);
            HttpClient h = new HttpClient();
            h.BaseAddress = new Uri(Link);
            var res = h.GetAsync(APINAME + "?StartSearchWord=" + txtENBTNSearch.Trim()).Result;
            var mylst = res.Content.ReadAsAsync<List<T>>().Result;
            //var mylst = mydb.Set<T>().ToList();
            return mylst;
        }


        public T AddNewRow(string APINAME, object model)
        {
            string Link = APILinks.GetAPILINK(1);
            HttpClient h = new HttpClient();
            h.BaseAddress = new Uri(Link);
            string APISAuthPinCode = CurrentUserLoginData.getcurrentusrdataAuth(8);
            string APISAuthPassword = CurrentUserLoginData.getcurrentusrdataAuth(9);
            var res = h.PostAsJsonAsync(APINAME + "?Addnew=1&APISAuthPinCode=" + APISAuthPinCode + "&APISAuthPassword=" + APISAuthPassword, model).Result;
            var mylst = res.Content.ReadAsAsync<T>().Result;
            return mylst;
        }


        public T EditRow(string APINAME, object model)
        {
            string Link = APILinks.GetAPILINK(1);
            HttpClient h = new HttpClient();
            h.BaseAddress = new Uri(Link);
            string APISAuthPinCode = CurrentUserLoginData.getcurrentusrdataAuth(8);
            string APISAuthPassword = CurrentUserLoginData.getcurrentusrdataAuth(9);
            var res = h.PostAsJsonAsync(APINAME + "?EditExists=1&APISAuthPinCode=" + APISAuthPinCode + "&APISAuthPassword=" + APISAuthPassword, model).Result;
            var mylst = res.Content.ReadAsAsync<T>().Result;
            return mylst;
        }


        public T DeleteRow(string APINAME, object model)
        {
            string Link = APILinks.GetAPILINK(1);
            HttpClient h = new HttpClient();
            h.BaseAddress = new Uri(Link);
            string APISAuthPinCode = CurrentUserLoginData.getcurrentusrdataAuth(8);
            string APISAuthPassword = CurrentUserLoginData.getcurrentusrdataAuth(9);
            var res = h.PostAsJsonAsync(APINAME + "?Del=1&APISAuthPinCode=" + APISAuthPinCode + "&APISAuthPassword=" + APISAuthPassword, model).Result;
            var mylst = res.Content.ReadAsAsync<T>().Result;
            return mylst;
        }

    }
}