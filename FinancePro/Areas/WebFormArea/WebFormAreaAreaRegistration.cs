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
         "web_apos",
         "apos.html",
         new { controller = "WebHome", action = "ApplyPOS", id = UrlParameter.Optional }
     );
            context.MapRoute(
         "web_upd",
         "upd.html",
         new { controller = "WebHome", action = "UpdatePwd", id = UrlParameter.Optional }
     );
            context.MapRoute(
         "web_membercenter",
         "membercenter.html",
         new { controller = "WebHome", action = "UserInfo", id = UrlParameter.Optional }
     );
            context.MapRoute(
         "web_messagelist",
         "messagelist.html",
         new { controller = "WebHome", action = "MessageList", id = UrlParameter.Optional }
     );
            context.MapRoute(
         "web_sendmag",
         "sendmag.html",
         new { controller = "WebHome", action = "WriteMessage", id = UrlParameter.Optional }
     );
            context.MapRoute(
         "web_membermap",
         "membermap.html",
         new { controller = "WebHome", action = "MemberMap", id = UrlParameter.Optional }
     );
            context.MapRoute(
         "web_detaillist",
         "detaillist.html",
         new { controller = "WebHome", action = "CapitalDetailLog", id = UrlParameter.Optional }
     );
            context.MapRoute(
         "web_transferorderlist",
         "transferorderlist.html",
         new { controller = "WebHome", action = "Transferlist", id = UrlParameter.Optional }
     );
            context.MapRoute(
         "web_transferorder",
         "transferorder.html",
         new { controller = "WebHome", action = "MemberTransfer", id = UrlParameter.Optional }
     );
            context.MapRoute(
         "web_cashorderlist",
         "cashorderlist.html",
         new { controller = "WebHome", action = "CashList", id = UrlParameter.Optional }
     );
            context.MapRoute(
         "web_cashorder",
         "cashorder.html",
         new { controller = "WebHome", action = "ApplyCash", id = UrlParameter.Optional }
     );
            context.MapRoute(
         "web_childaccountlist",
         "childaccountlist.html",
         new { controller = "WebHome", action = "MemberChildList", id = UrlParameter.Optional }
     );
            context.MapRoute(
          "web_memberlist",
          "memberlist.html",
          new { controller = "WebHome", action = "MemberList", id = UrlParameter.Optional }
      );
            context.MapRoute(
           "web_addmember",
           "addmember.html",
           new { controller = "WebHome", action = "AddMember", id = UrlParameter.Optional }
       );
            context.MapRoute(
           "web_index",
           "index.html",
           new { controller = "WebHome", action = "Index", id = UrlParameter.Optional }
       );
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
