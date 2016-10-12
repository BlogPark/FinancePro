using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancePro.Areas.AdminArea.Models;
using FinancePro.BLLData;
using FinancePro.DataModels;
using Webdiyer.WebControls.Mvc;

namespace FinancePro.Areas.AdminArea.Controllers
{
    public class MemberCashOrderController : Controller
    {
        //会员提现管理
        // GET: /AdminArea/MemberCashOrder/
        private MemberCashOrderBLL cashorderbll = new MemberCashOrderBLL();
        private int PageSize = 30;
        public ActionResult Index(MemberCashOrderModel cashorder,int page=1)
        {
            int totalrowcount = 0;
            cashorder.PageIndex = page;
            cashorder.PageSize = PageSize;
            List<MemberCashOrderModel> membercashorderlist = cashorderbll.GetAllMemberCashByPage(cashorder, out totalrowcount);
            PagedList<MemberCashOrderModel> pageList = null;
            if (membercashorderlist != null)
            {
                pageList = new PagedList<MemberCashOrderModel>(membercashorderlist, page, PageSize, totalrowcount);
            }
            this.ViewData["cashorder.CStatus"] = GetStatusListItem(0);
            MemberCashOrderIndexViewModel model = new MemberCashOrderIndexViewModel();
            model.cashorderlist = pageList;
            model.totalcount = totalrowcount;
            model.pagesize = PageSize;
            model.currentpage = page;
            ViewBag.PageTitle = "会员提现列表";
            return View(model);
        }
        /// <summary>
        /// 得到状态列表
        /// </summary>
        /// <param name="defval"></param>
        /// <returns></returns>
        private List<SelectListItem> GetStatusListItem(int defval = 1)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "全部", Value = "0", Selected = defval == 0 });
            items.Add(new SelectListItem { Text = "新申请", Value = "1", Selected = defval == 1 });
            items.Add(new SelectListItem { Text = "已打款", Value = "2", Selected = defval == 2 });
            items.Add(new SelectListItem { Text = "已驳回", Value = "3", Selected = defval == 3 });
            return items;
        }
        /// <summary>
        /// 更改单据的状态信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult updatecashorderstatus(int id,int status)
        {
            int result = cashorderbll.UpdateMemberCashOrderStatus(id, status);
            if (result > 0)
            {
                return Json("1");
            }
            else
            {
                return Json("0");
            }
        }
    }
}
