using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancePro.BLLData;
using FinancePro.DataModels;

namespace FinancePro.Controllers
{
    public class publicController : Controller
    {
        //
        // GET: /public/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult obtainreagin(int id)
        {
            List<ReginTableModel> list = ReginTableBLL.GetReginTableListModel(id);
            return Json(list);
        }
    }
}
