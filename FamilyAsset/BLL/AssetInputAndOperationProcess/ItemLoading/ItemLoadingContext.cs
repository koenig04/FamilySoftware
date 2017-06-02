using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ItemConfigureProcess;
using Common;

namespace BLL.AssetInputAndOperationProcess.ItemLoading
{
    class ItemLoadingContext
    {
        public event EventHandler<HashtableEventArgs> LoadedItemsEvent;
        private ItemLoadingStrategySuper _itemLoadingStrategy = null;
        private ItemProcess _itemProcess = null;

        public ItemLoadingContext()
        {
            this._itemProcess = new ItemProcess();
        }

        public void LoadItems(ItemSelectedInfo info)
        {
            switch (info.ItemType)
            {
                case ItemType.None:
                    _itemLoadingStrategy = new ItemOneLoadingStrategy(this._itemProcess);
                    break;
                case ItemType.ItemOne:
                    _itemLoadingStrategy = new ItemTwoAndPhrasesLoadingStrategy(this._itemProcess);
                    break;
                case ItemType.ItemTwo:
                    _itemLoadingStrategy = new PhrasesLoadingStrategy(this._itemProcess);
                    break;
            }

            _itemLoadingStrategy.LoadedItemsEvent -= RaiseLoadedItemsEvent;
            _itemLoadingStrategy.LoadedItemsEvent += RaiseLoadedItemsEvent;

            _itemLoadingStrategy.LoadItems(info);
        }

        private void RaiseLoadedItemsEvent(object sender,HashtableEventArgs e)
        {
            if (LoadedItemsEvent != null)
            {
                LoadedItemsEvent(null, e);
            }
        }
    }
}
