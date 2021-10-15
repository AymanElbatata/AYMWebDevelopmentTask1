using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace AYMWebDevelopment.App_Start
{
    public class MyBundleCls
    {
        public static void MyBundleJSCS(BundleCollection myobject)
        {

            myobject.Add(new StyleBundle("~/CSFILES1").Include(
                "~/Content/bootstrap.min.css"
                , "~/Content/themes/base/jquery-ui.css"
               , "~/Scripts/jtable/themes/lightcolor/gray/jtable.min.css"
               , "~/Content/chosen.css"
               , "~/Content/validationEngine.jquery.css"
               , "~/Content/themes/base/all.css"
               //, "~/DesignTemps/html5up-phantom/assets/webfonts/fa-brands-400.woff2"
               //, "~/DesignTemps/html5up-phantom/assets/webfonts/fa-solid-900.woff2"
                ));

            myobject.Add(new StyleBundle("~/CSFILES2").Include(

           // "~/DesignTemps/ahmeddesign1/css/font-awesome.min.css"
           //, "~/DesignTemps/ahmeddesign1/css/style.css"
           //, "~/DesignTemps/ahmeddesign1/css/animate.css"
           //, "~/DesignTemps/ahmeddesign1/css/owl.carousel.css"

           //  "~/DesignTemps/html5up-twenty/assets/css/main.css"
           //, "~/DesignTemps/html5up-twenty/assets/css/noscript.css"
           //"~/DesignTemps/html5up-stellar/assets/css/fontawesome-all.min.css"
           // "~/DesignTemps/html5up-stellar/assets/css/main.css"
           //, "~/DesignTemps/html5up-stellar/assets/css/noscript.css"
           "~/DesignTemps/DesignAYM01/CSS/CSS1.css"
          //, "~/Content/StyleSheet1AYM.css"
          ));

            myobject.Add(new ScriptBundle("~/JSFILES1").Include(
             "~/scripts/modernizr-2.8.3.js"
            //, "~/Scripts/jquery-3.5.1.min.js"
            , "~/Scripts/AYM/jquery-2.2.4.min.js"
            , "~/Scripts/jquery.validate.min.js"
            , "~/Scripts/jquery.validate.unobtrusive.min.js"
            , "~/Scripts/jquery.unobtrusive-ajax.min.js"
            , "~/Scripts/jquery-ui-1.12.1.min.js"
            , "~/Scripts/jtable/jquery.jtable.min.js"
            , "~/Scripts/chosen.jquery.min.js"
            , "~/Scripts/jquery.validationEngine.js"
            , "~/Scripts/jquery.validationEngine-en.js"

         ));

     //       myobject.Add(new ScriptBundle("~/JSFILES2").Include(
     // //"~/DesignTemps/ahmeddesign1/js/popper.js"
     // //, "~/DesignTemps/ahmeddesign1/js/main.js"
     // //, "~/DesignTemps/ahmeddesign1/js/wow.min.js"
     // //, "~/DesignTemps/ahmeddesign1/js/owl.carousel.min.js"

     // //  "~/DesignTemps/html5up-twenty/assets/js/jquery.dropotron.min.js"
     // //, "~/DesignTemps/html5up-twenty/assets/js/jquery.scrolly.min.js"
     // //, "~/DesignTemps/html5up-twenty/assets/js/jquery.scrollex.min.js"
     // //, "~/DesignTemps/html5up-twenty/assets/js/browser.min.js"
     // //, "~/DesignTemps/html5up-twenty/assets/js/breakpoints.min.js"
     // //, "~/DesignTemps/html5up-twenty/assets/js/util.js"
     // //, "~/DesignTemps/html5up-twenty/assets/js/main.js"

     // "~/DesignTemps/html5up-stellar/assets/js/jquery.scrolly.min.js"
     //, "~/DesignTemps/html5up-stellar/assets/js/jquery.scrollex.min.js"
     //, "~/DesignTemps/html5up-stellar/assets/js/browser.min.js"
     //, "~/DesignTemps/html5up-stellar/assets/js/breakpoints.min.js"
     //, "~/DesignTemps/html5up-stellar/assets/js/util.js"
     //, "~/DesignTemps/html5up-stellar/assets/js/main.js"

     //));


            myobject.Add(new ScriptBundle("~/JSFILES3").Include("~/scripts/bootstrap.min.js"));

            BundleTable.EnableOptimizations = false;
        }
    }
}