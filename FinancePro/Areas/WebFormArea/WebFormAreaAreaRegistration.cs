using System.Web.Mvc;

namespace FinancePro.Areas.WebFormArea
{
    public class WebFormAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "WebFormArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
            "web_register",
            "register.html",
            new { controller = "Register", action = "Index", id = UrlParameter.Optional }
        );
            context.MapRoute(
             "web_login",
             "login.html",
             new { controller = "Login", action = "Index", id = UrlParameter.Optional }
         );
            context.MapRoute(
                "WebFormArea_default",
                "WebFormArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
