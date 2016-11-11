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
    public class MemberMapViewModel
    {
        [DataMember]
        public MemberInfoModel member { get; set; }
        [DataMember]
        public int childcount { get; set; }
        [DataMember]
        public bool isParent { get; set; }
    }
}