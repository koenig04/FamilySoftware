using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class JZItemOne
    {
        public JZItemOne()
        { }
        #region Model
        private int _id;
        private string _jzitemoneid;
        private string _jzitemonename;
        private bool _incomeorcost;
        private string _iconname;

        public string IconNamePressed { get; set; }
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
        public string JZItemOneID
        {
            set { _jzitemoneid = value; }
            get { return _jzitemoneid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JZItemOneName
        {
            set { _jzitemonename = value; }
            get { return _jzitemonename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IncomeOrCost
        {
            set { _incomeorcost = value; }
            get { return _incomeorcost; }
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
