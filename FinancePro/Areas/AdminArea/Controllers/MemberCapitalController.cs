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
    public class MemberCapitalController : Controller
    {
        //会员资产管理信息
        // GET: /AdminArea/MemberCapital/
        private MemberCapitalDetailBLL membercapitalbll = new MemberCapitalDetailBLL();
        private int PageSize = 30;
        /// <summary>
        /// 资产信息首页
        /// </summary>
        /// <param name="membercapital"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Index(MemberCapitalDetailModel membercapital, int page = 1)
        {
            int totalrowcount = 0;
            membercapital.PageIndex = page;
            membercapital.PageSize = PageSize;
            List<MemberCapitalDetailModel> memberlist = membercapitalbll.GetMemberCapitalDetailForPage(membercapital, out totalrowcount);
            PagedList<MemberCapitalDetailModel> pageList = null;
            if (memberlist != null)
            {
                pageList = new PagedList<MemberCapitalDetailModel>(memberlist, page, PageSize, totalrowcount);
            }
            MemberCapitalIndexViewModel model = new MemberCapitalIndexViewModel();
            model.membercapitallist = pageList;
            model.totalcount = totalrowcount;
            model.pagesize = PageSize;
            model.currentpage = page;
            ViewBag.PageTitle = "会员资产列表";
            return View(model);
        }
        /// <summary>
        /// 会员报单币页
        /// </summary>
        /// <param name="memberextend"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult FormCurreyDetail(MemberExtendInfoModel memberextend, int page = 1)
        {
            int totalrowcount = 0;
            memberextend.PageIndex = page;
            memberextend.PageSize = PageSize;
            List<MemberExtendInfoModel> memberlist = membercapitalbll.GetMemberExtendInfoForPage(memberextend, out totalrowcount);
            PagedList<MemberExtendInfoModel> pageList = null;
            if (memberlist != null)
            {
                pageList = new PagedList<MemberExtendInfoModel>(memberlist, page, PageSize, totalrowcount);
            }
            FormCurreyDetailViewModel model = new FormCurreyDetailViewModel();
            model.memberextendlist = pageList;
            model.totalcount = totalrowcount;
            model.pagesize = PageSize;
            model.currentpage = page;
            model.SysFormCurreyCount = SystemConfigsBLL.GetConfigsValueByID(1).ParseToInt(0);
            ViewBag.PageTitle = "会员报单币列表";
            return View(model);
        }
        /// <summary>
        /// 奖励会员
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult rewardmember(int memberid, int type, decimal count, string remark)
        {
            int result = membercapitalbll.RewardAndPunishmentMember(memberid, 1, count, type, remark);
            if (result > 0)
                return Json("1");
            else
                return Json("0");
        }
        /// <summary>
        /// 惩罚会员
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult punishmentmember(int memberid, int type, int count, string remark)
        {
            int result = membercapitalbll.RewardAndPunishmentMember(memberid, 2, count, type, remark);
            if (result > 0)
                return Json("1");
            else
                return Json("0");
        }
        /// <summary>
        ///奖励/惩罚会员报单币
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="type"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult randpformcurrey(int memberid, int type, int count)
        {
            if (type == 2)
            {
                count = 0 - count;
            }
            string result = membercapitalbll.SystemDistributeFormCurrey(memberid, count);
            return Json(result);
        }
        /// <summary>
        /// 追加系统报单币数量
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult createsysformcurrey()
        {
            int count=SystemConfigsBLL.GetConfigsValueByID(24).ParseToInt(0);
            if (membercapitalbll.AddSystemFormCurrey(count))
            {
                return Json("1");
            }
            else
                return Json("操作失败");
        }
        /// <summary>
        /// 动态分配领导奖
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult distributedynamicbonus()
        {
            string result = membercapitalbll.DistributeDynamicBonus();
            return Json(result);
        }
    }
}
