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
    public class MemberListViewModel
    {
        /// <summary>
        /// 会员列表
        /// </summary>
        [DataMember]
        public List<MemberInfoModel> MemberList { get; set; }
    }
}