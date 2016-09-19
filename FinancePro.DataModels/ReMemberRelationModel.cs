using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FinancePro.DataModels
{
    /// <summary>
    /// 推荐关系
    /// </summary>
    [Serializable]
    [DataContract]
    public class ReMemberRelationModel
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
        private int _recommendmemberid;
        /// <summary>
        /// 推荐人ID
        /// </summary>		
        [DataMember]
        public int RecommendMemberID
        {
            get { return _recommendmemberid; }
            set { _recommendmemberid = value; }
        }
        private string _recommendmembername;
        /// <summary>
        /// 推荐人名字
        /// </summary>		
        [DataMember]
        public string RecommendMemberName
        {
            get { return _recommendmembername; }
            set { _recommendmembername = value; }
        }
        private string _recommendmembercode;
        /// <summary>
        /// 推荐人编号
        /// </summary>		
        [DataMember]
        public string RecommendMemberCode
        {
            get { return _recommendmembercode; }
            set { _recommendmembercode = value; }
        }
        private int _berecommmemberid;
        /// <summary>
        /// 被推荐人ID
        /// </summary>		
        [DataMember]
        public int BeRecommMemberID
        {
            get { return _berecommmemberid; }
            set { _berecommmemberid = value; }
        }
        private string _berecommmembername;
        /// <summary>
        /// 被推荐人名字
        /// </summary>		
        [DataMember]
        public string BeRecommMemberName
        {
            get { return _berecommmembername; }
            set { _berecommmembername = value; }
        }
        private string _berecommmembercode;
        /// <summary>
        /// 被推荐人编号
        /// </summary>		
        [DataMember]
        public string BeRecommMemberCode
        {
            get { return _berecommmembercode; }
            set { _berecommmembercode = value; }
        }
        private DateTime _recommendtime;
        /// <summary>
        /// 推荐时间
        /// </summary>		
        [DataMember]
        public DateTime RecommendTime
        {
            get { return _recommendtime; }
            set { _recommendtime = value; }
        }
        private int _recommendstatus;
        /// <summary>
        /// 推荐关系状态(1 存在 2 废止)
        /// </summary>		
        [DataMember]
        public int RecommendStatus
        {
            get { return _recommendstatus; }
            set { _recommendstatus = value; }
        }
        #endregion
        #region 扩展字段

        #endregion
    }
}
