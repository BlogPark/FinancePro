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
    public class AdminSiteNewsModel
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
        private string _stitle;
        /// <summary>
        /// 消息标题
        /// </summary>		
        [DataMember]
        public string STitle
        {
            get { return _stitle; }
            set { _stitle = value; }
        }
        private string _scontent;
        /// <summary>
        /// 消息内容
        /// </summary>		
        [DataMember]
        public string SContent
        {
            get { return _scontent; }
            set { _scontent = value; }
        }
        private int _senduserid;
        /// <summary>
        /// 发送人ID
        /// </summary>		
        [DataMember]
        public int SendUserID
        {
            get { return _senduserid; }
            set { _senduserid = value; }
        }
        private string _sendusername;
        /// <summary>
        /// 发送人名称
        /// </summary>		
        [DataMember]
        public string SendUserName
        {
            get { return _sendusername; }
            set { _sendusername = value; }
        }
        private int _receiveuserid;
        /// <summary>
        /// 接收人ID
        /// </summary>		
        [DataMember]
        public int ReceiveUserID
        {
            get { return _receiveuserid; }
            set { _receiveuserid = value; }
        }
        private string _receiveusername;
        /// <summary>
        /// 接收人名称
        /// </summary>		
        [DataMember]
        public string ReceiveUserName
        {
            get { return _receiveusername; }
            set { _receiveusername = value; }
        }
        private int _sstatus;
        /// <summary>
        /// 状态值（1 发布 2 已阅 3 删除）
        /// </summary>		
        [DataMember]
        public int SStatus
        {
            get { return _sstatus; }
            set { _sstatus = value; }
        }
        private DateTime _saddtime;
        /// <summary>
        /// 添加时间
        /// </summary>		
        [DataMember]
        public DateTime SAddTime
        {
            get { return _saddtime; }
            set { _saddtime = value; }
        }
        private int _isurgent;
        /// <summary>
        /// 是否紧急
        /// </summary>		
        [DataMember]
        public int IsUrgent
        {
            get { return _isurgent; }
            set { _isurgent = value; }
        }
        private int _istop;
        /// <summary>
        /// 是否置顶
        /// </summary>		
        [DataMember]
        public int IsTop
        {
            get { return _istop; }
            set { _istop = value; }
        } 
        #endregion

        #region 扩展字段
        /// <summary>
        /// 状态值（1 发布 2 已阅 3 删除）
        /// </summary>       
        [DataMember]
        public string SStatusName { get; set; }
        #endregion
    }
}
