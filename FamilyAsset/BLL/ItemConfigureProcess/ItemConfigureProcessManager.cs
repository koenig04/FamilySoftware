using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Common;
using DAL;

namespace BLL.ItemConfigureProcess
{
    public class ItemConfigureProcessManager : IItemConfigureProcess
    {
        public event EventHandler<ItemPopWindowInfoArgs> ItemPopWindowEvent;
        public event EventHandler<ItemChangedInfoArgs> ItemChangedEvent;
        public event EventHandler<ItemSearchedCollectionArgs> ItemSearchedResultEvent;

       
        private ItemConfigureHandlerBase _itemConfigureHandler;

        public ItemConfigureProcessManager()
        {
            //创建并组装ItemConfigureHandler责任链
            ItemConfigureHandlerItemOne handlerItemOne = new ItemConfigureHandlerItemOne();
            ItemConfigureHandlerItemTwo handlerItemTwo = new ItemConfigureHandlerItemTwo();
            ItemConfigureHandlerPhrase handlerPhrase = new ItemConfigureHandlerPhrase();

            handlerItemOne.ItemSearchedResultEvent += OnItemSearchedResult;
            handlerItemTwo.ItemSearchedResultEvent += OnItemSearchedResult;
            handlerPhrase.ItemSearchedResultEvent += OnItemSearchedResult;

            handlerItemOne.ItemChangedEvent += OnItemChanged;
            handlerItemTwo.ItemChangedEvent += OnItemChanged;
            handlerPhrase.ItemChangedEvent += OnItemChanged;

            handlerItemOne.ItemPopWindowEvent += OnItemPopWindow;
            handlerItemTwo.ItemPopWindowEvent += OnItemPopWindow;
            handlerPhrase.ItemPopWindowEvent += OnItemPopWindow;

            handlerItemTwo.SetNextHandler(handlerPhrase);
            handlerItemOne.SetNextHandler(handlerItemTwo);
            _itemConfigureHandler = handlerItemOne;
        }

        public void HandleItemValidOperation(ItemConfigureOperationValidInfo info)
        {
            _itemConfigureHandler.HandleItemValidOperation(info);
        }

        public void HandleCallBack(CallBackInfo info)
        {
            
        }

        public void HandleItemSelected(ItemSelectedInfo info)
        {
            _itemConfigureHandler.HandleItemSelected(info);
        }

        void OnItemSearchedResult(object sender, ItemSearchedCollectionArgs e)
        {
            if (ItemSearchedResultEvent != null)
            {
                ItemSearchedResultEvent(sender, e);
            }
        }

        void OnItemChanged(object sender, ItemChangedInfoArgs e)
        {
            if (ItemChangedEvent != null)
            {
                ItemChangedEvent(sender, e);
            }
        }

        void OnItemPopWindow(object sender, ItemPopWindowInfoArgs e)
        {
            if (ItemPopWindowEvent != null)
            {
                ItemPopWindowEvent(sender, e);
            }
        }

        public void HandleItemOperation(ItemConfigureOperationInfo info)
        {
            _itemConfigureHandler.HandleItemOperation(info);
        }
    }
}
