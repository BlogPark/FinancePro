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
                "WebFormArea_default",
                "WebFormArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
