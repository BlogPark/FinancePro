using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FinancePro.DataModels
{
    /// <summary>
    /// 会员扩展信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class MemberExtendInfoModel
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
        private int _memberid;
        /// <summary>
        /// 会员ID
        /// </summary>		
        [DataMember]
        public int MemberID
        {
            get { return _memberid; }
            set { _memberid = value; }
        }
        private string _membername;
        /// <summary>
        /// 会员名字
        /// </summary>		
        [DataMember]
        public string MemberName
        {
            get { return _membername; }
            set { _membername = value; }
        }
        private string _membercode;
        /// <summary>
        /// 会员编号
        /// </summary>		
        [DataMember]
        public string MemberCode
        {
            get { return _membercode; }
            set { _membercode = value; }
        }
        private int _formcurreynum;
        /// <summary>
        /// 报单币数量
        /// </summary>		
        [DataMember]
        public int FormCurreyNum
        {
            get { return _formcurreynum; }
            set { _formcurreynum = value; }
        } 
        #endregion
        #region 扩展字段
        /// <summary>
        /// 页索引
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页容量
        /// </summary>
        public int PageSize { get; set; }
        #endregion
    }
}
