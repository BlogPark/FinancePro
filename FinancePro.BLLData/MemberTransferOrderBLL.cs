using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancePro.DALData;
using FinancePro.DataModels;

namespace FinancePro.BLLData
{
    /// <summary>
    /// 会员转账信息
    /// </summary>
    public class MemberTransferOrderBLL
    {
        /// <summary>
        /// 添加新的转账记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddNewMemberTransferOrder(MemberTransferOrderModel model)
        {
            return MemberTransferOrderDAL.AddNewMemberTransferOrder(model);
        }
        /// <summary>
        /// 按照分页读取会员转账记录
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public List<MemberTransferOrderModel> GetMemberTransferOrderByMemberID(int memberid,int pageindex,int pagesize,out int totalrowcount)
        {
            return MemberTransferOrderDAL.GetMemberTransferByLaunchMemberID(memberid,pageindex,pagesize,out totalrowcount);
        }
    }
}
