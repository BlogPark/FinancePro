using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FinancePro.DataModels
{
    [Serializable]
    [DataContract]
    public class WebContactMessageModel
    {

        #region 原表字段
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
        /// 会员名称
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
        private string _messagetitle;
        /// <summary>
        /// 留言标题
        /// </summary>		
        [DataMember]
        public string MessageTitle
        {
            get { return _messagetitle; }
            set { _messagetitle = value; }
        }
        private string _messagecontent;
        /// <summary>
        /// 留言内容
        /// </summary>		
        [DataMember]
        public string MessageContent
        {
            get { return _messagecontent; }
            set { _messagecontent = value; }
        }
        private DateTime _addtime;
        /// <summary>
        /// 提交时间
        /// </summary>		
        [DataMember]
        public DateTime AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        private int _mstatus;
        /// <summary>
        /// 留言状态（1 提问 2 已回复 3 删除）
        /// </summary>		
        [DataMember]
        public int MStatus
        {
            get { return _mstatus; }
            set { _mstatus = value; }
        }
        private string _replycontent;
        /// <summary>
        /// 回复内容
        /// </summary>		
        [DataMember]
        public string ReplyContent
        {
            get { return _replycontent; }
            set { _replycontent = value; }
        }
        private DateTime _replytime;
        /// <summary>
        /// 回复时间
        /// </summary>		
        [DataMember]
        public DateTime ReplyTime
        {
            get { return _replytime; }
            set { _replytime = value; }
        }
        private int _isreaded;
        /// <summary>
        /// 是否已读
        /// </summary>		
        [DataMember]
        public int IsReaded
        {
            get { return _isreaded; }
            set { _isreaded = value; }
        } 
        #endregion
        #region 扩展字段
        /// <summary>
        /// 状态名称
        /// </summary>
        public string MStatusName { get; set; }
        #endregion
    }
}
