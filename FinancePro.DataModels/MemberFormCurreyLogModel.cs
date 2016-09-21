using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FinancePro.DataModels
{
    /// <summary>
    /// 会员报单币变动记录
    /// </summary>
    [Serializable]
    [DataContract]
    public class MemberFormCurreyLogModel
    {

        #region 原始字段
        private int _id;
        /// <summary>
        /// ID
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
        private int _bformcurreynum;
        /// <summary>
        /// 变动前报单币个数
        /// </summary>		
        [DataMember]
        public int BFormCurreyNum
        {
            get { return _bformcurreynum; }
            set { _bformcurreynum = value; }
        }
        private int _nformcurreynum;
        /// <summary>
        /// 最新报单币个数
        /// </summary>		
        [DataMember]
        public int NFormCurreyNum
        {
            get { return _nformcurreynum; }
            set { _nformcurreynum = value; }
        }
        private string _remark;
        /// <summary>
        /// 变动备注
        /// </summary>		
        [DataMember]
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        private DateTime _addtime;
        /// <summary>
        /// 变动时间
        /// </summary>		
        [DataMember]
        public DateTime AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        } 
        #endregion

    }
}
