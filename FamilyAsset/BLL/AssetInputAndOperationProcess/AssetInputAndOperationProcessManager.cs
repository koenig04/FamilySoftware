using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.AssetInputAndOperationProcess.AccountOperation;
using BLL.AssetInputAndOperationProcess.ItemLoading;
using BLL.ItemConfigureProcess;
using Common;

namespace BLL.AssetInputAndOperationProcess
{
    public class AssetInputAndOperationProcessManager : IAssetInputAndOperationProcess
    {
        public event EventHandler<ItemSearchedCollectionArgs> ItemSearchedResultEvent;
        public event EventHandler<AccountSearchedCollectionArgs> AccountSearchedResultEvent;
        public event EventHandler<BoolenEventArgs> AccountOperationResultEvent;

        private ItemLoadingContext _itemProcess;
        private AccountProcess _accountProcess;

        public AssetInputAndOperationProcessManager()
        {
            this._itemProcess = new ItemLoadingContext();
            this._itemProcess.LoadedItemsEvent += OnLoadedItems;
            this._accountProcess = new AccountProcess();
            this._accountProcess.AccountOperationResultEvent += OnAccountOperationResult;
            this._accountProcess.AccountSearchedResultEvent += OnAccountSearchedResult;
        }

        private void OnAccountOperationResult(object sender, BoolenEventArgs e)
        {
            if (AccountOperationResultEvent != null)
            {
                AccountOperationResultEvent(sender, e);
            }
        }

        private void OnAccountSearchedResult(object sender, AccountSearchedCollectionArgs e)
        {
            if (AccountSearchedResultEvent != null)
            {
                AccountSearchedResultEvent(sender, e);
            }
        }

        private void OnLoadedItems(object sender, ItemSearchedCollectionArgs e)
        {
            if (ItemSearchedResultEvent != null)
            {
                ItemSearchedResultEvent(sender, e);
            }
        }

        public void HandleItemSelected(ItemSelectedInfo info)
        {
            _itemProcess.LoadItems(info);
        }

        public void HandleAccountInput(AccountInputInfo info)
        {
            _accountProcess.HandleAccountInput(info);
        }

        public void HandleAccountOperation(AccountOperationInfo info)
        {
            _accountProcess.HandleAccountOperation(info);
        }


        public Model.AccountInfo GetAccountModel(string accountID)
        {
            return _accountProcess.GetAccountModel(accountID);
        }
    }
}
