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
    public class MessageListViewModel
    {
        /// <summary>
        /// 会员消息列表
        /// </summary>
        [DataMember]
        public List<WebContactMessageModel> WebContactList { get; set; }
    }
}