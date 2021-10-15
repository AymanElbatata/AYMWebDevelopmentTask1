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
    public class EducationController : ApiController
    {

        AYMCOMPDataContext mydb = new AYMCOMPDataContext();

        [HttpGet]
        public HttpResponseMessage RetrieveAllData()
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                var data = mydb.EducationTBL.Where(A => A.IsDeleted == false).ToList();
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
                var data = mydb.EducationTBL.Where(A => A.IsDeleted == false && A.EducationID == Id).SingleOrDefault();

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
                var data = mydb.EducationTBL.Where(A => A.IsDeleted == false && A.EducationName.StartsWith(StartSearchWord)).ToList();

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
                var continentals = mydb.EducationTBL.Where(a => a.IsDeleted == false).Select(c => new CustomOption { DisplayText = c.EducationName, Value = c.EducationID }).ToList();
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
                var continentals = mydb.EducationTBL.Select(c => new CustomOption { DisplayText = c.EducationName, Value = c.EducationID }).ToList();
                return Request.CreateResponse(HttpStatusCode.Accepted, continentals);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage CheckDataQueryCreate(EducationTBL myobject, int CreatePermission)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                int result;
                var data = mydb.EducationTBL.Where(A => A.IsDeleted == false && A.EducationName == myobject.EducationName).Count();
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
        public HttpResponseMessage CheckDataQueryUpdate(EducationTBL myobject, int UpdatePermission)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                int result;
                var data = mydb.EducationTBL.Where(A => A.IsDeleted == false && A.EducationName == myobject.EducationName && A.EducationID != myobject.EducationID).Count();
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
        public HttpResponseMessage AddNewRow(EducationTBL myobject, int Addnew, string APISAuthPinCode, string APISAuthPassword)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                bool isallowed = mydb.CallingAPISAuthTBL.Where(x => x.CallingAPISAuthPinCode == APISAuthPinCode && x.CallingAPISAuthPassword == APISAuthPassword && x.IsDeleted == false).Any();
                if (isallowed)
                {
                    myobject.IsDeleted = false;
                    var data = mydb.EducationTBL.Add(myobject);
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
        public HttpResponseMessage EditExistsRow(EducationTBL myobject, int EditExists, string APISAuthPinCode, string APISAuthPassword)
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
                    var data = mydb.EducationTBL.Find(myobject.EducationID);
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
        public HttpResponseMessage StopExistsRow(EducationTBL myobject, int Del, string APISAuthPinCode, string APISAuthPassword)
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
                    var data = mydb.EducationTBL.Find(myobject.EducationID);
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
