using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancePro.Areas.WebFormArea.Models;
using FinancePro.BLLData;
using FinancePro.Controllers;
using FinancePro.DataModels;
using FinancePro.Filters;
using Webdiyer.WebControls.Mvc;

namespace FinancePro.Areas.WebFormArea.Controllers
{
    [WebLoginAttribute]
    public class WebHomeController : Controller
    {
        //网站前端页面
        // GET: /WebFormArea/WebHome/
        private AdminSiteNewsBLL sitenewsbll = new AdminSiteNewsBLL();
        private MemberCapitalDetailBLL capitaldetailbll = new MemberCapitalDetailBLL();
        private MemberInfoBLL memberbll = new MemberInfoBLL();
        private MemberTransferOrderBLL tranferbll = new MemberTransferOrderBLL();
        private OperateLogBLL logbll = new OperateLogBLL();
        private MemberCashOrderBLL cashbll = new MemberCashOrderBLL();
        private ApplyPOSBLL applybll = new ApplyPOSBLL();
        private int PageSize = 20;
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
            MemberCodeModel codemodel = MemberCodeBLL.GetMemberCode();
            AddMemberViewModel model = new AddMemberViewModel();
            model.member = new MemberInfoModel();
            model.member.MemberType = 2;
            model.member.SourceMemberCode = logmember.MemberCode;
            model.member.MemberCode = "JLY" + codemodel.MemberCode;
            model.member.MemberCodeID = codemodel.ID;
            model.regintable = ReginTableBLL.GetReginTableListModel(1);
            model.member.IsDerivativeMember = 1;
            return View(model);
        }
        /// <summary>
        /// 添加会员提交
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddMember(MemberInfoModel member)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            AddMemberViewModel model = new AddMemberViewModel();
            model.member = member;
            string result = memberbll.AddNewMemberInfo(member, member.MemberType);
            if (result == "1")
            {
                model.ErrorStr = "操作成功";
            }
            else
            {
                model.ErrorStr = result;
            }
            return View(model);
        }
        //会员列表
        public ActionResult MemberList()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            MemberListViewModel model = new MemberListViewModel();
            model.MemberList = memberbll.GetRecommendListByRecommendMemberId(logmember.ID);
            return View(model);
        }
        //会员子账户管理
        public ActionResult MemberChildList()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            MemberChildListViewModel model = new MemberChildListViewModel();
            model.memberlist = memberbll.GetChildCountListBySourceMemberCode(logmember.MemberCode);
            return View(model);
        }
        //提现
        public ActionResult ApplyCash()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            MemberCapitalDetailModel capitaldetail = capitaldetailbll.GetMemberCapitalDetailByMemberID(logmember.ID);
            ApplyCashViewModel model = new ApplyCashViewModel();
            model.memberpoint = capitaldetail.MemberPoints;
            model.cashorder = new MemberCashOrderModel() { CashBankCode = logmember.MemberBankCode, CashBankName = logmember.MemberBankName, CashBankUserName = logmember.MemberBankUserName };
            model.feenum = SystemConfigsBLL.GetConfigsValueByID(10).ParseToInt(20);
            model.basenum = SystemConfigsBLL.GetConfigsValueByID(20).ParseToInt(30);
            model.maxnum = SystemConfigsBLL.GetConfigsValueByID(13).ParseToInt(50);
            model.mincashnum = SystemConfigsBLL.GetConfigsValueByID(21).ParseToInt(50);
            return View(model);
        }
        /// <summary>
        /// 提现申请提交
        /// </summary>
        /// <param name="cashorder"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ApplyCash(MemberCashOrderModel cashorder)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            MemberCapitalDetailModel capitaldetail = capitaldetailbll.GetMemberCapitalDetailByMemberID(logmember.ID);
            ApplyCashViewModel model = new ApplyCashViewModel();
            MemberCashOrderModel sourcemodel = cashorder;
            sourcemodel.MemberCode = logmember.MemberCode;
            sourcemodel.MemberID = logmember.ID;
            sourcemodel.MemberName = logmember.MemberName;
            string result = cashbll.AddNewMemberCashOrder(sourcemodel);
            if (result != "1")
            {
                model.ErrorStr = result;
            }
            else
            {
                model.ErrorStr = "操作成功";
            }
            model.memberpoint = capitaldetail.MemberPoints;
            model.cashorder = new MemberCashOrderModel() { CashBankCode = logmember.MemberBankCode, CashBankName = logmember.MemberBankName, CashBankUserName = logmember.MemberBankUserName };
            model.feenum = SystemConfigsBLL.GetConfigsValueByID(10).ParseToInt(20);
            model.basenum = SystemConfigsBLL.GetConfigsValueByID(20).ParseToInt(30);
            model.maxnum = SystemConfigsBLL.GetConfigsValueByID(13).ParseToInt(50);
            model.mincashnum = SystemConfigsBLL.GetConfigsValueByID(21).ParseToInt(50);
            return View(model);
        }
        //提现列表
        public ActionResult CashList(int page = 1)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            CashListViewModel model = new CashListViewModel();
            int totalrowcount = 0;
            List<MemberCashOrderModel> list = cashbll.GetMemberCashByMemberId(logmember.ID, page, PageSize, out totalrowcount);
            PagedList<MemberCashOrderModel> pagelist = null;
            if (list != null)
            {
                pagelist = new PagedList<MemberCashOrderModel>(list, page, PageSize, totalrowcount);
            }
            model.cashlist = pagelist;
            return View(model);
        }
        //会员转账
        public ActionResult MemberTransfer()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            MemberTransferViewModel model = new MemberTransferViewModel();
            model.feetype = SystemConfigsBLL.GetConfigsValueByID(25).ParseToInt(0);
            return View(model);
        }
        /// <summary>
        /// 会员转账提交
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MemberTransfer(MemberTransferOrderModel transfer)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            MemberTransferOrderModel model = transfer;
            model.LaunchMemberID = logmember.ID;
            model.LaunchMemberCode = logmember.MemberCode;
            string result = capitaldetailbll.AddNewMemberTransferOrder(model);
            MemberTransferViewModel returnmodel = new MemberTransferViewModel();
            if (result == "1")
            {
                returnmodel.ErrorText = "";
                returnmodel.transfer = new MemberTransferOrderModel();
                returnmodel.feetype = SystemConfigsBLL.GetConfigsValueByID(25).ParseToInt(0);
            }
            else
            {
                returnmodel.ErrorText = result;
                returnmodel.transfer = new MemberTransferOrderModel();
                returnmodel.feetype = SystemConfigsBLL.GetConfigsValueByID(25).ParseToInt(0);
            }
            return View(returnmodel);
        }
        //转账列表
        public ActionResult Transferlist(int page = 1)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            TransferlistViewModel model = new TransferlistViewModel();
            int totalrowcount = 0;
            List<MemberTransferOrderModel> list = tranferbll.GetMemberTransferOrderByMemberID(logmember.ID, page, PageSize, out totalrowcount);
            PagedList<MemberTransferOrderModel> pagelist = null;
            if (list != null)
            {
                pagelist = new PagedList<MemberTransferOrderModel>(list, page, PageSize, totalrowcount);
            }
            model.transferlist = pagelist;
            return View(model);
        }
        //收益明细日志
        public ActionResult CapitalDetailLog(int page = 1)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            CapitalDetailLogViewModel model = new CapitalDetailLogViewModel();
            int totalrowcount = 0;
            List<MemberCapitalLogModel> list = logbll.GetMemberCapitalLogByMemberid(logmember.ID, page, PageSize, out totalrowcount);
            PagedList<MemberCapitalLogModel> pagelist = null;
            if (list != null)
            {
                pagelist = new PagedList<MemberCapitalLogModel>(list, page, PageSize, totalrowcount);
            }
            model.loglist = pagelist;
            return View(model);
        }
        //会员图谱
        public ActionResult MemberMap()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            MemberMapViewModel model = new MemberMapViewModel();
            int count = memberbll.recommendint(logmember.ID);
            model.member = logmember;
            model.childcount = count;
            model.isParent = count > 0;
            return View(model);
        }
        /// <summary>
        /// 查询子节点
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult getchildnote(int id)
        {
            List<RecommendMap> list = memberbll.GetRecommendListByRecommendMemberId2(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult getmemberinfo(int id)
        {
            MemberInfoModel model = memberbll.GetSingleMemberModel(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        //写消息
        public ActionResult WriteMessage()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            WriteMessageViewModel model = new WriteMessageViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult WriteMessage(WebContactMessageModel message)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            WriteMessageViewModel model = new WriteMessageViewModel();
            return View(model);
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
            UserInfoViewModel model = new UserInfoViewModel();
            model.userinfo = logmember;
            return View(model);
        }
        /// <summary>
        /// 用户修改信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserInfo(MemberInfoModel userinfo)
        {
            int result = memberbll.UpdateMemberInfoByMemberID(userinfo);
            if (result > 0)
            {
                ViewData["errorstr"] = "修改成功";
            }
            else
            {
                ViewData["errorstr"] = "操作失败";
            }
            UserInfoViewModel model = new UserInfoViewModel();
            model.userinfo = userinfo;
            return View(model);
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
        [HttpPost]
        public ActionResult UpdateLogpwd(string newpwd)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            int result = memberbll.UpdateMemberLogpwd(logmember.ID, newpwd); ;
            if (result == 0)
                return Json("0");
            else
                return Json("1");
        }
        [HttpPost]
        public ActionResult UpdateSecpwd(string newpwd)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            int result = memberbll.UpdateMemberMemberSecondPwd(logmember.ID, newpwd);
            if (result == 0)
                return Json("0");
            else
                return Json("1");
        }
        //申请POS
        public ActionResult ApplyPOS()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            ApplyPOSViewModel model = new ApplyPOSViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult ApplyPOS(ApplyPOSModel applymodel)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            ApplyPOSViewModel viewmodel = new ApplyPOSViewModel();
            ApplyPOSModel model = applymodel;
            model.MemberID = logmember.ID;
            model.MemberName = logmember.MemberName;
            model.MemberCode = logmember.MemberCode;
            string result = applybll.AddNewApplyPOS(model);
            if (result == "1")
            {
                viewmodel.ErrorStr = "";
            }
            else
            {
                viewmodel.ErrorStr = result;
            }
            return View(viewmodel);
        }
        // 激活会员
        [HttpPost]
        public ActionResult ActiveMember(int memberid)
        {
            string result = memberbll.ActiveMenber(memberid);
            return Json(result);
        }
    }
}
