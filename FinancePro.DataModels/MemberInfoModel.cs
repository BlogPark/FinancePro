﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FinancePro.DataModels
{
    /// <summary>
    /// 会员信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class MemberInfoModel
    {
        #region 原始字段
        private int _id;
        /// <summary>
        /// 自增标识符
        /// </summary>		
        [DataMember]
        public int ID
        {
            get { return _id; }
            set { _id = value; }
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
        private int _membersex;
        /// <summary>
        /// 会员性别(1 男 2 女)
        /// </summary>		
        [DataMember]
        public int MemberSex
        {
            get { return _membersex; }
            set { _membersex = value; }
        }
        private string _memberphone;
        /// <summary>
        /// 会员手机
        /// </summary>		
        [DataMember]
        public string MemberPhone
        {
            get { return _memberphone; }
            set { _memberphone = value; }
        }
        private string _memberemail;
        /// <summary>
        /// 会员邮箱
        /// </summary>		
        [DataMember]
        public string MemberEmail
        {
            get { return _memberemail; }
            set { _memberemail = value; }
        }
        private string _memberprovince;
        /// <summary>
        /// 省份
        /// </summary>		
        [DataMember]
        public string MemberProvince
        {
            get { return _memberprovince; }
            set { _memberprovince = value; }
        }
        private string _membercity;
        /// <summary>
        /// 城市
        /// </summary>		
        [DataMember]
        public string MemberCity
        {
            get { return _membercity; }
            set { _membercity = value; }
        }
        private string _memberarea;
        /// <summary>
        /// 区域
        /// </summary>		
        [DataMember]
        public string MemberArea
        {
            get { return _memberarea; }
            set { _memberarea = value; }
        }
        private string _memberaddress;
        /// <summary>
        /// 详细地址
        /// </summary>		
        [DataMember]
        public string MemberAddress
        {
            get { return _memberaddress; }
            set { _memberaddress = value; }
        }
        private string _memberbankname;
        /// <summary>
        /// 开户行名称
        /// </summary>		
        [DataMember]
        public string MemberBankName
        {
            get { return _memberbankname; }
            set { _memberbankname = value; }
        }
        private string _memberbankcode;
        /// <summary>
        /// 银行卡编号
        /// </summary>		
        [DataMember]
        public string MemberBankCode
        {
            get { return _memberbankcode; }
            set { _memberbankcode = value; }
        }
        private string _memberlogpwd;
        /// <summary>
        /// 登陆密码
        /// </summary>		
        [DataMember]
        public string MemberLogPwd
        {
            get { return _memberlogpwd; }
            set { _memberlogpwd = value; }
        }
        private int _memberstatus;
        /// <summary>
        /// 会员状态(1 待激活 2 已激活 3 已冻结)
        /// </summary>		
        [DataMember]
        public int MemberStatus
        {
            get { return _memberstatus; }
            set { _memberstatus = value; }
        }
        private int _membertype;
        /// <summary>
        /// 会员类型(1 常规会员  2 衍生会员 3衍生会员  4超级会员)
        /// </summary>		
        [DataMember]
        public int MemberType
        {
            get { return _membertype; }
            set { _membertype = value; }
        }
        private int _isfinalmember;
        /// <summary>
        /// 是否终极会员
        /// </summary>		
        [DataMember]
        public int IsFinalMember
        {
            get { return _isfinalmember; }
            set { _isfinalmember = value; }
        }
        private int _isderivativemember;
        /// <summary>
        /// 是否衍生账户
        /// </summary>		
        [DataMember]
        public int IsDerivativeMember
        {
            get { return _isderivativemember; }
            set { _isderivativemember = value; }
        }
        private int _isspecialmember;
        /// <summary>
        /// 是否特殊账户
        /// </summary>		
        [DataMember]
        public int IsSpecialMember
        {
            get { return _isspecialmember; }
            set { _isspecialmember = value; }
        }
        private int _isreportmember;
        /// <summary>
        /// 是否报单中心
        /// </summary>		
        [DataMember]
        public int IsReportMember
        {
            get { return _isreportmember; }
            set { _isreportmember = value; }
        }
        private DateTime _addtime;
        /// <summary>
        /// 注册时间
        /// </summary>		
        [DataMember]
        public DateTime AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        } 
        #endregion
        #region 扩展字段

        #endregion

    }
}
