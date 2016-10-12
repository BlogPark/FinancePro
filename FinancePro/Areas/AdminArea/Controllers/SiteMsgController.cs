using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancePro.Areas.AdminArea.Models;
using FinancePro.BLLData;
using FinancePro.Controllers;
using FinancePro.DataModels;
using FinancePro.Models;

namespace FinancePro.Areas.AdminArea.Controllers
{
    public class SiteMsgController : Controller
    {
        //
        // GET: /AdminArea/SiteMsg/
        AdminSiteNewsBLL bll = new AdminSiteNewsBLL();
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            SiteMsgIndexViewModel model = new SiteMsgIndexViewModel();
            model.modellist = bll.GetModelListByUserID(0);
            return View(model);
        }
        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="addmodel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult addnotice(AdminSiteNewsModel addmodel)
        {
            if (addmodel == null)
            {
                return RedirectToAction("Index", "SysNotice", new { area = "AdminArea" });
            }
            SessionLoginModel user = Session[AppContent.SESSION_LOGIN_NAME] as SessionLoginModel;
            if (user == null)
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }
            addmodel.SendUserID = user.User.ID;
            addmodel.SendUserName = user.User.UserName;
            addmodel.SStatus = 1;
            addmodel.ReceiveUserID = 0;
            addmodel.ReceiveUserName = "全体会员";
            int id = bll.AddAdminSiteNew(addmodel);
            return RedirectToAction("Index", "SiteMsg", new { area = "AdminArea" });
        }
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult delenotice(int id)
        {
            if (id == 0)
            {
                return Json("0");
            }
            bool success = bll.UpdateStatus(id, 3);
            if (success)
            {
                return Json("1");
            }
            else { return Json("0"); }
        }

        /// <summary>
        /// 会员留言管理
        /// </summary>
        /// <returns></returns>
        public ActionResult MemberMsg()
        {
            MemberMsgViewModel model = new MemberMsgViewModel();
            model.list = bll.GetAllContractMessage();
            return View(model);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdateMsg(WebContactMessageModel updatemodel)
        {
            if (updatemodel != null)
            {
                bool row = bll.UpdateMsg(updatemodel);
            }
            return RedirectToAction("MemberMsg", "SiteMsg", new { area = "AdminArea" });
        }
        [HttpPost]
        public ActionResult deletemembermsg(int id)
        {
            bool row = bll.deleteMsg(id);
            if (row)
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
