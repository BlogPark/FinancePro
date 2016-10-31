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
    public class AddMemberViewModel
    {
        /// <summary>
        /// 会员信息
        /// </summary>
        [DataMember]
        public MemberInfoModel member { get; set; }
    }
}