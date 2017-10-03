using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Common;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.Statistic.AccountDetail
{
    class AccountDetailViewModel : NotificationObject
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

        private string _amount;

        public string Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                RaisePropertyChanged("Amount");
            }
        }

        private Color _accountColor;

        public Color AccountColor
        {
            get { return _accountColor; }
            set
            {
                _accountColor = value;
                RaisePropertyChanged("AccountColor");
            }
        }

        private DelegateCommand _accountItemClicked;

        public DelegateCommand AccountItemClicked
        {
            get
            {
                if (_accountItemClicked == null)
                {
                    _accountItemClicked = new DelegateCommand(o =>
                        {
                            RaiseItemClickedEvent(_detailID);
                        });
                }
                return _accountItemClicked;
            }
            set
            {
                _accountItemClicked = value;
                RaisePropertyChanged("AccountItemClicked");
            }
        }


        public event EventHandler<StringEventArgs> ItemClickedEvent;

        protected void RaiseItemClickedEvent(string e)
        {
            if (ItemClickedEvent != null)
            {
                ItemClickedEvent(null, new StringEventArgs(e));
            }
        }

        private string _detailID;

        public AccountDetailViewModel(BLL.StatisticProcess.DiagramRelative.AccountDetail detail)
        {
            this._detailID = detail.AccountID;
            DetailDate = detail.AccountDate.ToString("yyyy-MM-dd");
            Amount = detail.AccountAmount.ToString() + " 元";
            AccountColor = detail.IsIncome ? Colors.LimeGreen : Colors.Firebrick;
            ItemName = detail.ItemOneName + (string.IsNullOrEmpty(detail.ItemTwoName) ? "" : ("-" + detail.ItemTwoName));
        }
    }
}
