using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancePro.BLLData;
using FinancePro.Common;
using FinancePro.DataModels;

namespace FinancePro.Controllers
{
    public class publicController : Controller
    {
        //
        // GET: /public/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult obtainreagin(int id)
        {
            List<ReginTableModel> list = ReginTableBLL.GetReginTableListModel(id);
            return Json(list);
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadFiles()
        {
            int filecount = Request.Files.Count;
            if (filecount > 0)
            {
                var file = Request.Files[0];
                //上传配置
                int size = 2048;           //文件大小限制,单位MB                  //文件大小限制，单位MB 
                string[] filetype = AppContent.ImgType.Split(',');         //文件允许格式
                string path = AppContent.UploadPath.TrimEnd('\\') + "\\";     //文件上传路径             
                UploadFileModel uploadImage = UploadHelper.LocalUpLoadForSingle(file, path, filetype, size, "");
                if (uploadImage.status == "success")
                {
                    return Json(new { status = true, filename = uploadImage.filename, imgwidth = uploadImage.width, imgheight = uploadImage.height, urlpath = AppContent.Imgdomain + uploadImage.filepath, path = uploadImage.filepath });
                }
                else
                {
                    return Json(new { status = false, meg = uploadImage.message });
                }
            }
            else
            {
                return Json(new { status = false, meg = "未获得文件" });
            }
        }
    }
}
