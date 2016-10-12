using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FinancePro.DataModels
{
    /// <summary>
    /// 会员编号
    /// </summary>
    [Serializable]
    [DataContract]
    public class MemberCodeModel
    {
        #region 原始字段
        private int _id;
        /// <summary>
        /// 自增标识
        /// </summary>		
        [DataMember]
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        private int _membercode;
        /// <summary>
        /// 会员编号
        /// </summary>		
        [DataMember]
        public int MemberCode
        {
            get { return _membercode; }
            set { _membercode = value; }
        }
        private int _cstatus;
        /// <summary>
        /// 状态信息（1 未使用 2 已使用）
        /// </summary>		
        [DataMember]
        public int CStatus
        {
            get { return _cstatus; }
            set { _cstatus = value; }
        }
        #endregion

    }
}
