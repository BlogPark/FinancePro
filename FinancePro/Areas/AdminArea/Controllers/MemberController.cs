using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancePro.Areas.AdminArea.Models;
using FinancePro.BLLData;
using FinancePro.Common;
using FinancePro.Controllers;
using FinancePro.DataModels;
using FinancePro.Filters;
using Webdiyer.WebControls.Mvc;

namespace FinancePro.Areas.AdminArea.Controllers
{
    [AdminLoginAttribute]
    public class MemberController : Controller
    {
        //会员操作相关页面
        // GET: /AdminArea/Member/
        private MemberInfoBLL memberbll = new MemberInfoBLL();
        private int PageSize = 30;
        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="member"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Index(MemberInfoModel member, int page = 1)
        {
            int totalrowcount = 0;
            member.PageIndex = page;
            member.PageSize = PageSize;
            List<MemberInfoModel> memberlist = memberbll.GetMemberListForPage(member, out totalrowcount);
            PagedList<MemberInfoModel> pageList = null;
            if (memberlist != null)
            {
                pageList = new PagedList<MemberInfoModel>(memberlist, page, PageSize, totalrowcount);
            }
            this.ViewData["member.MemberStatus"] = GetStatusListItem(0);
            MemberIndexViewModel model = new MemberIndexViewModel();
            model.memberlist = pageList;
            model.totalcount = totalrowcount;
            model.pagesize = PageSize;
            model.currentpage = page;
            ViewBag.PageTitle = "会员列表";
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
            items.Add(new SelectListItem { Text = "待激活", Value = "1", Selected = defval == 1 });
            items.Add(new SelectListItem { Text = "已激活", Value = "2", Selected = defval == 2 });
            items.Add(new SelectListItem { Text = "已冻结", Value = "3", Selected = defval == 3 });
            items.Add(new SelectListItem { Text = "已出局", Value = "4", Selected = defval == 4 });
            return items;
        }
        /// <summary>
        /// 修改会员信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type">2 激活 3 冻结 4 解冻 5 重置密码</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult updatesta(int id, int type)
        {
            string result = "";
            if (type == 2)
            {
                result = memberbll.ActiveMenber(id);//系统激活会员
            }
            else if (type == 5)//重置密码
            {
                string pwd = DESEncrypt.Encrypt("666666", AppContent.SecrectStr);
                result = memberbll.UpdateMemberLogpwd(id, pwd).ToString();
            }
            else
            {
                if (type == 4)
                {
                    type = 2;
                }
                result = memberbll.UpdateMemberStatus(id, type).ToString();
            }
            if (!result.StartsWith("0"))
            {
                return Json("1");
            }
            else
            {
                return Json(result);
            }
        }

        /// <summary>
        /// 添加会员
        /// </summary>
        /// <returns></returns>
        public ActionResult AddMember()
        {
            AddMemberViewModel model = new AddMemberViewModel();
            model.regintable = ReginTableBLL.GetReginTableListModel(1);
            return View(model);
        }
        [HttpPost]
        public ActionResult AddMember(MemberInfoModel member)
        {
            if (member != null)
            {
                MemberCodeModel codemodel = MemberCodeBLL.GetMemberCode();
                member.MemberCode = "JL" + codemodel.MemberCode;
                member.MemberCodeID = codemodel.ID;
                member.MemberType = 1;
                member.IsDerivativeMember = 0;
                member.MemberStatus = 1;
                member.MemberLogPwd = "666666";//加密密码
                member.MemberSecondPwd = "888888";//加密密码
                string row = memberbll.AddNewMemberInfo(member, 1);
            }
            return RedirectToActionPermanent("Index", "Member", new { area = "AdminArea" });
        }
        [HttpGet]
        public ActionResult EditMember(int mid)
        {
            EditMemberViewModel model = new EditMemberViewModel();
            model.member = memberbll.GetSingleMemberModel(mid);
            model.regintable = ReginTableBLL.GetReginTableListModel(1);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditMember(MemberInfoModel member)
        {
            if (member != null)
            {
                int row = memberbll.UpdateMemberInfoByMemberID(member);
            }
            return RedirectToActionPermanent("Index", "Member", new { area = "AdminArea" });
        }
        [HttpPost]
        public ActionResult obtainreagin(int id)
        {
            List<ReginTableModel> list = ReginTableBLL.GetReginTableListModel(id);
            return Json(list);
        }
        [HttpPost]
        public ActionResult addmembercode()
        {
            int result = MemberCodeBLL.CreateNewMemberCode();
            if (result == 1)
                return Json("1");
            else
                return Json("0");
        }
    }
}
