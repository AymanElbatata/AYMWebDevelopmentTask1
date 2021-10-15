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
    public class ClientController : ApiController
    {

        AYMCOMPDataContext mydb = new AYMCOMPDataContext();

        [HttpGet]
        public HttpResponseMessage RetrieveAllData()
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                var data = mydb.ClientTBL.Where(A => A.IsDeleted == false).ToList();
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
                var data = mydb.ClientTBL.Where(A => A.IsDeleted == false && A.ClientID == Id).SingleOrDefault();

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
                var data = new List<ClientTBL>();
                if (type == 1)
                {
                    data = mydb.ClientTBL.Where(A => A.IsDeleted == false && A.AutoCode.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 2)
                {
                    data = mydb.ClientTBL.Where(A => A.IsDeleted == false && A.ClientEmail1.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 3)
                {
                    data = mydb.ClientTBL.Where(A => A.IsDeleted == false && A.ClientEmail2.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 4)
                {
                    data = mydb.ClientTBL.Where(A => A.IsDeleted == false && A.ClientPhone1.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 5)
                {
                    data = mydb.ClientTBL.Where(A => A.IsDeleted == false && A.ClientPhone2.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 6)
                {
                    data = mydb.ClientTBL.Where(A => A.IsDeleted == false && A.ClientWhatsAppNumber.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 7)
                {
                    data = mydb.ClientTBL.Where(A => A.IsDeleted == false && A.ClientFullName.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 8)
                {
                    data = mydb.ClientTBL.Where(A => A.IsDeleted == false && A.ClientFaceBookURL.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 9)
                {
                    data = mydb.ClientTBL.Where(A => A.IsDeleted == false && A.ClientAddress.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 10)
                {
                    data = mydb.ClientTBL.Where(A => A.IsDeleted == false && A.CountryTBL.CountryName.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 11)
                {
                    data = mydb.ClientTBL.Where(A => A.IsDeleted == false && A.GovernorateTBL.GovernorateName.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 12)
                {
                    data = mydb.ClientTBL.Where(A => A.IsDeleted == false && A.CityTBL.CityName.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 13)
                {
                    data = mydb.ClientTBL.Where(A => A.IsDeleted == false && A.NationalityTBL.NationalityName.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 14)
                {
                    data = mydb.ClientTBL.Where(A => A.IsDeleted == false && A.ProfessionTBL.ProfessionName.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 15)
                {
                    data = mydb.ClientTBL.Where(A => A.IsDeleted == false && A.GenderTBL.GenderName.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 16)
                {
                    data = mydb.ClientTBL.Where(A => A.IsDeleted == false && A.MaritalStatusTBL.MaritalStatusName.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 17)
                {
                    data = mydb.ClientTBL.Where(A => A.IsDeleted == false && A.EducationTBL.EducationName.StartsWith(StartSearchWord)).ToList();
                }
                else if (type == 18)
                {
                    var data1 = mydb.ClientTBL.Where(A => A.IsDeleted == false).ToList();
                    foreach (var item in data1)
                    {
                        if (DatesComparison.DateEqualsORLessThanDate2((DateTime)StartingDate, (DateTime)EndingDate, (DateTime)item.DateOfBirth))
                        {
                            data.Add(item);
                        }
                    }
                }
                else if (type == 19)
                {
                    var data1 = mydb.ClientTBL.Where(A => A.IsDeleted == false).ToList();
                    foreach (var item in data1)
                    {
                        if (DatesComparison.DateEqualsORLessThanDate2((DateTime)StartingDate, (DateTime)EndingDate, (DateTime)item.DateOfMaking))
                        {
                            data.Add(item);
                        }
                    }
                }
                else if (type == 20)
                {
                    var data1 = mydb.ClientTBL.Where(A => A.IsDeleted == false).ToList();
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
                var continentals = mydb.ClientTBL.Where(a => a.IsDeleted == false).Select(c => new CustomOption { DisplayText = c.AutoCode, Value = c.ClientID }).ToList();
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
                var continentals = mydb.ClientTBL.Select(c => new CustomOption { DisplayText = c.AutoCode, Value = c.ClientID }).ToList();
                return Request.CreateResponse(HttpStatusCode.Accepted, continentals);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage CheckDataQueryCreate(ClientTBL myobject, int CreatePermission)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                int result;
                var data = mydb.ClientTBL.Where(A => A.IsDeleted == false && (A.AutoCode == myobject.AutoCode || A.PassportOrIDNumber == myobject.PassportOrIDNumber)).Count();
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
        public HttpResponseMessage CheckDataQueryUpdate(ClientTBL myobject, int UpdatePermission)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                int result;
                var data = mydb.ClientTBL.Where(A => A.IsDeleted == false && ( A.AutoCode == myobject.AutoCode || A.PassportOrIDNumber == myobject.PassportOrIDNumber) && A.ClientID != myobject.ClientID).Count();
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
        public HttpResponseMessage AddNewRow(ClientTBL myobject, int Addnew, string APISAuthPinCode, string APISAuthPassword)
        {
            try
            {
                mydb.Configuration.ProxyCreationEnabled = false;
                bool isallowed = mydb.CallingAPISAuthTBL.Where(x => x.CallingAPISAuthPinCode == APISAuthPinCode && x.CallingAPISAuthPassword == APISAuthPassword && x.IsDeleted == false).Any();
                if (isallowed)
                {
                    myobject.IsDeleted = false;
                    var data = mydb.ClientTBL.Add(myobject);
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
        public HttpResponseMessage EditExistsRow(ClientTBL myobject, int EditExists, string APISAuthPinCode, string APISAuthPassword)
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
                    var data = mydb.ClientTBL.Find(myobject.ClientID);
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
        public HttpResponseMessage StopExistsRow(ClientTBL myobject, int Del, string APISAuthPinCode, string APISAuthPassword)
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
                    var data = mydb.ClientTBL.Find(myobject.ClientID);
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
