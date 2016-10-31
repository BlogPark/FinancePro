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
    public class WriteMessageViewModel
    {
        [DataMember]
        public WebContactMessageModel message { get; set; }
    }
}