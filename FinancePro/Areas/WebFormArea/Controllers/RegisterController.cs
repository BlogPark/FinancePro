using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancePro.Areas.WebFormArea.Models;
using FinancePro.BLLData;
using FinancePro.DataModels;

namespace FinancePro.Areas.WebFormArea.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /WebFormArea/Register/

        private MemberInfoBLL memberbll = new MemberInfoBLL();
        public ActionResult Index()
        {
            RegisterViewModel model = new RegisterViewModel();
            MemberCodeModel codemodel=MemberCodeBLL.GetMemberCode();
            model.member = new MemberInfoModel();
            model.member.MemberCode = "JL"+codemodel.MemberCode;
            model.member.MemberCodeID = codemodel.ID;
            model.member.MemberType = 1;
            model.member.IsDerivativeMember = 0;
            model.regintable = ReginTableBLL.GetReginTableListModel(1);
            ViewData["Error"] = "";
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(MemberInfoModel member)
        {
            string result = memberbll.AddNewMemberInfo(member, member.MemberType);
            if (result == "1")
            {
                return RedirectToAction("Index", "Login", new { area = "WebFormArea" });
            }
            else
            {
                ViewData["Error"] = result;
            }
            RegisterViewModel model = new RegisterViewModel();
            model.regintable = ReginTableBLL.GetReginTableListModel(1);
            model.member = member;
            return View(model);
        }
    }
}
