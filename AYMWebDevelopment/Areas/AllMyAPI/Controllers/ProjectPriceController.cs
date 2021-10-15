using AYMWebDevelopment.Areas.AllMyAPI.Data;
using AYMWebDevelopment.Controllers;
using AYMWebDevelopment.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AYMWebDevelopment.Areas.AllMyAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProjectPriceController : ApiController
    {

        AYMCOMPDataContext mydb = new AYMCOMPDataContext();

        [HttpGet]
        public HttpResponseMessage RetrieveAllData()
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                var data = mydb.ProjectPriceTBL.Where(A => A.IsDeleted == false).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage RetrieveOneById(int Id)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                var data = mydb.ProjectPriceTBL.Where(A => A.IsDeleted == false && A.ProjectPriceID == Id).SingleOrDefault();

                return Request.CreateResponse(HttpStatusCode.Accepted, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage RetrieveMultipleBySearch(string StartSearchWord)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                var data = mydb.ProjectPriceTBL.Where(A => A.IsDeleted == false && A.ProjectPriceAmount.ToString().StartsWith(StartSearchWord)).ToList();

                return Request.CreateResponse(HttpStatusCode.Accepted, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpGet]
        public HttpResponseMessage RetrieveARRAYADDEDIT(int AddEDit)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                var continentals = mydb.ProjectPriceTBL.Where(a => a.IsDeleted == false).Select(c => new CustomOption { DisplayText = c.ProjectPriceAmount.ToString(), Value = c.ProjectPriceID }).ToList();
                CustomOption empty = new CustomOption();
                empty.Value = null;
                empty.DisplayText = "-";
                continentals.Insert(0, empty);
                return Request.CreateResponse(HttpStatusCode.Accepted, continentals);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        [HttpGet]
        public HttpResponseMessage RetrieveARRAYListOnly(int Listonly)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                var continentals = mydb.ProjectPriceTBL.Select(c => new CustomOption { DisplayText = c.ProjectPriceAmount.ToString(), Value = c.ProjectPriceID }).ToList();
                return Request.CreateResponse(HttpStatusCode.Accepted, continentals);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage CheckDataQueryCreate(ProjectPriceTBL myobject, int CreatePermission)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                int result;
                var data = mydb.ProjectPriceTBL.Where(A => A.IsDeleted == false && A.ProjectPriceAmount == myobject.ProjectPriceAmount).Count();
                if (data == 0)
                {
                    result = 0;
                }
                else
                {
                    result = 1;
                }
                return Request.CreateResponse(HttpStatusCode.Accepted, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage CheckDataQueryUpdate(ProjectPriceTBL myobject, int UpdatePermission)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                int result;
                var data = mydb.ProjectPriceTBL.Where(A => A.IsDeleted == false && A.ProjectPriceAmount == myobject.ProjectPriceAmount && A.ProjectPriceID != myobject.ProjectPriceID).Count();
                if (data == 0)
                {
                    result = 0;
                }
                else
                {
                    result = 1;
                }
                return Request.CreateResponse(HttpStatusCode.Accepted, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddNewRow(ProjectPriceTBL myobject, int Addnew, string APISAuthPinCode, string APISAuthPassword)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                bool isallowed = mydb.CallingAPISAuthTBL.Where(x => x.CallingAPISAuthPinCode == APISAuthPinCode && x.CallingAPISAuthPassword == APISAuthPassword && x.IsDeleted == false).Any();
                if (isallowed)
                {
                    myobject.IsDeleted = false;
                    var data = mydb.ProjectPriceTBL.Add(myobject);
                    mydb.SaveChanges();
                    string json = JsonConvert.SerializeObject(data);
                    return Request.CreateResponse(HttpStatusCode.Accepted, json);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Canceled");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage EditExistsRow(ProjectPriceTBL myobject, int EditExists, string APISAuthPinCode, string APISAuthPassword)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                bool isallowed = mydb.CallingAPISAuthTBL.Where(x => x.CallingAPISAuthPinCode == APISAuthPinCode && x.CallingAPISAuthPassword == APISAuthPassword && x.IsDeleted == false).Any();
                if (isallowed)
                {
                    myobject.IsDeleted = false;
                    mydb.Entry(myobject).State = EntityState.Modified;
                    mydb.SaveChanges();
                    var data = mydb.ProjectPriceTBL.Find(myobject.ProjectPriceID);
                    string json = JsonConvert.SerializeObject(data);
                    return Request.CreateResponse(HttpStatusCode.Accepted, json);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Canceled");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage StopExistsRow(ProjectPriceTBL myobject, int Del, string APISAuthPinCode, string APISAuthPassword)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                bool isallowed = mydb.CallingAPISAuthTBL.Where(x => x.CallingAPISAuthPinCode == APISAuthPinCode && x.CallingAPISAuthPassword == APISAuthPassword && x.IsDeleted == false).Any();
                if (isallowed)
                {
                    myobject.IsDeleted = true;
                    mydb.Entry(myobject).State = EntityState.Modified;
                    mydb.SaveChanges();
                    var data = mydb.ProjectPriceTBL.Find(myobject.ProjectPriceID);
                    string json = JsonConvert.SerializeObject(data);
                    return Request.CreateResponse(HttpStatusCode.Accepted, json);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Canceled");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //

    }
}
