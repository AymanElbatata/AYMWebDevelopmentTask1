﻿using System;
using System.Web;
using System.Web.Mvc;

namespace AYMWebDevelopment.Controllers
{
    public class Base123Controller : Controller
    {
        //var ctx = filterContext.HttpContext;

        int currenuser = Convert.ToInt32(CurrentUserLoginData.getcurrentusrdataAuth(5));


        protected override void OnActionExecuting(ActionExecutingContext filterContext)

        {
            if (currenuser != 1 && currenuser != 2 && currenuser != 3)
            {
                // return   RedirectToAction("Login", "HomeAccount",new {area="" });
                filterContext.Result = new RedirectResult("~/Home/InvalidNotAllowed");
                return;
            }
        }


    }
}