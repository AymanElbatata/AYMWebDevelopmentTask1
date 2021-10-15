using System.Web.Mvc;

namespace AYMWebDevelopment.Areas.AllMyAPI
{
    public class AllMyAPIAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AllMyAPI";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AllMyAPI_default",
                "AllMyAPI/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}