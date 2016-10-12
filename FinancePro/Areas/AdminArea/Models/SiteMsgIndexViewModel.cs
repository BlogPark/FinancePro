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
    public class SiteMsgIndexViewModel
    {
        [DataMember]
        public List<AdminSiteNewsModel> modellist { get; set; }
        [DataMember]
        public AdminSiteNewsModel addmodel { get; set; }
    }
}