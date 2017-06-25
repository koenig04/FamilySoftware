using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.AssetInputAndOperationProcess;
using FamilyAsset.Pages.AccountRecord.Elements;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.AccountRecord
{
    class AccountRecordViewModel : NotificationObject
    {
        private ObservableCollection<AccountItemViewModel> _itemOneCollection;

        public ObservableCollection<AccountItemViewModel> ItemOneCollection
        {
            get { return _itemOneCollection; }
            set
            {
                _itemOneCollection = value;
                RaisePropertyChanged("ItemOneCollection");
            }
        }

        private ObservableCollection<AccountItemViewModel> _itemTwoCollection;

        public ObservableCollection<AccountItemViewModel> ItemTwoCollction
        {
            get { return _itemTwoCollection; }
            set
            {
                _itemTwoCollection = value;
                RaisePropertyChanged("ItemTwoCollection");
            }
        }

        private string _selectedItemOneID;
        private string _selectedItemTwoID;
        private IAssetInputAndOperationProcess _accountProcess;

        public AccountRecordViewModel()
        {
            _accountProcess = new AssetInputAndOperationProcessManager();
            _accountProcess.ItemSearchedResultEvent += OnItemSearched;
            _accountProcess.AccountOperationResultEvent += OnAccountOperated;
        }

        private void OnAccountOperated(object sender, Common.BoolenEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnItemSearched(object sender, BLL.ItemSearchedCollectionArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
