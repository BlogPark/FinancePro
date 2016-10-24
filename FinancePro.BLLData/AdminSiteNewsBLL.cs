using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancePro.DALData;
using FinancePro.DataModels;

namespace FinancePro.BLLData
{
    public class AdminSiteNewsBLL
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddAdminSiteNew(AdminSiteNewsModel model)
        {
            return AdminSiteNewsDAL.AddAdminSiteNew(model);
        }
        /// <summary>
        /// 设置紧急条目
        /// </summary>
        public bool SetUrgent(int id, int isurgent)
        {
            return AdminSiteNewsDAL.SetUrgent(id, isurgent);
        }
        /// <summary>
        /// 设置置顶条目
        /// </summary>
        public bool SetIsTop(int id, int istop)
        {
            return AdminSiteNewsDAL.SetIsTop(id, istop);
        }
        /// <summary>
        /// 更新条目状态
        /// </summary>
        public bool UpdateStatus(int id, int status)
        {
            return AdminSiteNewsDAL.UpdateStatus(id, status);
        }
        /// <summary>
        /// 根据ID得到一个对象实体
        /// </summary>
        public AdminSiteNewsModel GetModel(int ID)
        {
            return AdminSiteNewsDAL.GetModel(ID);
        }
        /// <summary>
        ///根据用户ID得到对应的系统公告
        /// </summary>
        public List<AdminSiteNewsModel> GetModelListByUserID(int userid, int topnum = 100)
        {
            return AdminSiteNewsDAL.GetModelListByUserID(userid, topnum);
        }
        /// <summary>
        /// 根据用户ID查询前3条系统公告
        /// </summary>
        public List<AdminSiteNewsModel> GetTop3ModelListByUserID(int userid)
        {
            return AdminSiteNewsDAL.GetTop3ModelListByUserID(userid);
        }
        /// <summary>
        /// 查询会员的留言信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<WebContactMessageModel> GetContractMessage(int userid)
        {
            return AdminSiteNewsDAL.GetContractMessage(userid);
        }
        /// <summary>
        /// 查询所有的会员留言信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<WebContactMessageModel> GetAllContractMessage()
        {
            return AdminSiteNewsDAL.GetAllContractMessage();
        }
        /// <summary>
        /// 添加留言信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddContactMessage(WebContactMessageModel model)
        {
            return AdminSiteNewsDAL.AddContactMessage(model);
        }
        /// <summary>
        /// 回复会员留言
        /// </summary>
        public bool UpdateMsg(WebContactMessageModel model)
        {
            return AdminSiteNewsDAL.UpdateMsg(model);
        }
        /// <summary>
        /// 删除会员留言
        /// </summary>
        public bool deleteMsg(int id)
        {
            return AdminSiteNewsDAL.deleteMsg(id);
        }
        /// <summary>
        /// 查询系统公告数目
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public int GetSysNewsCount(int memberid)
        {
            return AdminSiteNewsDAL.GetSysNewsCount(memberid);
        }
        /// <summary>
        /// 查询我的留言数目
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public int GetNewWebContentCount(int memberid)
        {
            return AdminSiteNewsDAL.GetNewWebContentCount(memberid);
        }
    }
}
