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
    public class AdminHomeDataModel
    {
        /// <summary>
        /// 系统总会员数
        /// </summary>
        [DataMember]
        public int TotalMemberCount { get; set; }
        /// <summary>
        /// 系统活动会员数
        /// </summary>
        [DataMember]
        public int ActiveMemberCount { get; set; }
        /// <summary>
        /// 系统的总报单币数量
        /// </summary>
        [DataMember]
        public int FormCurreyCount { get; set; }       
    }
}
