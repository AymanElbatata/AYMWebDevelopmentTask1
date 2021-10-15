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
    public class ContactUSController : ApiController
    {

        AYMCOMPDataContext mydb = new AYMCOMPDataContext();

        [HttpGet]
        public HttpResponseMessage RetrieveAllData()
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                var data = mydb.ContactUSTBL.Where(A => A.IsDeleted == false).ToList();
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
                var data = mydb.ContactUSTBL.Where(A => A.IsDeleted == false && A.ContactID == Id).SingleOrDefault();

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
                var data = new List<ContactUSTBL>();
                if (type == 1)
                {
                    data = mydb.ContactUSTBL.Where(A => A.IsDeleted == false && A.ContactEmail.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 2)
                {
                    data = mydb.ContactUSTBL.Where(A => A.IsDeleted == false && A.ContactName.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 3)
                {
                    data = mydb.ContactUSTBL.Where(A => A.IsDeleted == false && A.ContactMessage.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 4)
                {
                    data = mydb.ContactUSTBL.Where(A => A.IsDeleted == false && A.ContactIPAddress.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 5)
                {
                    var data1 = mydb.ContactUSTBL.Where(A => A.IsDeleted == false).ToList();
                    foreach (var item in data1)
                    {
                        if (DatesComparison.DateEqualsORLessThanDate2((DateTime)StartingDate, (DateTime)EndingDate, (DateTime)item.DateOfMaking))
                        {
                            data.Add(item);
                        }
                    }
                }
                else if (type == 6)
                {
                    data = mydb.ContactUSTBL.Where(A => A.IsDeleted == false && A.IsSeen == false).ToList();
                }


                return Request.CreateResponse(HttpStatusCode.Accepted, data);
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
        //        var continentals = mydb.BranchTBL.Where(a => a.IsDeleted == false).Select(c => new CustomOption { DisplayText = c.BranchName, Value = c.BranchID }).ToList();
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
        //public HttpResponseMessage RetrieveARRAYListOnly(int Listonly)
        //{
        //    try
        //    {
        //        mydb.Configuration.ProxyCreationEnabled = false;
        //        var continentals = mydb.BranchTBL.Select(c => new CustomOption { DisplayText = c.BranchName, Value = c.BranchID }).ToList();
        //        return Request.CreateResponse(HttpStatusCode.Accepted, continentals);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        //[HttpPost]
        //public HttpResponseMessage CheckDataQueryCreate(BranchTBL myobject, int CreatePermission)
        //{
        //    try
        //    {
        //        mydb.Configuration.ProxyCreationEnabled = false;
        //        int result;
        //        var data = mydb.BranchTBL.Where(A => A.IsDeleted == false && A.BranchName == myobject.BranchName).SingleOrDefault();
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
        //public HttpResponseMessage CheckDataQueryUpdate(BranchTBL myobject, int UpdatePermission)
        //{
        //    try
        //    {
        //        mydb.Configuration.ProxyCreationEnabled = false;
        //        int result;
        //        var data = mydb.BranchTBL.Where(A => A.IsDeleted == false && A.BranchName == myobject.BranchName && A.BranchID != myobject.BranchID).SingleOrDefault();
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
        public HttpResponseMessage AddNewRow(ContactUSTBL myobject, int Addnew, string APISAuthPinCode, string APISAuthPassword)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                bool isallowed = mydb.CallingAPISAuthTBL.Where(x => x.CallingAPISAuthPinCode == APISAuthPinCode && x.CallingAPISAuthPassword == APISAuthPassword && x.IsDeleted == false).Any();
                if (isallowed)
                {
                    myobject.IsDeleted = false;
                    var data = mydb.ContactUSTBL.Add(myobject);
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
        public HttpResponseMessage EditExistsRow(ContactUSTBL myobject, int EditExists, string APISAuthPinCode, string APISAuthPassword)
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
                    var data = mydb.ContactUSTBL.Find(myobject.ContactID);
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
        public HttpResponseMessage StopExistsRow(ContactUSTBL myobject, int Del, string APISAuthPinCode, string APISAuthPassword)
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
                    var data = mydb.ContactUSTBL.Find(myobject.ContactID);
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
