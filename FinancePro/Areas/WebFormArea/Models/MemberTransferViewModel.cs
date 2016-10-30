using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using FinancePro.DataModels;

namespace FinancePro.Areas.WebFormArea.Models
{
    /// <summary>
    /// 会员转账Model
    /// </summary>
    [Serializable]
    [DataContract]
    public class MemberTransferViewModel
    {
        /// <summary>
        /// 转账记录
        /// </summary>
        [DataMember]
        public MemberTransferOrderModel transfer { get; set; }
        /// <summary>
        /// 收费类型
        /// </summary>
        [DataMember]
        public int feetype { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        [DataMember]
        public string ErrorText { get; set; }
    }
}