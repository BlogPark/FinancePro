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
    public class ApplyPOSListViewModel
    {
        /// <summary>
        /// 申请列表
        /// </summary>
        [DataMember]
        public List<ApplyPOSModel> ApplyList { get; set; }
    }
}