using System.Web.Mvc;

namespace AYMWebDevelopment.Areas.AllMyMVC
{
    public class AllMyMVCAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AllMyMVC";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AllMyMVC_default",
                "AllMyMVC/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}