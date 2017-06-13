using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Income
    {
        public Income()
        { }
        #region Model
        private string _incomeid;
        private string _generalid;
        private string _itemoneid;
        private string _itemtwoid;
        private decimal? _incomeamount;
        /// <summary>
        /// 
        /// </summary>
        public string IncomeID
        {
            set { _incomeid = value; }
            get { return _incomeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GeneralID
        {
            set { _generalid = value; }
            get { return _generalid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ItemOneID
        {
            set { _itemoneid = value; }
            get { return _itemoneid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ItemTwoID
        {
            set { _itemtwoid = value; }
            get { return _itemtwoid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? IncomeAmount
        {
            set { _incomeamount = value; }
            get { return _incomeamount; }
        }
        #endregion Model
    }
}
