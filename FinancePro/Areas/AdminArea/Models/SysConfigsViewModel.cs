using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using FinancePro.DataModels;

namespace FinancePro.Areas.AdminArea.Models
{
    /// <summary>
    /// 系统配置项页面
    /// </summary>
    [Serializable]
    [DataContract]
    public class SysConfigsViewModel
    {
        /// <summary>
        /// 全部配置项
        /// </summary>
        [DataMember]
        public List<SystemConfigsModel> Allconfigs { get; set; }
        /// <summary>
        /// 配置项
        /// </summary>
        [DataMember]
        public SystemConfigsModel AConfig { get; set; }
        /// <summary>
        /// 修改配置项
        /// </summary>
        [DataMember]
        public SystemConfigsModel UConfig { get; set; }        
        /// <summary>
        /// 是否指定用户可见
        /// </summary>
        [DataMember]
        public int isadmin { get; set; }
    }
}