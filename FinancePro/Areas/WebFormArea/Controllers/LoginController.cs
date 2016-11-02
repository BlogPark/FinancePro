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
            MemberInfoModel model = new MemberInfoModel();
            ViewData["ErrorStr"] = "";
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
            if (string.IsNullOrWhiteSpace(member.MemberCode))
            {
                ViewData["ErrorStr"] = "账号未填写！";
                return View(member);
            }
            if (string.IsNullOrWhiteSpace(member.MemberLogPwd))
            {
                ViewData["ErrorStr"] = "密码未填写！";
                return View(member);
            }
            if (string.IsNullOrWhiteSpace(member.VerificationCode))
            {
                ViewData["ErrorStr"] = "验证码未填写！";
                return View(member);
            }
            string vcode = Session[AppContent.VALICODE].ToString();
            if (member.VerificationCode.ToUpper() != vcode.ToUpper())
            {
                ViewData["ErrorStr"] = "验证码填写不正确！";
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
                ViewData["ErrorStr"] = logmsg;
                return View(member);
            }
        }
        /// <summary>
        /// 产生验证码图片
        /// </summary>
        /// <returns></returns>
        public ActionResult GetImg()
        {
            int width = 100;
            int height = 40;
            int fontsize = 20;
            string code = string.Empty;
            byte[] bytes = ValidateCode.CreateValidateGraphic(out code, 4, width, height, fontsize);
            Session[AppContent.VALICODE] = code;
            return File(bytes, @"image/jpeg");
        }

    }
}
