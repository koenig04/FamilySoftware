using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BLL;
using BLL.StatisticProcess.DiagramRelative;
using Common;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.Statistic.AccountDetail
{
    class AccountSortDetailViewModel : NotificationObject
    {
        private BitmapImage _itemImg;

        public BitmapImage ItemImg
        {
            get { return _itemImg; }
            set
            {
                _itemImg = value;
                RaisePropertyChanged("ItemImg");
            }
        }

        private int _recLength;

        public int RecLength
        {
            get { return _recLength; }
            set
            {
                _recLength = value;
                RaisePropertyChanged("RecLength");
            }
        }

        private Color _recColor;

        public Color RecColor
        {
            get { return _recColor; }
            set
            {
                _recColor = value;
                RaisePropertyChanged("RecColor");
            }
        }

        private string _itemName;

        public string ItemName
        {
            get { return _itemName; }
            set
            {
                _itemName = value;
                RaisePropertyChanged("ItemName");
            }
        }

        private string _itemPrecent;

        public string itemPrecent
        {
            get { return _itemPrecent; }
            set
            {
                _itemPrecent = value;
                RaisePropertyChanged("ItemPrecent");
            }
        }

        private string _itemTotal;

        public string ItemTotal
        {
            get { return _itemTotal; }
            set
            {
                _itemTotal = value;
                RaisePropertyChanged("ItemTotal");
            }
        }

        private Color _itemTotalColor;

        public Color ItemTotalColor
        {
            get { return _itemTotalColor; }
            set
            {
                _itemTotalColor = value;
                RaisePropertyChanged("ItemTotalColor");
            }
        }

        private string _itemAccountCount;

        public string ItemAccountCount
        {
            get { return _itemAccountCount; }
            set
            {
                _itemAccountCount = value;
                RaisePropertyChanged("ItemAccountCount");
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

        private void OnItemClickedEvent(object sender,StringEventArgs e)
        {
            if (ItemClickedEvent != null)
            {
                ItemClickedEvent(null, e);
            }
        }

        private int _totalRecLength = 450;

        public AccountSortDetailViewModel(AccountDetailBySort sortInfo, Color recColor, decimal segmentPercent)
        {
            ItemImg = new BitmapImage(new Uri(Common.GlobalVariables.iconPath.Replace("\\", "/") + sortInfo.IconURL,
                UriKind.RelativeOrAbsolute));
            RecLength = (int)(segmentPercent * _totalRecLength);
            RecColor = recColor;
            ItemName = sortInfo.ItemName;
            itemPrecent = (segmentPercent * 100).ToString() + "%";
            ItemTotal = (from d in sortInfo.AccountDetailCollection
                         select d.AccountAmount).Sum().ToString() + "元";
            ItemTotalColor = sortInfo.AccountDetailCollection[0].IsIncome ? Colors.Firebrick : Colors.LimeGreen;
            ItemAccountCount = sortInfo.AccountDetailCollection.Count().ToString() + "笔";
            DetailCollection = new ObservableCollection<AccountDetailViewModel>();
            foreach (BLL.StatisticProcess.DiagramRelative.AccountDetail item in sortInfo.AccountDetailCollection)
            {
                DetailCollection.Add(new AccountDetailViewModel(item));
                DetailCollection[DetailCollection.Count - 1].ItemClickedEvent += OnItemClickedEvent;
            }
        }

        private void OnItemClickedEvent(object sender, ItemModifyArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
