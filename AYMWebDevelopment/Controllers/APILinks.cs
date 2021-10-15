using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AYMWebDevelopment.Controllers
{
    public class APILinks
    {
        public static string GetAPILINK(int type)
        {
            string Result = null;
            if (type == 1)
            {
                Result = "http://41.38.247.175/AYMWebDevelopment/AllPRJapis/";
                //"http://192.168.1.55/AYMWebDevelopment/AllPRJapis/";
                //http://aymanelbatata.vr.lt/AYMWebDevelopment/AllPRJapis/";
                //string API = "/api/Cities";
            }
            else if (type == 2)
            {
                Result = "https://localhost:44330/AllPRJapis/";
            }
            return Result;
        }

    }
}