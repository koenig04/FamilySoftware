using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Expenditure
    {
        public Expenditure()
        { }
        #region Model
        private string _expenditureid;
        private string _generalid;
        private string _itemoneid;
        private string _itemtwoid;
        private decimal? _expenditureamount;
        /// <summary>
        /// 
        /// </summary>
        public string ExpenditureID
        {
            set { _expenditureid = value; }
            get { return _expenditureid; }
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
        public decimal? ExpenditureAmount
        {
            set { _expenditureamount = value; }
            get { return _expenditureamount; }
        }
        #endregion Model
    }
}
