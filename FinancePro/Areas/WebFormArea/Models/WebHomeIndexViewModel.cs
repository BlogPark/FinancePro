using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using FinancePro.DataModels;

namespace FinancePro.Areas.WebFormArea.Models
{
    /// <summary>
    /// 首页数据
    /// </summary>
    [Serializable]
    [DataContract]
    public class WebHomeIndexViewModel
    {
        /// <summary>
        /// 会员信息
        /// </summary>
        [DataMember]
        public MemberInfoModel MemberInfo { get; set; }

        [DataMember]
        public MemberCapitalDetailModel CapitalDetail { get; set; }
        /// <summary>
        /// 系统公告
        /// </summary>
        [DataMember]
        public List<AdminSiteNewsModel> AdminSiteNews { get; set; }
        /// <summary>
        /// 报单币数量
        /// </summary>
        [DataMember]
        public decimal FormCurreyCount { get; set; }
        /// <summary>
        /// 会员直推人数
        /// </summary>
        [DataMember]
        public int recommendnumber { get; set; }
    }
}