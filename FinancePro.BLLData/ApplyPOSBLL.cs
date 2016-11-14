using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancePro.DALData;
using FinancePro.DataModels;

namespace FinancePro.BLLData
{
    public class ApplyPOSBLL
    {
        /// <summary>
        /// 添加申请记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddNewApplyPOS(ApplyPOSModel model)
        {
            string result = "";
            result = ApplyPOSDAL.AddNewApplyPOS(model).ToString();
            return result;
        }
        /// <summary>
        /// 分页查询申请POS的列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public List<ApplyPOSModel> GetApplyPOSListByPage(ApplyPOSModel applymodel, out int totalrowcount)
        {
            return ApplyPOSDAL.GetApplyPOSListByPage(applymodel, out totalrowcount);
        }
        /// <summary>
        /// 修改申请单状态
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public int UpdatePStatusByID(int aid, int statusnum)
        {
            return ApplyPOSDAL.UpdatePStatusByID(aid, statusnum);
        }
        /// <summary>
        /// 批量修改单据状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="statusnum"></param>
        /// <returns></returns>
        public int UpdatePStatusByIDs(string ids, int statusnum)
        {
            return ApplyPOSDAL.UpdatePStatusByIDs(ids, statusnum);
        }
        /// <summary>
        /// 查询单据的图片信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  List<string> GetApplyPOSImageByID(int id)
        {
            return ApplyPOSDAL.GetApplyPOSImageByID(id);
        }
         /// <summary>
        /// 根据会员查询申请单信息
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public List<ApplyPOSModel> GetApplyPosOrderByMemberid(int memberid)
        {
            return ApplyPOSDAL.GetApplyPosOrderByMemberid(memberid);
        }
    }
}
