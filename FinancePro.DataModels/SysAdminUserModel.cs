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
    public class SysAdminUserModel
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
        private string _username;
        /// <summary>
        /// UserName
        /// </summary>		
        [DataMember]
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }
        private string _userpwd;
        /// <summary>
        /// UserPwd
        /// </summary>		
        [DataMember]
        public string UserPwd
        {
            get { return _userpwd; }
            set { _userpwd = value; }
        }
        private int _userstatus;
        /// <summary>
        /// UserStatus
        /// </summary>		
        [DataMember]
        public int UserStatus
        {
            get { return _userstatus; }
            set { _userstatus = value; }
        }
        private string _useremail;
        /// <summary>
        /// UserEmail
        /// </summary>		
        [DataMember]
        public string UserEmail
        {
            get { return _useremail; }
            set { _useremail = value; }
        }
        private string _truethname;
        /// <summary>
        /// TruethName
        /// </summary>		
        [DataMember]
        public string TruethName
        {
            get { return _truethname; }
            set { _truethname = value; }
        }
        private string _userphone;
        /// <summary>
        /// UserPhone
        /// </summary>		
        [DataMember]
        public string UserPhone
        {
            get { return _userphone; }
            set { _userphone = value; }
        }
        private string _question;
        /// <summary>
        /// Question
        /// </summary>		
        [DataMember]
        public string Question
        {
            get { return _question; }
            set { _question = value; }
        }
        private string _answer;
        /// <summary>
        /// Answer
        /// </summary>		
        [DataMember]
        public string Answer
        {
            get { return _answer; }
            set { _answer = value; }
        }
        private int _gid;
        /// <summary>
        /// GID
        /// </summary>		
        [DataMember]
        public int GID
        {
            get { return _gid; }
            set { _gid = value; }
        }
        private string _gname;
        /// <summary>
        /// GName
        /// </summary>		
        [DataMember]
        public string GName
        {
            get { return _gname; }
            set { _gname = value; }
        }
        private string _loginname;
        /// <summary>
        /// LoginName
        /// </summary>		
        [DataMember]
        public string LoginName
        {
            get { return _loginname; }
            set { _loginname = value; }
        }
        private string _headerimg;
        /// <summary>
        /// HeaderImg
        /// </summary>		
        [DataMember]
        public string HeaderImg
        {
            get { return _headerimg; }
            set { _headerimg = value; }
        }
        private string _pinyin;
        /// <summary>
        /// PinYin
        /// </summary>		
        [DataMember]
        public string PinYin
        {
            get { return _pinyin; }
            set { _pinyin = value; }
        }
        private string _firstpinyin;
        /// <summary>
        /// FirstPinYin
        /// </summary>		
        [DataMember]
        public string FirstPinYin
        {
            get { return _firstpinyin; }
            set { _firstpinyin = value; }
        }
        private string _webskin;
        /// <summary>
        /// WebSkin
        /// </summary>		
        [DataMember]
        public string WebSkin
        {
            get { return _webskin; }
            set { _webskin = value; }
        }
        private string _lastloginip;
        /// <summary>
        /// LastLoginIP
        /// </summary>		
        [DataMember]
        public string LastLoginIP
        {
            get { return _lastloginip; }
            set { _lastloginip = value; }
        }
        private DateTime _lastlogintime;
        /// <summary>
        /// LastLoginTime
        /// </summary>		
        [DataMember]
        public DateTime LastLoginTime
        {
            get { return _lastlogintime; }
            set { _lastlogintime = value; }
        }
        private string _confirmpwd;
        /// <summary>
        /// ConfirmPwd
        /// </summary>		
        [DataMember]
        public string ConfirmPwd
        {
            get { return _confirmpwd; }
            set { _confirmpwd = value; }
        }
        private int _isadmin;
        /// <summary>
        /// IsAdmin
        /// </summary>		
        [DataMember]
        public int IsAdmin
        {
            get { return _isadmin; }
            set { _isadmin = value; }
        } 
        #endregion
        #region 扩展字段
        /// <summary>
        /// 登录结果字符串
        /// </summary>
        [DataMember]
        public string LoginResult { get; set; }
        /// <summary>
        /// 状态名称
        /// </summary>
        [DataMember]
        public string UserStatusName { get; set; }
        #endregion
    }
}
