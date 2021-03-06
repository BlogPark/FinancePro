﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancePro.BLLData;
using FinancePro.DataModels;
using FinancePro.Models;

namespace FinancePro.Controllers
{
    public class IndexPubController : Controller
    {
        //
        // GET: /IndexPub/
        private SysMenuAndUserBLL bll = new SysMenuAndUserBLL();

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 首页左侧菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult Menu(string currentpage)
        {
            SessionLoginModel model = Session[AppContent.SESSION_LOGIN_NAME] as SessionLoginModel;
            MenuViewModel models = new MenuViewModel();
            if (model != null)
            {
                string idstr = "";
                idstr = string.Join(",", model.UserMenus.Where(p => p.MenuType == 1).Select(p => p.FatherID).Distinct());
                models.firstlist = bll.GetSysMenuByIds(idstr.TrimEnd(','));
                models.sublist = model.UserMenus.Where(p => p.FatherID != 0).ToList();
                models.Currentpage = currentpage;
            }
            return View(models);
        }
        /// <summary>
        /// 管理员通知
        /// </summary>
        /// <returns></returns>
        public ActionResult Notification()
        {
            return View();
        }
        /// <summary>
        /// 管理员日程
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminTask()
        {
            return View();
        }
        /// <summary>
        /// 管理员消息
        /// </summary>
        /// <returns></returns>
        public ActionResult Message()
        {
            SessionLoginModel user = Session[AppContent.SESSION_LOGIN_NAME] as SessionLoginModel;
            partMessageViewModel model = new partMessageViewModel();
            if (user != null)
            {
                AdminSiteNewsBLL newsbll = new AdminSiteNewsBLL();
                List<AdminSiteNewsModel> list = newsbll.GetTop3ModelListByUserID(user.User.ID);
                if (list != null && list.Count > 0)
                {
                    model.newmsglist = list.Where(m => m.SStatus == 1).ToList();
                    model.newcount = model.newmsglist.Count;
                    model.oldmsglist = list.Where(m => m.SStatus == 2).ToList();
                }
            }
            return View(model);
        }
        /// <summary>
        /// 管理员信息
        /// </summary>
        /// <returns></returns>
        public ActionResult UserInfo()
        {
            SessionLoginModel model = Session[AppContent.SESSION_LOGIN_NAME] as SessionLoginModel;
            if (model != null)
            {
                return View(model.User);
            }
            else
            {
                return View(new SysAdminUserModel());
            }
        }

        public ActionResult LoginOut()
        {
            Session.Clear();// Session[AppContext.SESSION_LOGIN_NAME] = null;
            return RedirectToAction("Login", "Default", new { area = "AdminArea", returnurl = "" });
        }
        /// <summary>
        /// 网站左侧菜单
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult LeftWebMenu(int type)
        {
            ViewData["lefttype"] = type;
            return View();
        }
    }
}
