using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using FinancePro.DataModels;
using Webdiyer.WebControls.Mvc;

namespace FinancePro.Areas.AdminArea.Models
{
    [Serializable]
    [DataContract]
    public class MemberCapitalIndexViewModel
    {
        [DataMember]
        public PagedList<MemberCapitalDetailModel> membercapitallist { get; set; }
        /// <summary>
        /// 查询条件
        /// </summary>
        [DataMember]
        public MemberCapitalDetailModel membercapital { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        [DataMember]
        public int totalcount { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        [DataMember]
        public int currentpage { get; set; }
        /// <summary>
        /// 页容量
        /// </summary>
        [DataMember]
        public int pagesize { get; set; }
    }
}