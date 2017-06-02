using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ItemConfigureProcess;
using Common;

namespace BLL.AssetInputAndOperationProcess
{
    public class AssetInputAndOperationProcessManage : IAssetInputAndOperationProcess
    {
        public event EventHandler<ItemSearchedCollectionArgs> ItemSearchedResultEvent;
        public event EventHandler<AccountSearchedCollectionArgs> AccountSearchedResultEvent;
        public event EventHandler<BoolenEventArgs> AccountOperationResultEvent;

        private ItemProcess _itemProcess;
        private AccountProcess _accountProcess;

        public AssetInputAndOperationProcessManage()
        {
            this._itemProcess = new ItemProcess();
            this._accountProcess = new AccountProcess();
        }

        public void HandleItemSelected(ItemSelectedInfo info)
        {
            switch (info.ItemType)
            {
                //case ItemType.None:
                //    this._itemProcess.LoadItemOne(
            }
        }

        public void HandleAccountInput(AccountInputInfo info)
        {
            throw new NotImplementedException();
        }

        public void HandleAccountOperation(AccountOperationInfo info)
        {
            throw new NotImplementedException();
        }        
    }
}
