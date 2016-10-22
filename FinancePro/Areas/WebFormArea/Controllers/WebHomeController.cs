using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinancePro.Areas.WebFormArea.Controllers
{
    public class WebHomeController : Controller
    {
        //网站前端页面
        // GET: /WebFormArea/WebHome/

        //首页
        public ActionResult Index()
        {
            return View();
        }
        //添加会员
        public ActionResult AddMember()
        {
            return View();
        }
       //会员列表
        public ActionResult MemberList()
        {
            return View();
        }
        //提现
        public ActionResult ApplyCash()
        {
            return View();
        }
        //提现列表
        public ActionResult CashList()
        {
            return View();
        }
        //会员转账
        public ActionResult MemberTransfer()
        {
            return View();
        }
        //转账列表
        public ActionResult Transferlist()
        {
            return View();
        }
        //收益明细日志
        public ActionResult CapitalDetailLog()
        {
            return View();
        }

        //会员图谱
        public ActionResult MemberMap()
        {
            return View();
        }
        //写消息
        public ActionResult WriteMessage()
        {
            return View();
        }
        //消息列表
        public ActionResult MessageList()
        {
            return View();
        }
        //用户资料
        public ActionResult UserInfo()
        {
            return View();
        }
        //修改密码
        public ActionResult UpdatePwd()
        {
            return View();
        }
    }
}
