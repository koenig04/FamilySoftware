using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.Statistic.AccountDetail
{
    class AccountDetailBase : NotificationObject
    {
        public event EventHandler<StatisticItemClickedEvnetArgs> ItemClickedEvent;

        protected void RaiseItemClickedEvent(StatisticItemClickedEvnetArgs e)
        {
            if (ItemClickedEvent != null)
            {
                ItemClickedEvent(null, e);
            }
        }
    }

    class StatisticItemClickedEvnetArgs : EventArgs
    {
        /// <summary>
        /// 0:Sort 1:AccountRecord
        /// </summary>
        public int ItemType { get; set; }
        public string ItemID { get; set; }
    }
}
