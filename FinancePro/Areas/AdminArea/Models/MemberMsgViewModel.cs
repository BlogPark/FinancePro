using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using FinancePro.DataModels;

namespace FinancePro.Areas.AdminArea.Models
{
    [Serializable]
    [DataContract]
    public class MemberMsgViewModel
    {
        [DataMember]
        public List<WebContactMessageModel> list { get; set; }
        [DataMember]
        public WebContactMessageModel updatemodel { get; set; }
    }
}