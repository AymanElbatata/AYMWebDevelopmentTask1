using AYMWebDevelopment.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AYMWebDevelopment.Models;
using AutoMapper;

namespace AYMWebDevelopment
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();

            // To start My Bundles
            MyBundleCls.MyBundleJSCS(BundleTable.Bundles);

            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            AutoMapper.Mapper.Initialize(X => X.AddProfile<AutoMapperProfile>());

            //var config = new MapperConfiguration(cfg => {cfg.AddProfile<AutoMapperProfile>();});
            //var mapper = config.CreateMapper();
            // or
            //var mapper = new Mapper(config);

            UnityConfig.RegisterComponents();

        }
    }
}
