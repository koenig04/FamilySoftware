using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ItemConfigureProcess;
using Common;

namespace BLL.AssetInputAndOperationProcess.ItemLoading
{
    abstract class ItemLoadingStrategySuper
    {
        public event EventHandler<ItemSearchedCollectionArgs> LoadedItemsEvent;
        protected ItemProcess _itemProcess = null;

        public ItemLoadingStrategySuper(ItemProcess ItemProcess)
        {
            this._itemProcess = ItemProcess;
        }

        public abstract void LoadItems(ItemSelectedInfo info);

        protected void RaiseLoadedItemsEvent(ItemSearchedCollectionArgs e)
        {
            if (LoadedItemsEvent != null)
            {
                LoadedItemsEvent(null, e);
            }
        }
        
    }
}
