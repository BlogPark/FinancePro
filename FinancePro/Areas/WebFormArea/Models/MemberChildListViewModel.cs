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
    public class MemberChildListViewModel
    {
        [DataMember]
        public List<MemberInfoModel> memberlist { get; set; }
    }
}