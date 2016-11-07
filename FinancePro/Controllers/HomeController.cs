using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancePro.Filters;

namespace FinancePro.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        [WebLoginAttribute]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
        }

    }
}
