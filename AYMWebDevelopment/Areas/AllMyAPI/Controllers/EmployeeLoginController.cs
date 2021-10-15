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
    public class EmployeeLoginController : ApiController
    {

        AYMCOMPDataContext mydb = new AYMCOMPDataContext();

        [HttpGet]
        public HttpResponseMessage RetrieveAllData()
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                var data = mydb.EmployeeLoginTBL.Where(A => A.IsDeleted == false).ToList();
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
                var data = mydb.EmployeeLoginTBL.Where(A => A.IsDeleted == false && A.EmployeeLoginID == Id).SingleOrDefault();

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
                var data = new List<EmployeeLoginTBL>();
                if (type == 1)
                {
                    data = mydb.EmployeeLoginTBL.Where(A => A.IsDeleted == false && A.EmployeeTBL.AutoCode.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 2)
                {
                    var data1 = mydb.EmployeeLoginTBL.Where(A => A.IsDeleted == false).ToList();
                    foreach (var item in data1)
                    {
                        if (DatesComparison.DateEqualsORLessThanDate2((DateTime)StartingDate, (DateTime)EndingDate, (DateTime)item.LoginTime))
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
        public HttpResponseMessage RetrieveOneBySearchSessionID(string sessionUnique, int type)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                if (type == 1)
                {
                    var data = mydb.EmployeeLoginTBL.Where(A => A.IsDeleted == false && A.SessionUnique == sessionUnique).SingleOrDefault();
                    return Request.CreateResponse(HttpStatusCode.Accepted, data);
                }
                else
                {
                    var data = mydb.EmployeeLoginTBL.Where(A => A.IsDeleted == false && A.SessionUnique == sessionUnique).SingleOrDefault();
                    var data2 = mydb.EmployeeTBL.Include(x => x.EmployeeTypeTBL).Where(x => x.EmployeeID == data.EmployeeIDD).SingleOrDefault();
                    EmployeeModel1 mydata = new EmployeeModel1();
                    mydata.EmployeeID = data2.EmployeeID;
                    mydata.EmployeeFullName = data2.EmployeeFullName;
                    mydata.AutoCode = data2.AutoCode;
                    mydata.EmployeeEmail = data2.EmployeeEmail;
                    mydata.EmployeeTypeTypeIDD = Convert.ToInt32(data2.EmployeeTypeIDD);
                    mydata.EmployeeTypeName = data2.EmployeeTypeTBL.EmployeeTypeName;
                    mydata.EmployeeTypeNumber = data2.EmployeeTypeTBL.EmployeeTypeNumber.ToString();

                    return Request.CreateResponse(HttpStatusCode.Accepted, mydata);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //[HttpGet]
        //public HttpResponseMessage RetrieveARRAYADDEDIT(int AddEDit)
        //{
        //    try
        //    {
        //        mydb.Configuration.ProxyCreationEnabled = false;
        //        var continentals = mydb.InternalChatTBL.Where(a => a.IsDeleted == false).Select(c => new CustomOption { DisplayText = c.TextMessage, Value = c.InternalChatID }).ToList();
        //        CustomOption empty = new CustomOption();
        //        empty.Value = null;
        //        empty.DisplayText = "-";
        //        continentals.Insert(0, empty);
        //        return Request.CreateResponse(HttpStatusCode.Accepted, continentals);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}


        //[HttpGet]
        //public HttpResponseMessage RetrieveARRAYADDEDITSerialized(int AddEditSerialize)
        //{
        //    try
        //    {
        //        mydb.Configuration.ProxyCreationEnabled = false;
        //        var continentals = mydb.InternalChatTBL.Where(a => a.IsDeleted == false).Select(c => new CustomOption { DisplayText = c.TextMessage, Value = c.InternalChatID }).ToList();
        //        CustomOption empty = new CustomOption();
        //        empty.Value = null;
        //        empty.DisplayText = "-";
        //        continentals.Insert(0, empty);
        //        string json = JsonConvert.SerializeObject(continentals);

        //        return Request.CreateResponse(HttpStatusCode.Accepted, json);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        //[HttpGet]
        //public HttpResponseMessage RetrieveARRAYListOnly(int Listonly)
        //{
        //    try
        //    {
        //        mydb.Configuration.ProxyCreationEnabled = false;
        //        var continentals = mydb.InternalChatTBL.Select(c => new CustomOption { DisplayText = c.TextMessage, Value = c.InternalChatID }).ToList();
        //        return Request.CreateResponse(HttpStatusCode.Accepted, continentals);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        //[HttpPost]
        //public HttpResponseMessage CheckDataQueryCreate(InternalChatTBL myobject, int CreatePermission)
        //{
        //    try
        //    {
        //        mydb.Configuration.ProxyCreationEnabled = false;
        //        int result;
        //        var data = mydb.InternalChatTBL.Where(A => A.IsDeleted == false && A.CountryName == myobject.CountryName).SingleOrDefault();
        //        if (data == null)
        //        {
        //            result = 0;
        //        }
        //        else
        //        {
        //            result = 1;
        //        }
        //        return Request.CreateResponse(HttpStatusCode.Accepted, result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        //[HttpPost]
        //public HttpResponseMessage CheckDataQueryUpdate(CountryTBL myobject, int UpdatePermission)
        //{
        //    try
        //    {
        //        mydb.Configuration.ProxyCreationEnabled = false;
        //        int result;
        //        var data = mydb.CountryTBL.Where(A => A.IsDeleted == false && A.CountryName == myobject.CountryName && A.CountryID != myobject.CountryID).SingleOrDefault();
        //        if (data == null)
        //        {
        //            result = 0;
        //        }
        //        else
        //        {
        //            result = 1;
        //        }
        //        return Request.CreateResponse(HttpStatusCode.Accepted, result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        [HttpPost]
        public HttpResponseMessage AddNewRow(EmployeeLoginTBL myobject, int Addnew, string APISAuthPinCode, string APISAuthPassword)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                bool isallowed = mydb.CallingAPISAuthTBL.Where(x => x.CallingAPISAuthPinCode == APISAuthPinCode && x.CallingAPISAuthPassword == APISAuthPassword && x.IsDeleted == false).Any();
                if (isallowed)
                {
                    myobject.IsDeleted = false;
                    var data = mydb.EmployeeLoginTBL.Add(myobject);
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
        public HttpResponseMessage EditExistsRow(EmployeeLoginTBL myobject, int EditExists, string APISAuthPinCode, string APISAuthPassword)
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
                    var data = mydb.EmployeeLoginTBL.Find(myobject.EmployeeLoginID);
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
        public HttpResponseMessage StopExistsRow(EmployeeLoginTBL myobject, int Del, string APISAuthPinCode, string APISAuthPassword)
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
                    var data = mydb.EmployeeLoginTBL.Find(myobject.EmployeeLoginID);
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
