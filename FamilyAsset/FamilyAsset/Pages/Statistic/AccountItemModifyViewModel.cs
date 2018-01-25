using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BLL;
using BLL.AssetInputAndOperationProcess;
using Common;
using FamilyAsset.Pages.AccountRecord.Elements;
using FamilyAsset.Pages.SysConfigure.Element.ItemConfigure;
using FamilyAsset.UICore;
using Model;

namespace FamilyAsset.Pages.Statistic
{
    class AccountItemModifyViewModel : NotificationObject
    {
        private ObservableCollection<AccountItemViewModel> _itemOneCollection;

        public ObservableCollection<AccountItemViewModel> ItemOneCollection
        {
            get
            {
                if (_itemOneCollection == null)
                {
                    _itemOneCollection = new ObservableCollection<AccountItemViewModel>();
                }
                return _itemOneCollection;
            }
            set
            {
                _itemOneCollection = value;
                RaisePropertyChanged("ItemOneCollection");
            }
        }

        private ObservableCollection<AccountItemViewModel> _itemTwoCollection;

        public ObservableCollection<AccountItemViewModel> ItemTwoCollection
        {
            get
            {
                if (_itemTwoCollection == null)
                {
                    _itemTwoCollection = new ObservableCollection<AccountItemViewModel>();
                }
                return _itemTwoCollection;
            }
            set
            {
                _itemTwoCollection = value;
                RaisePropertyChanged("ItemTwoCollection");
            }
        }

        private InOutSwitchViewModel _inOrOut;

        public InOutSwitchViewModel InOrOut
        {
            get
            {
                if (_inOrOut == null)
                {
                    _inOrOut = new InOutSwitchViewModel();
                    _inOrOut.IsIncomeEvent += OnInOrOut;
                }
                return _inOrOut;
            }
            set
            {
                _inOrOut = value;
                RaisePropertyChanged("InOrOut");
            }
        }

        private DelegateCommand _return;

        public DelegateCommand Return
        {
            get
            {
                _return = _return ?? (new DelegateCommand(
                    o =>
                    {
                        Vis = Visibility.Collapsed;
                    }
                    ));
                return _return;
            }
            set
            {
                _return = value;
                RaisePropertyChanged("Return");
            }
        }

        private DelegateCommand _accountModify;

        public DelegateCommand ModifyAccount
        {
            get
            {
                _accountModify = _accountModify ?? (new DelegateCommand(
                    o =>
                    {
                        RaiseAccountInfoOperationEvent(OperationType.Modify);
                    }));
                return _accountModify;
            }
            set
            {
                _accountModify = value;
                RaisePropertyChanged("ModifyAccount");
            }
        }

        private DelegateCommand _deleteAccount;

        public DelegateCommand DeleteAccount
        {
            get
            {
                _deleteAccount = _deleteAccount ?? (new DelegateCommand(
                    o =>
                    {
                        RaiseAccountInfoOperationEvent(OperationType.Delete);
                    }));
                return _deleteAccount;
            }
            set
            {
                _deleteAccount = value;
                RaisePropertyChanged("DeleteAccount");
            }
        }

        /// <summary>
        /// raise account modify and delete event
        /// if it is modify event,we must callback all the account info which is modified.
        /// if it is delete event,we only need to callback the account id.
        /// </summary>
        /// <param name="operation"></param>
        private void RaiseAccountInfoOperationEvent(OperationType operation)
        {
            if (AccountInfoOperationEvent != null)
            {
                if (operation == OperationType.Modify)
                {
                    AccountInfoOperationEvent(null, new AccountInfoOperationInfoArgs()
                    {
                        AccountInfo = new AccountInfo()
                        {
                            AccountAmount = decimal.Parse(this.AccountAmount),
                            AccountDate = this.AccountDate,
                            AccountID = _accountID,
                            ItemOneID = _selectedItemOneID,
                            ItemTwoID = _selectedItemTwoID,
                            Notice = this.Notice
                        },
                        Operation = operation
                    });
                }
                else if (operation == OperationType.Delete)
                {
                    AccountInfoOperationEvent(null, new AccountInfoOperationInfoArgs()
                    {
                        AccountInfo = new AccountInfo()
                        {
                            AccountID = _accountID
                        },
                        Operation = operation
                    });
                }
            }
        }

