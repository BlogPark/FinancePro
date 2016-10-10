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
    public class ReginTableModel
    {
        private int _region_id;
        /// <summary>
        /// REGION_ID
        /// </summary>		
        [DataMember]
        public int REGION_ID
        {
            get { return _region_id; }
            set { _region_id = value; }
        }
        private string _region_code;
        /// <summary>
        /// REGION_CODE
        /// </summary>		
        [DataMember]
        public string REGION_CODE
        {
            get { return _region_code; }
            set { _region_code = value; }
        }
        private string _region_name;
        /// <summary>
        /// REGION_NAME
        /// </summary>		
        [DataMember]
        public string REGION_NAME
        {
            get { return _region_name; }
            set { _region_name = value; }
        }
        private int _parent_id;
        /// <summary>
        /// PARENT_ID
        /// </summary>		
        [DataMember]
        public int PARENT_ID
        {
            get { return _parent_id; }
            set { _parent_id = value; }
        }
        private int _region_level;
        /// <summary>
        /// REGION_LEVEL
        /// </summary>		
        [DataMember]
        public int REGION_LEVEL
        {
            get { return _region_level; }
            set { _region_level = value; }
        }
        private int _region_order;
        /// <summary>
        /// REGION_ORDER
        /// </summary>		
        [DataMember]
        public int REGION_ORDER
        {
            get { return _region_order; }
            set { _region_order = value; }
        }
        private string _region_name_en;
        /// <summary>
        /// REGION_NAME_EN
        /// </summary>		
        [DataMember]
        public string REGION_NAME_EN
        {
            get { return _region_name_en; }
            set { _region_name_en = value; }
        }
        private string _region_shortname_en;
        /// <summary>
        /// REGION_SHORTNAME_EN
        /// </summary>		
        [DataMember]
        public string REGION_SHORTNAME_EN
        {
            get { return _region_shortname_en; }
            set { _region_shortname_en = value; }
        }

    }
}
