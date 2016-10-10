using System.Web.Mvc;

namespace FinancePro.Areas.AdminArea
{
    public class AdminAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AdminArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              "admin_login",
              "ad/login.html",
              new { controller = "Default", action = "Login", id = UrlParameter.Optional }
          );
            context.MapRoute(
                "AdminArea_default",
                "AdminArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
