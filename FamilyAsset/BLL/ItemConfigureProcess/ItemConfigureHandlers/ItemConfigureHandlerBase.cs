using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using BLL.ActionDictionary;
using Common;
using DAL;

namespace BLL.ItemConfigureProcess
{
    abstract class ItemConfigureHandlerBase
    {
        public abstract void HandleItemValidOperation(ItemConfigureOperationValidInfo info);
        public abstract void HandleItemOperation(ItemConfigureOperationInfo info);
        public abstract void HandleItemSelected(ItemSelectedInfo info);        

        public event EventHandler<ItemPopWindowInfoArgs> ItemPopWindowEvent;
        public event EventHandler<ItemChangedInfoArgs> ItemChangedEvent;
        public event EventHandler<ItemSearchedCollectionArgs> ItemSearchedResultEvent;

        protected ItemConfigureHandlerBase _nextHandler;
        protected ItemProcess _itemProcessDal;

        public ItemConfigureHandlerBase()
        {
            _itemProcessDal = new ItemProcess();
        }

        private void ItemConfigureAction(ItemConfigureEventArgs e)
        {
            if (e.OptType == OperationType.Search)
            {
                switch (e.ItemType)
                {
                    case ItemType.ItemOne:

                        break;
                }
            }
        }

        public void SetNextHandler(ItemConfigureHandlerBase nextHandler)
        {
            this._nextHandler = nextHandler;
        }        

        protected void RaiseItemPopWindowEvent(ItemPopWindowInfoArgs e)
        {
            if (ItemPopWindowEvent != null)
            {
                ItemPopWindowEvent(null, e);
            }
        }

        protected void RaiseItemChangedEvent(ItemChangedInfoArgs e)
        {
            if (ItemChangedEvent != null)
            {
                ItemChangedEvent(null, e);
            }
        }

        protected void RaiseItemSearchedResultEvent(ItemSearchedCollectionArgs e)
        {
            if (ItemSearchedResultEvent != null)
            {
                ItemSearchedResultEvent(null, e);
            }
        }
    }
}
