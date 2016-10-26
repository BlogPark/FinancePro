using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancePro.DataModels;

namespace FinancePro.BLLData
{
    /// <summary>
    /// 会员转账信息
    /// </summary>
    public class MemberTransferOrderBLL
    {
        /// <summary>
        /// 会员转账
        /// </summary>
        /// <param name="sourcemodel"></param>
        /// <returns></returns>
        public string MemberTransferOrder(MemberTransferOrderModel sourcemodel)
        {
            string result = "";
            if (string.IsNullOrWhiteSpace(sourcemodel.ReceiveMemberCode))
            {
                return "没有传入接受会员";
            }
            if (sourcemodel.TransferNumber < 1)
            {
                return "传入的转账金额不正确";
            }
            return result;
        }
    }
}
