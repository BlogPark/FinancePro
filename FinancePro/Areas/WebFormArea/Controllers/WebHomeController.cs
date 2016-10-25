using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancePro.Areas.WebFormArea.Models;
using FinancePro.BLLData;
using FinancePro.Controllers;
using FinancePro.DataModels;

namespace FinancePro.Areas.WebFormArea.Controllers
{
    public class WebHomeController : Controller
    {
        //网站前端页面
        // GET: /WebFormArea/WebHome/
        private AdminSiteNewsBLL sitenewsbll = new AdminSiteNewsBLL();
        private MemberCapitalDetailBLL capitaldetailbll = new MemberCapitalDetailBLL();
        private MemberInfoBLL memberbll = new MemberInfoBLL();
        //首页
        public ActionResult Index()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            WebHomeIndexViewModel model = new WebHomeIndexViewModel();
            model.MemberInfo = logmember;
            model.AdminSiteNews = sitenewsbll.GetModelListByUserID(logmember.ID, 8);
            model.CapitalDetail = capitaldetailbll.GetMemberCapitalDetailByMemberID(logmember.ID);
            model.FormCurreyCount = memberbll.GetMemberFormCurreyNum(logmember.ID);
            return View(model);
        }
        //添加会员
        public ActionResult AddMember()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            return View();
        }
        //会员列表
        public ActionResult MemberList()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            return View();
        }
        //提现
        public ActionResult ApplyCash()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            return View();
        }
        //提现列表
        public ActionResult CashList()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            return View();
        }
        //会员转账
        public ActionResult MemberTransfer()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            return View();
        }
        //转账列表
        public ActionResult Transferlist()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            return View();
        }
        //收益明细日志
        public ActionResult CapitalDetailLog()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }

            return View();
        }

        //会员图谱
        public ActionResult MemberMap()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            return View();
        }
        //写消息
        public ActionResult WriteMessage()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            return View();
        }
        //消息列表
        public ActionResult MessageList()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            MessageListViewModel model = new MessageListViewModel();
            model.WebContactList = sitenewsbll.GetContractMessage(logmember.ID);
            return View(model);
        }
        //用户资料
        public ActionResult UserInfo()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            return View();
        }
        //修改密码
        public ActionResult UpdatePwd()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            return View();
        }
    }
}
