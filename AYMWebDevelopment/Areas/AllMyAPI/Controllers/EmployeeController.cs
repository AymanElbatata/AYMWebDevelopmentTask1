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
    public class EmployeeController : ApiController
    {

        AYMCOMPDataContext mydb = new AYMCOMPDataContext();

        [HttpGet]
        public HttpResponseMessage RetrieveAllData()
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                var data = mydb.EmployeeTBL.Where(A => A.IsDeleted == false).ToList();
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
                var data = mydb.EmployeeTBL.Where(A => A.IsDeleted == false && A.EmployeeID == Id).SingleOrDefault();

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
                var data = new List<EmployeeTBL>();
                if (type ==1)
                {
                    data = mydb.EmployeeTBL.Where(A => A.IsDeleted == false && A.AutoCode.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 2)
                {
                    data = mydb.EmployeeTBL.Where(A => A.IsDeleted == false && A.EmployeeEmail.StartsWith(StartSearchWord)).ToList();
                }            
                else if (type == 3)
                {
                    data = mydb.EmployeeTBL.Where(A => A.IsDeleted == false && A.EmployeeFullName.StartsWith(StartSearchWord)).ToList();
                }                         
                else if (type == 4)
                {
                    data = mydb.EmployeeTBL.Where(A => A.IsDeleted == false && A.EmployeePhone1.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 5)
                {
                    data = mydb.EmployeeTBL.Where(A => A.IsDeleted == false && A.EmployeePhone2.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 6)
                {
                    data = mydb.EmployeeTBL.Where(A => A.IsDeleted == false && A.EmployeeAddress.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 7)
                {
                    var data1 = mydb.EmployeeTBL.Where(A => A.IsDeleted == false).ToList();
                    foreach (var item in data1)
                    {
                        if (DatesComparison.DateEqualsORLessThanDate2((DateTime)StartingDate, (DateTime)EndingDate, (DateTime)item.DateOfBirth))
                        {
                            data.Add(item);
                        }
                    }
                }
                else if (type == 8)
                {
                    var data1 = mydb.EmployeeTBL.Where(A => A.IsDeleted == false).ToList();
                    foreach (var item in data1)
                    {
                        if (DatesComparison.DateEqualsORLessThanDate2((DateTime)StartingDate, (DateTime)EndingDate, (DateTime)item.DateOfHiring))
                        {
                            data.Add(item);
                        }
                    }
                }
                else if (type == 9)
                {
                    var data1 = mydb.EmployeeTBL.Where(A => A.IsDeleted == false).ToList();
                    foreach (var item in data1)
                    {
                        if (DatesComparison.DateEqualsORLessThanDate2((DateTime)StartingDate, (DateTime)EndingDate, (DateTime)item.DateOfeaving))
                        {
                            data.Add(item);
                        }
                    }
                }
                else if (type == 10)
                {
                    var data1 = mydb.EmployeeTBL.Where(A => A.IsDeleted == false).ToList();
                    foreach (var item in data1)
                    {
                        if (DatesComparison.DateEqualsORLessThanDate2((DateTime)StartingDate, (DateTime)EndingDate, (DateTime)item.DateOfMaking))
                        {
                            data.Add(item);
                        }
                    }
                }
                else if (type == 11)
                {
                    var data1 = mydb.EmployeeTBL.Where(A => A.IsDeleted == false && A.DateOfLastUpdate!=null).ToList();
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
        public HttpResponseMessage RetrieveOneBySearchEMailAndPW(string LoginEmail, string Password, int type)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;

                if (type == 1)
                {
                    var data = mydb.EmployeeTBL.Where(A => A.IsDeleted == false && A.EmployeeEmail == LoginEmail && A.EmployeePassword == Password).Any();

                    return Request.CreateResponse(HttpStatusCode.Accepted, data);
                }
                else
                {
                    var data = mydb.EmployeeTBL.Where(A => A.IsDeleted == false && A.EmployeeEmail == LoginEmail && A.EmployeePassword == Password).SingleOrDefault();
                    return Request.CreateResponse(HttpStatusCode.Accepted, data);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpGet]
        public HttpResponseMessage RetrieveMultipleByRecoverEmployeeEmail(string RecoverEmployeeEmail)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                var data = mydb.EmployeeTBL.Where(A => A.IsDeleted == false && A.EmployeeEmail == RecoverEmployeeEmail).SingleOrDefault();

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
                var continentals = mydb.EmployeeTBL.Where(a => a.IsDeleted == false).Select(c => new CustomOption { DisplayText = c.AutoCode, Value = c.EmployeeID }).ToList();
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
                var continentals = mydb.EmployeeTBL.Select(c => new CustomOption { DisplayText = c.AutoCode, Value = c.EmployeeID }).ToList();
                return Request.CreateResponse(HttpStatusCode.Accepted, continentals);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage CheckDataQueryCreate(EmployeeTBL myobject, int CreatePermission)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                int result;
                var data = mydb.EmployeeTBL.Where(A => A.IsDeleted == false && (A.EmployeeEmail == myobject.EmployeeEmail || A.AutoCode == myobject.AutoCode || A.PassportOrIDNumber == myobject.PassportOrIDNumber)).Count();
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
        public HttpResponseMessage CheckDataQueryUpdate(EmployeeTBL myobject, int UpdatePermission)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                int result;
                var data = mydb.EmployeeTBL.Where(A => A.IsDeleted == false && (A.EmployeeEmail == myobject.EmployeeEmail || A.AutoCode == myobject.AutoCode || A.PassportOrIDNumber == myobject.PassportOrIDNumber) && A.EmployeeID != myobject.EmployeeID).Count();
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
        public HttpResponseMessage AddNewRow(EmployeeTBL myobject, int Addnew, string APISAuthPinCode, string APISAuthPassword)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                bool isallowed = mydb.CallingAPISAuthTBL.Where(x => x.CallingAPISAuthPinCode == APISAuthPinCode && x.CallingAPISAuthPassword == APISAuthPassword && x.IsDeleted == false).Any();
                if (isallowed)
                {
                    myobject.IsDeleted = false;
                    var data = mydb.EmployeeTBL.Add(myobject);
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
        public HttpResponseMessage EditExistsRow(EmployeeTBL myobject, int EditExists, string APISAuthPinCode, string APISAuthPassword)
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
                    var data = mydb.EmployeeTBL.Find(myobject.EmployeeID);
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
        public HttpResponseMessage StopExistsRow(EmployeeTBL myobject, int Del, string APISAuthPinCode, string APISAuthPassword)
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
                    var data = mydb.EmployeeTBL.Find(myobject.EmployeeID);
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
