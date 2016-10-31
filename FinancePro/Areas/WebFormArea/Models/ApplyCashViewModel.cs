using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using FinancePro.DataModels;

namespace FinancePro.Areas.WebFormArea.Models
{
    [Serializable]
    [DataContract]
    public class ApplyCashViewModel
    {
        /// <summary>
        /// 提现信息
        /// </summary>
        [DataMember]
        public MemberCashOrderModel cashorder { get; set; }
        /// <summary>
        /// 会员目前积分
        /// </summary>
        [DataMember]
        public decimal memberpoint { get; set; }
        /// <summary>
        /// 手续费比例
        /// </summary>
        [DataMember]
        public int feenum { get; set; }
        /// <summary>
        /// 最低提现费率
        /// </summary>
        [DataMember]
        public decimal mincashnum { get; set; }
        /// <summary>
        /// 最大提现费率
        /// </summary>
        [DataMember]
        public int maxnum { get; set; }
        /// <summary>
        /// 基数费率
        /// </summary>
        [DataMember]
        public int basenum { get; set; }
    }
}