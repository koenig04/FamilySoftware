using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ItemConfigureProcess;
using Common;

namespace BLL.AssetInputAndOperationProcess.ItemLoading
{
    class ItemLoadingStrategySuper
    {
        public event EventHandler<HashtableEventArgs> LoadedItemsEvent;
        protected ItemProcess _itemProcess = null;

        public ItemLoadingStrategySuper(ItemProcess ItemProcess)
        {
            this._itemProcess = ItemProcess;
        }

        public abstract void LoadItems(ItemSelectedInfo info);

        protected void RaiseLoadedItemsEvent(HashtableEventArgs e)
        {
            if (LoadedItemsEvent != null)
            {
                LoadedItemsEvent(null, e);
            }
        }
        
    }
}
