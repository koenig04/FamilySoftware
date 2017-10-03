using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.StatisticProcess.DiagramRelative;
using Common;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.Statistic.AccountDetail
{
    class AccountTimeDetailViewModel : NotificationObject
    {
        private string _detailDate;

        public string DetailDate
        {
            get { return _detailDate; }
            set
            {
                _detailDate = value;
                RaisePropertyChanged("DetailDate");
            }
        }

        private ObservableCollection<AccountDetailViewModel> _detailCollection;

        public ObservableCollection<AccountDetailViewModel> DetailCollection
        {
            get { return _detailCollection; }
            set
            {
                _detailCollection = value;
                RaisePropertyChanged("DetailCollection");
            }
        }

        public event EventHandler<StringEventArgs> ItemClickedEvent;

        private void OnItemClickedEvent(object sender, StringEventArgs e)
        {
            if (ItemClickedEvent != null)
            {
                ItemClickedEvent(null, e);
            }
        }

        public AccountTimeDetailViewModel(AccountDetailByDate dateInfo)
        {
            DetailDate = dateInfo.AccountDate.ToString("yyyy-MM-dd");
            foreach (BLL.StatisticProcess.DiagramRelative.AccountDetail item in dateInfo.AccountDetailCollection)
            {
                DetailCollection.Add(new AccountDetailViewModel(item));
                DetailCollection[DetailCollection.Count - 1].ItemClickedEvent += OnItemClickedEvent;
            }
        }
    }
}
