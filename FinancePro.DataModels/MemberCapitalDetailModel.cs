using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FinancePro.DataModels
{
    /// <summary>
    /// 会员资产明细
    /// </summary>
    [Serializable]
    [DataContract]
    public class MemberCapitalDetailModel
    {
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
        private int _gamecurrency;
        /// <summary>
        /// 游戏币
        /// </summary>		
        [DataMember]
        public int GameCurrency
        {
            get { return _gamecurrency; }
            set { _gamecurrency = value; }
        }
        private int _sharescurrency;
        /// <summary>
        /// 股权币
        /// </summary>		
        [DataMember]
        public int SharesCurrency
        {
            get { return _sharescurrency; }
            set { _sharescurrency = value; }
        }
        private int _shoppingcurrency;
        /// <summary>
        /// 购物币
        /// </summary>		
        [DataMember]
        public int ShoppingCurrency
        {
            get { return _shoppingcurrency; }
            set { _shoppingcurrency = value; }
        }
        private int _memberpoints;
        /// <summary>
        /// 会员积分
        /// </summary>		
        [DataMember]
        public int MemberPoints
        {
            get { return _memberpoints; }
            set { _memberpoints = value; }
        }
        private int _compoundcurrency;
        /// <summary>
        /// 复利币(报单币)
        /// </summary>		
        [DataMember]
        public int CompoundCurrency
        {
            get { return _compoundcurrency; }
            set { _compoundcurrency = value; }
        }

    }
}
