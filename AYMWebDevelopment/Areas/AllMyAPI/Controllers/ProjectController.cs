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
    public class ProjectController : ApiController
    {

        AYMCOMPDataContext mydb = new AYMCOMPDataContext();

        [HttpGet]
        public HttpResponseMessage RetrieveAllData()
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                var data = mydb.ProjectTBL.Where(A => A.IsDeleted == false).ToList();
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
                var data = mydb.ProjectTBL.Where(A => A.IsDeleted == false && A.ProjectID == Id).SingleOrDefault();

                return Request.CreateResponse(HttpStatusCode.Accepted, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage RetrieveMultipleBySearch(string StartSearchWord, int type, DateTime? StartingDate = null, DateTime? EndingDate = null)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                var data = new List<ProjectTBL>();
                if (type == 1)
                {
                    data = mydb.ProjectTBL.Where(A => A.IsDeleted == false && A.AutoCode.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 2)
                {
                    data = mydb.ProjectTBL.Where(A => A.IsDeleted == false && A.ClientTBL.AutoCode.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 3)
                {
                    data = mydb.ProjectTBL.Where(A => A.IsDeleted == false && (A.EmployeeTBL.AutoCode.StartsWith(StartSearchWord) || A.EmployeeTBL1.AutoCode.StartsWith(StartSearchWord))).ToList();
                }
                else if (type == 4)
                {
                    data = mydb.ProjectTBL.Where(A => A.IsDeleted == false && A.ProjectTypeTBL.ProjectTypeName.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 5)
                {
                    data = mydb.ProjectTBL.Where(A => A.IsDeleted == false && A.ProjectStatusTBL.ProjectStatusName.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 6)
                {
                    data = mydb.ProjectTBL.Where(A => A.IsDeleted == false && A.ProjectPriceTBL.ProjectPriceAmount.ToString().StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 7)
                {
                    var data1 = mydb.ProjectTBL.Where(A => A.IsDeleted == false).ToList();
                    foreach (var item in data1)
                    {
                        if (DatesComparison.DateEqualsORLessThanDate2((DateTime)StartingDate, (DateTime)EndingDate, (DateTime)item.DateOfMaking))
                        {
                            data.Add(item);
                        }
                    }
                }
                else if (type == 8)
                {
                    var data1 = mydb.ProjectTBL.Where(A => A.IsDeleted == false && A.DateOfLastUpdate != null).ToList();
                    foreach (var item in data1)
                    {
                        if (DatesComparison.DateEqualsORLessThanDate2((DateTime)StartingDate, (DateTime)EndingDate, (DateTime)item.DateOfLastUpdate))
                        {
                            data.Add(item);
                        }
                    }
                }
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
                var continentals = mydb.ProjectTBL.Where(a => a.IsDeleted == false).Select(c => new CustomOption { DisplayText = c.AutoCode, Value = c.ProjectID }).ToList();
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
                var continentals = mydb.ProjectTBL.Select(c => new CustomOption { DisplayText = c.AutoCode, Value = c.ProjectID }).ToList();
                return Request.CreateResponse(HttpStatusCode.Accepted, continentals);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage CheckDataQueryCreate(ProjectTBL myobject, int CreatePermission)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                int result;
                var data = mydb.ProjectTBL.Where(A => A.IsDeleted == false && A.AutoCode == myobject.AutoCode).Count();
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
        public HttpResponseMessage CheckDataQueryUpdate(ProjectTBL myobject, int UpdatePermission)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                int result;
                var data = mydb.ProjectTBL.Where(A => A.IsDeleted == false && A.AutoCode == myobject.AutoCode && A.ProjectID != myobject.ProjectID).Count();
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
        public HttpResponseMessage AddNewRow(ProjectTBL myobject, int Addnew, string APISAuthPinCode, string APISAuthPassword)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                bool isallowed = mydb.CallingAPISAuthTBL.Where(x => x.CallingAPISAuthPinCode == APISAuthPinCode && x.CallingAPISAuthPassword == APISAuthPassword && x.IsDeleted == false).Any();
                if (isallowed)
                {
                    myobject.IsDeleted = false;
                    var data = mydb.ProjectTBL.Add(myobject);
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
        public HttpResponseMessage EditExistsRow(ProjectTBL myobject, int EditExists, string APISAuthPinCode, string APISAuthPassword)
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
                    var data = mydb.ProjectTBL.Find(myobject.ProjectID);
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
        public HttpResponseMessage StopExistsRow(ProjectTBL myobject, int Del, string APISAuthPinCode, string APISAuthPassword)
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
                    var data = mydb.ProjectTBL.Find(myobject.ProjectID);
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
