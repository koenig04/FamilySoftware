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
    class AccountSortCollectionViewModel : NotificationObject
    {
        private Visibility _vis;

        public Visibility SortVis
        {
            get { return _vis; }
            set
            {
                _vis = value;
                RaisePropertyChanged("SortVis");
            }
        }

        private ObservableCollection<AccountSortDetailViewModel> _sortCollection;

        public ObservableCollection<AccountSortDetailViewModel> SortCollection
        {
            get { return _sortCollection; }
            set
            {
                _sortCollection = value;
                RaisePropertyChanged("SortCollection");
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

        public AccountSortCollectionViewModel()
        {
            SortCollection = new ObservableCollection<AccountSortDetailViewModel>();
        }

        public void UpdatePieData(BLL.StatisticProcess.DiagramRelative.PieData detail)
        {
            List<Color> colors = StatisticColorSet.GetSeperateColors(detail.PieDataDetailCollection.Count);
            SortCollection.Clear();
            int colorIndex = 0;
            decimal totalAmount = (from d in detail.PieDataDetailCollection
                                   select d.SumAmount).Sum();
            foreach (AccountDetailBySort item in detail.Details)
            {
                SortCollection.Add(new AccountSortDetailViewModel(item,
                    colors[colorIndex],
                    detail.PieDataDetailCollection[colorIndex].SumAmount / totalAmount));
                SortCollection[colorIndex].ItemClickedEvent += OnItemClickedEvent;
                colorIndex++;
            }
        }
    }
}
