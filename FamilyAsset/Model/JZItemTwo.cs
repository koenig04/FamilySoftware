using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class JZItemTwo
    {
        public JZItemTwo()
        { }
        #region Model
        private int _id;
        private string _jzitemtwoid;
        private string _jzitemoneid;
        private string _jzitemtwoname;
        private string _iconname;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JZItemTwoID
        {
            set { _jzitemtwoid = value; }
            get { return _jzitemtwoid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JZItemOneID
        {
            set { _jzitemoneid = value; }
            get { return _jzitemoneid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JZItemTwoName
        {
            set { _jzitemtwoname = value; }
            get { return _jzitemtwoname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IconName
        {
            set { _iconname = value; }
            get { return _iconname; }
        }
        #endregion Model
    }
}
