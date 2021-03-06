﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using FinancePro.DataModels;

namespace FinancePro.Areas.AdminArea.Models
{
    [Serializable]
    [DataContract]
    public class DefaultViewModel
    {
        /// <summary>
        /// 后台首页数据
        /// </summary>
        [DataMember]
        public AdminHomeDataModel admindata { get; set; }
    }
}