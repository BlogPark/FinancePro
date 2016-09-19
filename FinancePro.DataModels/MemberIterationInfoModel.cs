using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FinancePro.DataModels
{
    /// <summary>
    /// 会员迭代
    /// </summary>
    [Serializable]
    [DataContract]
    public class MemberIterationInfoModel
    {
        #region 扩展字段
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
        private int _superiormemberid;
        /// <summary>
        /// 上级会员ID
        /// </summary>		
        [DataMember]
        public int SuperiorMemberID
        {
            get { return _superiormemberid; }
            set { _superiormemberid = value; }
        }
        private string _superiormembername;
        /// <summary>
        /// 上级会员名字
        /// </summary>		
        [DataMember]
        public string SuperiorMemberName
        {
            get { return _superiormembername; }
            set { _superiormembername = value; }
        }
        private string _superiormembercode;
        /// <summary>
        /// 上级会员编号
        /// </summary>		
        [DataMember]
        public string SuperiorMemberCode
        {
            get { return _superiormembercode; }
            set { _superiormembercode = value; }
        }
        private int _generationnum;
        /// <summary>
        /// 世代数
        /// </summary>		
        [DataMember]
        public int GenerationNum
        {
            get { return _generationnum; }
            set { _generationnum = value; }
        }
        private int _rstatus;
        /// <summary>
        /// 状态信息(1 存在 2 废止)
        /// </summary>		
        [DataMember]
        public int RStatus
        {
            get { return _rstatus; }
            set { _rstatus = value; }
        }
        private DateTime _raddtime;
        /// <summary>
        /// 记录添加时间
        /// </summary>		
        [DataMember]
        public DateTime RAddTime
        {
            get { return _raddtime; }
            set { _raddtime = value; }
        }
        #endregion
        #region 扩展字段

        #endregion
    }
}
