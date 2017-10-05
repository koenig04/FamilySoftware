using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using BLL.StatisticProcess.DiagramRelative;
using Common;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.Statistic.AccountDetail
{
    class AccountTimeCollectionViewModel : NotificationObject
    {
        private Visibility _vis;

        public Visibility TimeVis
        {
            get { return _vis; }
            set
            {
                _vis = value;
                RaisePropertyChanged("TimeVis");
            }
        }

        private ObservableCollection<AccountTimeDetailViewModel> _timeCollection;

        public ObservableCollection<AccountTimeDetailViewModel> TimeCollection
        {
            get { return _timeCollection; }
            set
            {
                _timeCollection = value;
                RaisePropertyChanged("TimeCollection");
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

        public AccountTimeCollectionViewModel()
        {
            TimeCollection = new ObservableCollection<AccountTimeDetailViewModel>();
        }

        public void UpdateCurveData(BLL.StatisticProcess.DiagramRelative.CurveData detail)
        {
            TimeCollection.Clear();
            foreach (AccountDetailByDate item in detail.Details)
            {
                TimeCollection.Add(new AccountTimeDetailViewModel(item));                
            }
        }
    }
}
