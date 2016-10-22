using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancePro.BLLData;
using FinancePro.Common;
using FinancePro.Controllers;
using FinancePro.DataModels;
using FinancePro.Filters;

namespace FinancePro.Areas.WebFormArea.Controllers
{
     [WebLoginAttribute]
    public class LoginController : Controller
    {
        //登陆视图
        // GET: /WebFormArea/Login/

        MemberInfoBLL bll = new MemberInfoBLL();
        private WebSettingsBLL webbll = new WebSettingsBLL();
        private WebSettingsModel web;
        public LoginController()
        {
            web = webbll.GetWebSiteModel();
        }
        public ActionResult Index(string bl = "")
        {
            //string name = SysAdminConfigBLL.GetConfigValue(23);
            //if (name == "NewTemplateArea")
            //{
            //    return RedirectToAction("Index", "Login", new { area = "NewTemplateArea" });
            //}
            MemberInfoModel model = new MemberInfoModel();
            ViewBag.PageTitle = web.WebName;
            return View(model);
        }
        /// <summary>
        /// 会员登陆
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(MemberInfoModel member)
        {
            if (member == null)
            {
                return View(member);
            }
            string newpwd = DESEncrypt.Encrypt(member.MemberLogPwd, AppContent.SecrectStr);
            string logmsg = "";
            MemberInfoModel logmember = bll.MemberLogin(member.MemberCode, newpwd, out logmsg);
            if (logmsg == "1")
            {
                Session[AppContent.SESSION_WEB_LOGIN] = logmember;
                return RedirectToAction("Index", "WebHome", new { area = "WebFormArea" });
            }
            else
            {
                ViewBag.TempMsg = logmsg;
                return View(member);
            }
        }

    }
}
