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
            throw new NotImplementedException();
        }        
    }
}
