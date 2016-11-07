using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancePro.Areas.AdminArea.Models;
using FinancePro.BLLData;
using FinancePro.DataModels;
using FinancePro.Filters;
using Webdiyer.WebControls.Mvc;

namespace FinancePro.Areas.AdminArea.Controllers
{
    [AdminLoginAttribute]
    public class ApplyPOSController : Controller
    {
        //POS机申请
        // GET: /AdminArea/ApplyPOS/
        private int PageSize = 30;
        private ApplyPOSBLL applybll = new ApplyPOSBLL(); 
        [HttpGet]
        public ActionResult Index(ApplyPOSModel applymodel, int page = 1)
        {
            int totalrowcount = 0;
            applymodel.PageIndex = page;
            applymodel.PageSize = PageSize;
            List<ApplyPOSModel> applylist = applybll.GetApplyPOSListByPage(applymodel, out totalrowcount);
            PagedList<ApplyPOSModel> pageList = null;
            if (applylist != null)
            {
                pageList = new PagedList<ApplyPOSModel>(applylist, page, PageSize, totalrowcount);
            }
            ApplyPOSIndexViewModel model = new ApplyPOSIndexViewModel();
            model.ApplyList = pageList;
            model.totalcount = totalrowcount;
            model.pagesize = PageSize;
            model.currentpage = page;
            this.ViewData["applymodel.PStatus"] = GetStatusListItem(0);
            return View(model);
        }
        /// <summary>
        /// 得到状态列表
        /// </summary>
        /// <param name="defval"></param>
        /// <returns></returns>
        private List<SelectListItem> GetStatusListItem(int defval = 0)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "全部", Value = "0", Selected = defval == 0 });
            items.Add(new SelectListItem { Text = "新申请", Value = "1", Selected = defval == 1 });
            items.Add(new SelectListItem { Text = "审核通过", Value = "2", Selected = defval == 2 });
            items.Add(new SelectListItem { Text = "已发货", Value = "3", Selected = defval == 3 });
            items.Add(new SelectListItem { Text = "审核失败", Value = "4", Selected = defval == 4 });
            return items;
        }
        [HttpPost]
        public ActionResult Getimagelist(int id)
        {
            List<string> images = applybll.GetApplyPOSImageByID(id);
            return Json(images);
        }

        [HttpPost]
        public ActionResult updatestatus(int id,int sta)
        {
            int rowcount = applybll.UpdatePStatusByID(id,sta);
            if (rowcount > 0)
            { return Json("1"); }
            else
            {
                return Json("0");
            }
        }
    }
}
