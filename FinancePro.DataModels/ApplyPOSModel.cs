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
    public class ApplyPOSModel
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
        private string _memberphone;
        /// <summary>
        /// 会员电话
        /// </summary>		
        [DataMember]
        public string MemberPhone
        {
            get { return _memberphone; }
            set { _memberphone = value; }
        }
        private string _memberidnumber;
        /// <summary>
        /// 身份证号
        /// </summary>		
        [DataMember]
        public string MemberIDNumber
        {
            get { return _memberidnumber; }
            set { _memberidnumber = value; }
        }
        private string _idnumberfrontpath;
        /// <summary>
        /// 身份证正面照
        /// </summary>		
        [DataMember]
        public string IDNumberFrontPath
        {
            get { return _idnumberfrontpath; }
            set { _idnumberfrontpath = value; }
        }
        private string _idnumberbackpath;
        /// <summary>
        /// 身份证反面照
        /// </summary>		
        [DataMember]
        public string IDNumberBackPath
        {
            get { return _idnumberbackpath; }
            set { _idnumberbackpath = value; }
        }
        private string _idwithhandpath;
        /// <summary>
        /// 手持身份证照
        /// </summary>		
        [DataMember]
        public string IDWithHandPath
        {
            get { return _idwithhandpath; }
            set { _idwithhandpath = value; }
        }
        private string _cardfrontpath;
        /// <summary>
        /// 银行卡正面照
        /// </summary>		
        [DataMember]
        public string CardFrontPath
        {
            get { return _cardfrontpath; }
            set { _cardfrontpath = value; }
        }
        private string _receiveaddress;
        /// <summary>
        /// 收货地址
        /// </summary>		
        [DataMember]
        public string ReceiveAddress
        {
            get { return _receiveaddress; }
            set { _receiveaddress = value; }
        }
        private int _pstatus;
        /// <summary>
        /// 申请状态（1 新申请 2 审核通过 3 发货途中 4 审核失败）
        /// </summary>		
        [DataMember]
        public int PStatus
        {
            get { return _pstatus; }
            set { _pstatus = value; }
        }
        private string _applyremark;
        /// <summary>
        /// 申请备注
        /// </summary>		
        [DataMember]
        public string ApplyRemark
        {
            get { return _applyremark; }
            set { _applyremark = value; }
        }
        private DateTime _addtime;
        /// <summary>
        /// 添加时间
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
