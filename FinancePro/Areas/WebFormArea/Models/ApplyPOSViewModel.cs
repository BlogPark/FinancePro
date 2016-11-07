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
    public class ApplyPOSViewModel
    {
        [DataMember]
        public ApplyPOSModel applymodel { get; set; }


        [DataMember]
        public string ErrorStr { get; set; }
    }
}