        /// <summary>
        /// when in/out is switched, we should at first clear all the screen items.
        /// then load all the itemone which are income/cost
        /// </summary>
        /// <param name="obj"></param>
        private void OnInOrOut(bool obj)
        {
            this._isInOrOut = obj;
            ClearItems(new List<AccountItemType>() { AccountItemType.All });
            this._accountProcess.HandleItemSelected(new ItemSelectedInfo() { IsIncome = _isInOrOut });
        }

        private string _accountAmount;

        public string AccountAmount
        {
            get { return _accountAmount; }
            set
            {
                _accountAmount = value;
                RaisePropertyChanged("AccountAmount");
            }
        }

        private DateTime _accountDate;

        public DateTime AccountDate
        {
            get { return _accountDate; }
            set
            {
                _accountDate = value;
                RaisePropertyChanged("AccountDate");
            }
        }


        private string _notice;

        public string Notice
        {
            get { return _notice; }
            set
            {
                _notice = value;
                RaisePropertyChanged("Notice");
            }
        }

        private ObservableCollection<PhraseViewModel> _phraseCollection;

        public ObservableCollection<PhraseViewModel> PhrasesCollection
        {
            get
            {
                if (_phraseCollection == null)
                {
                    _phraseCollection = new ObservableCollection<PhraseViewModel>();
                }
                return _phraseCollection;
            }
            set
            {
                _phraseCollection = value;
                RaisePropertyChanged("PhrasesCollection");
            }
        }

        private Visibility _vis = Visibility.Visible;

        public Visibility Vis
        {
            get { return _vis; }
            set
            {
                _vis = value;
                RaisePropertyChanged("Vis");
            }
        }

        private enum AccountItemType
        {
            All,
            ItemOne,
            ItemTwo,
            Phrese,
            Amount,
            Notice
        }

        private void ClearItems(List<AccountItemType> clearedItemCollection)
        {
            if (clearedItemCollection.Contains(AccountItemType.All) || clearedItemCollection.Contains(AccountItemType.ItemOne))
            {
                ItemOneCollection.Clear();
            }
            if (clearedItemCollection.Contains(AccountItemType.All) || clearedItemCollection.Contains(AccountItemType.ItemTwo))
            {
                ItemTwoCollection.Clear();
            }
            if (clearedItemCollection.Contains(AccountItemType.All) || clearedItemCollection.Contains(AccountItemType.Phrese))
            {
                PhrasesCollection.Clear();
            }
            if (clearedItemCollection.Contains(AccountItemType.All) || clearedItemCollection.Contains(AccountItemType.Amount))
            {
                AccountAmount = string.Empty;
            }
            if (clearedItemCollection.Contains(AccountItemType.All) || clearedItemCollection.Contains(AccountItemType.Notice))
            {
                Notice = string.Empty;
            }
        }

        private string _selectedItemOneID;
        private string _selectedItemTwoID;
        private bool _isInOrOut = true;
        private IAssetInputAndOperationProcess _accountProcess;
        private string _accountID;

        public event EventHandler<AccountInfoOperationInfoArgs> AccountInfoOperationEvent;

        public AccountItemModifyViewModel()
        {
            Vis = Visibility.Collapsed;
            try
            {
                _accountProcess = new AssetInputAndOperationProcessManager();
                _accountProcess.ItemSearchedResultEvent += OnItemSearched;
            }
            catch { }
        }

        public void UpdateAccountInfo(string accountID, bool isIncome)
        {
            Model.AccountInfo info = _accountProcess.GetAccountModel(accountID);
            _accountProcess.HandleItemSelected(new ItemSelectedInfo() { IsIncome = isIncome });
            ItemOneCollection.Where(a => a.ItemName == info.ItemOneName).First().ConvertToPressed();
            if (!string.IsNullOrEmpty(info.ItemTwoName))
            {
                ItemTwoCollection.Where(a => a.ItemName == info.ItemTwoName).First().ConvertToPressed();
            }
            Notice = info.Notice;
            AccountDate = info.AccountDate;
            AccountAmount = info.AccountAmount.ToString();
            _accountID = accountID;
        }

