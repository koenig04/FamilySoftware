using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DAL;

namespace BLL
{
    public class AccountOperationInfo
    {
        public OperationType OperationType { get;  set; }
        public AccountInputInfo AccountInfo { get;  set; }
        public ItemSelectedInfo ItemInfo { get;  set; }
        public DateTime StartDate { get;  set; }
        public DateTime EndDate { get;  set; }

    }

    public class AccountInputInfo
    {
        public DateTime AccountDate { get; set; }
        public bool IsIncome { get; set; }
        public string ItemOneID { get; set; }
        public string ItemTwoID { get; set; }
        public decimal AccountAmount { get; set; }
        public string Phrases { get; set; }

        public static implicit operator Model.AccountInfo(AccountInputInfo info)
        {
            Model.AccountInfo accountInfo = new Model.AccountInfo()
            {
                AccountDate = info.AccountDate,
                ItemOneID = info.ItemOneID,
                ItemTwoID = info.ItemTwoID,
                AccountAmount = info.AccountAmount,
                Notice = info.Phrases
            };

            return accountInfo;
        }
    }

    public class AccountSearchedResultInfo
    {
        public AccountInputInfo AccountInfo { get; set; }
        public string ItemOneName { get; set; }
        public string ItemTwoName { get; set; }
        public string Icon { get; set; }
    }

    public class AccountSearchedCollectionArgs : EventArgs
    {
        public List<AccountSearchedResultInfo> AccountCollection { get; set; }
    }
}
