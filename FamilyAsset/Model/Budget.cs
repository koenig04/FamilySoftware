using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Budget
    {
        public Budget()
        { }
        #region Model
        private string _budgetid;
        private string _generalid;
        private string _itemoneid;
        private string _itemtwoid;
        private decimal? _budgetamount;
        /// <summary>
        /// 
        /// </summary>
        public string BudgetID
        {
            set { _budgetid = value; }
            get { return _budgetid; }
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
        public decimal? BudgetAmount
        {
            set { _budgetamount = value; }
            get { return _budgetamount; }
        }
        #endregion Model
    }
}
