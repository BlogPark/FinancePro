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
    public class DynamicRewardModel
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
        private decimal _gamecurrency;
        /// <summary>
        /// 游戏币
        /// </summary>		
        [DataMember]
        public decimal GameCurrency
        {
            get { return _gamecurrency; }
            set { _gamecurrency = value; }
        }
        private decimal _sharescurrency;
        /// <summary>
        /// 股权币
        /// </summary>		
        [DataMember]
        public decimal SharesCurrency
        {
            get { return _sharescurrency; }
            set { _sharescurrency = value; }
        }
        private decimal _shoppingcurrency;
        /// <summary>
        /// 购物币
        /// </summary>		
        [DataMember]
        public decimal ShoppingCurrency
        {
            get { return _shoppingcurrency; }
            set { _shoppingcurrency = value; }
        }
        private decimal _memberpoints;
        /// <summary>
        /// 积分
        /// </summary>		
        [DataMember]
        public decimal MemberPoints
        {
            get { return _memberpoints; }
            set { _memberpoints = value; }
        }
        private decimal _compoundcurrency;
        /// <summary>
        /// 复利币
        /// </summary>		
        [DataMember]
        public decimal CompoundCurrency
        {
            get { return _compoundcurrency; }
            set { _compoundcurrency = value; }
        }
        private int _sourcememberid;
        /// <summary>
        /// 来源会员ID
        /// </summary>		
        [DataMember]
        public int SourceMemberID
        {
            get { return _sourcememberid; }
            set { _sourcememberid = value; }
        }
        private string _sourcemembername;
        /// <summary>
        /// 来源会员名字
        /// </summary>		
        [DataMember]
        public string SourceMemberName
        {
            get { return _sourcemembername; }
            set { _sourcemembername = value; }
        }
        private int _lstatus;
        /// <summary>
        /// 状态(1 未分配 2 已分配)
        /// </summary>		
        [DataMember]
        public int LStatus
        {
            get { return _lstatus; }
            set { _lstatus = value; }
        } 
        #endregion

    }
}
