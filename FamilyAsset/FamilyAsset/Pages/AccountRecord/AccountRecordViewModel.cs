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

namespace FamilyAsset.Pages.AccountRecord
{
    class AccountRecordViewModel : UserControlViewModelBase
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

        public AccountRecordViewModel()
        {
            Vis = Visibility.Hidden;
            try
            {
                _accountProcess = new AssetInputAndOperationProcessManager();
                _accountProcess.ItemSearchedResultEvent += OnItemSearched;
                _accountProcess.HandleItemSelected(new ItemSelectedInfo() { IsIncome = true });
            }
            catch { }
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

        public override void HandleViewModelCallBack(ViewModelCallBackInfo callbackInfo)
        {
            throw new NotImplementedException();
        }
    }
}