        private void OnItemSearched(object sender, ItemSearchedCollectionArgs e)
        {
            switch (e.ItemType)
            {
                case ItemType.None:
                    if (e.ItemCollection != null && (e.ItemCollection as Hashtable).ContainsKey("One"))
                    {
                        foreach (JZItemOne item in (e.ItemCollection as Hashtable)["One"] as List<JZItemOne>)
                        {
                            AccountItemViewModel ivm = new AccountItemViewModel(ItemType.ItemOne, item, _isInOrOut);
                            ivm.ItemClickedEvent += OnItemClicked;
                            this.ItemOneCollection.Add(ivm);
                        }
                    }
                    break;
                case ItemType.ItemOne:
                    if (e.ItemCollection != null && (e.ItemCollection as Hashtable).ContainsKey("Two"))
                    {
                        foreach (JZItemTwo item in (e.ItemCollection as Hashtable)["Two"] as List<JZItemTwo>)
                        {
                            AccountItemViewModel ivm = new AccountItemViewModel(ItemType.ItemTwo, item, _isInOrOut);
                            ivm.ItemClickedEvent += OnItemClicked;
                            this.ItemTwoCollection.Add(ivm);
                        }
                    }
                    if (e.ItemCollection != null && (e.ItemCollection as Hashtable).ContainsKey("Phrase"))
                    {
                        foreach (Phrase item in (e.ItemCollection as Hashtable)["Phrase"] as List<Phrase>)
                        {
                            PhraseViewModel ivm = new PhraseViewModel(item.PhraseContent);
                            ivm.PhraseSelectedEvent += OnPhraseSelected;
                            this.PhrasesCollection.Add(ivm);
                        }
                    }
                    break;
                case ItemType.ItemTwo:
                    if (e.ItemCollection != null && (e.ItemCollection as Hashtable).ContainsKey("Phrase"))
                    {
                        foreach (Phrase item in (e.ItemCollection as Hashtable)["Phrase"] as List<Phrase>)
                        {
                            PhraseViewModel ivm = new PhraseViewModel(item.PhraseContent);
                            ivm.PhraseSelectedEvent += OnPhraseSelected;
                            this.PhrasesCollection.Add(ivm);
                        }
                    }
                    break;
            }
        }

        private void OnPhraseSelected(object sender, PhraseSelectedEventArgs e)
        {
            if (e.IsSelected)
            {
                this.Notice += (e.PhraseName + ";");
            }
            else
            {
                if (this.Notice.Contains(e.PhraseName + ";"))
                {
                    int index = this.Notice.IndexOf(e.PhraseName);
                    this.Notice = this.Notice.Substring(0, index) +
                        this.Notice.Substring(index + e.PhraseName.Length + 1, this.Notice.Length - index);
                }
            }
        }


        private void OnItemClicked(object sender, SysConfigure.Element.ItemConfigure.ItemClickedEventArgs e)
        {
            switch (e.SelectedItem.ItemType)
            {
                case ItemType.ItemOne:
                    foreach (AccountItemViewModel item in ItemOneCollection.Where(a => a.SelectedItem != e.SelectedItem))
                    {
                        item.ConvertToUnPressed();
                    }
                    if (e.SelectedItem.ItemID != this._selectedItemOneID)
                    {
                        ClearItems(new List<AccountItemType>()
                        {
                            AccountItemType.ItemTwo,
                            AccountItemType.Phrese,
                            AccountItemType.Notice
                        });

                    }
                    break;
                case ItemType.ItemTwo:
                    foreach (AccountItemViewModel item in ItemTwoCollection.Where(a => a.SelectedItem != e.SelectedItem))
                    {
                        item.ConvertToUnPressed();
                    }
                    if (e.SelectedItem.ItemID != this._selectedItemTwoID)
                    {
                        ClearItems(new List<AccountItemType>()
                        {
                            AccountItemType.Phrese,
                            AccountItemType.Notice
                        });
                    }
                    break;
            }
            this._accountProcess.HandleItemSelected(e.SelectedItem);
        }

        //public override void HandleViewModelCallBack(ViewModelCallBackInfo callbackInfo)
        //{
        //    throw new NotImplementedException();
        //}
    }

    class AccountInfoOperationInfoArgs : EventArgs
    {
        public Model.AccountInfo AccountInfo { get; set; }
        public OperationType Operation { get; set; }
    }
}
