using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FinancePro.DataModels
{
    [Serializable]
    [DataContract]
    public class WebHomeDataModel
    {
        /// <summary>
        /// 会员信息
        /// </summary>
        [DataMember]
        public MemberInfoModel Memberinfo { get; set; }
        /// <summary>
        /// 会员资产信息
        /// </summary>
        [DataMember]
        public MemberCapitalDetailModel MemberCapital { get; set; }
        /// <summary>
        /// 会员扩展信息
        /// </summary>
        [DataMember]
        public MemberExtendInfoModel MemberExtendInfo { get; set; }
        /// <summary>
        /// 会员推荐关系列表
        /// </summary>
        [DataMember]
        public List<ReMemberRelationModel> MemberRelation { get; set; }
        /// <summary>
        /// 会员资金变动记录
        /// </summary>
        [DataMember]
        public List<MemberCapitalLogModel> MemberCapitalLog { get; set; }
        /// <summary>
        /// 会员报单币变动记录
        /// </summary>
        [DataMember]
        public List<MemberFormCurreyLogModel> MemberFormCurreyLog { get; set; }
        /// <summary>
        /// 系统公告列表
        /// </summary>
        [DataMember]
        public List<AdminSiteNewsModel> AdminSiteNews { get; set; }
        /// <summary>
        /// 系统消息列表
        /// </summary>
        [DataMember]
        public List<WebContactMessageModel> ContactMessages { get; set; }
        /// <summary>
        /// 团队总人数
        /// </summary>
        [DataMember]
        public int TeamTotalCount { get; set; }
        /// <summary>
        /// 直推人数
        /// </summary>
        [DataMember]
        public int RecommendCount { get; set; }
    }
}
