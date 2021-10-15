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
    public class InternalChatController : ApiController
    {

        AYMCOMPDataContext mydb = new AYMCOMPDataContext();

        [HttpGet]
        public HttpResponseMessage RetrieveAllData()
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                var data = mydb.InternalChatTBL.Where(A => A.IsDeleted == false).ToList();
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
                var data = mydb.InternalChatTBL.Where(A => A.IsDeleted == false && A.InternalChatID == Id).SingleOrDefault();

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
                var data = mydb.InternalChatTBL.Where(A => A.IsDeleted == false && A.TextMessage.StartsWith(StartSearchWord)).ToList();

                return Request.CreateResponse(HttpStatusCode.Accepted, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpGet]
        public HttpResponseMessage RetrieveMultipleBySearchToday(int UpdateToday)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                var data = new List<InternalChatTBL>();
                var data1 = mydb.InternalChatTBL.Where(A => A.IsDeleted == false).ToList();
                var newdata = new List<InternalChatModel2>();
                foreach (var item in data1)
                {
                    if (DatesComparison.DateEqualsORLessThanDate2((DateTime)DateTime.Now, (DateTime)DateTime.Now, (DateTime)item.DateOfMaking))
                    {
                        data.Add(item);
                    }
                }

                foreach (var item in data)
                {
                    var model = new InternalChatModel2();
                    model.DateOfMaking = item.DateOfMaking.ToString();
                    model.TextMessage = item.TextMessage;
                    var emp = mydb.EmployeeTBL.Include(x=>x.EmployeeTypeTBL).Where(x=>x.EmployeeID == item.EmployeeIDD).SingleOrDefault();
                    model.EmployeeAutoCode = emp.AutoCode;
                    model.EmployeeTypeName = emp.EmployeeTypeTBL.EmployeeTypeName;
                    newdata.Add(model);
                }

                return Request.CreateResponse(HttpStatusCode.Accepted, newdata);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpGet]
        public HttpResponseMessage RetrieveMultipleBySearchTODATES(int UpdateChatByDates, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                var data = new List<InternalChatTBL>();
                var data1 = mydb.InternalChatTBL.Where(A => A.IsDeleted == false).ToList();
                var newdata = new List<InternalChatModel2>();
                foreach (var item in data1)
                {
                    if (DatesComparison.DateEqualsORLessThanDate2((DateTime)DateFrom, (DateTime)DateTo, (DateTime)item.DateOfMaking))
                    {
                        data.Add(item);
                    }
                }

                foreach (var item in data)
                {
                    var model = new InternalChatModel2();
                    model.DateOfMaking = item.DateOfMaking.ToString();
                    model.TextMessage = item.TextMessage;
                    var emp = mydb.EmployeeTBL.Include(x => x.EmployeeTypeTBL).Where(x => x.EmployeeID == item.EmployeeIDD).SingleOrDefault();
                    model.EmployeeAutoCode = emp.AutoCode;
                    model.EmployeeTypeName = emp.EmployeeTypeTBL.EmployeeTypeName;
                    newdata.Add(model);
                }

                return Request.CreateResponse(HttpStatusCode.Accepted, newdata);
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
        public HttpResponseMessage AddNewRow(InternalChatTBL myobject, int Addnew, string APISAuthPinCode, string APISAuthPassword)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                bool isallowed = mydb.CallingAPISAuthTBL.Where(x => x.CallingAPISAuthPinCode == APISAuthPinCode && x.CallingAPISAuthPassword == APISAuthPassword && x.IsDeleted == false).Any();
                if (isallowed)
                {
                    myobject.IsDeleted = false;
                    var data = mydb.InternalChatTBL.Add(myobject);
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
        public HttpResponseMessage EditExistsRow(InternalChatTBL myobject, int EditExists, string APISAuthPinCode, string APISAuthPassword)
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
                    var data = mydb.InternalChatTBL.Find(myobject.InternalChatID);
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
        public HttpResponseMessage StopExistsRow(CountryTBL myobject, int Del, string APISAuthPinCode, string APISAuthPassword)
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
                    var data = mydb.CountryTBL.Find(myobject.CountryID);
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
