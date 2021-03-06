﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using FinancePro.DataModels;
using Webdiyer.WebControls.Mvc;

namespace FinancePro.Areas.WebFormArea.Models
{
    [Serializable]
    [DataContract]
    public class FormCurryDetailLogViewModel
    {
        [DataMember]
        public PagedList<MemberFormCurreyLogModel> loglist { get; set; }
    }
}