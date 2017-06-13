using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AccountInfo
    {
        public AccountInfo()
        { }
        #region Model
        private string _accountid;
        private DateTime _accountdate;
        private string _itemoneid;
        private string _itemtwoid;
        private decimal _accountamount;
        private string _notice;
        private string _reserve1;
        private string _reserve2;
        private string _reserve3;
        /// <summary>
        /// 
        /// </summary>
        public string AccountID
        {
            set { _accountid = value; }
            get { return _accountid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime AccountDate
        {
            set { _accountdate = value; }
            get { return _accountdate; }
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
        public decimal AccountAmount
        {
            set { _accountamount = value; }
            get { return _accountamount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Notice
        {
            set { _notice = value; }
            get { return _notice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Reserve1
        {
            set { _reserve1 = value; }
            get { return _reserve1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Reserve2
        {
            set { _reserve2 = value; }
            get { return _reserve2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Reserve3
        {
            set { _reserve3 = value; }
            get { return _reserve3; }
        }
        #endregion Model
    }
}
