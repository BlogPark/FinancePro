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
    /// 系统日志读取
    /// </summary>
    public  class OperateLogBLL
    {
        /// <summary>
        /// 读取会员的资金变动明细日志
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public List<MemberCapitalLogModel> GetMemberCapitalLogByMemberid(int memberid, int pageindex, int pagesize, out int totalrowcount)
        {
            return MemberCapitalLogDAL.GetMemberCapitalLogByMemberID(memberid, pageindex, pagesize, out totalrowcount);
        }
        /// <summary>
        /// 读取会员的报单币的变动日志
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public List<MemberFormCurreyLogModel> GetMemberFormCurreyLogByMemberID(int memberid,int pageindex,int pagesize,out int totalrowcount)
        {
            return MemberFormCurreyLogDAL.GetMemberFormCurreyByMemberID(memberid,pageindex,pagesize,out totalrowcount);
        }
    }
}
