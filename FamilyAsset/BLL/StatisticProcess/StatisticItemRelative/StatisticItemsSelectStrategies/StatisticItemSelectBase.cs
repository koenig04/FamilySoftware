using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ItemConfigureProcess;
using BLL.StatisticProcess.StatisticItemRelative;
using Common;

namespace BLL.StatisticProcess.StatisticItemRelative.StatisticItemsSelectStrategies
{
    abstract class StatisticItemSelectBase
    {
        public event EventHandler<ClearItemsArgs> ClearItemsEvent;
        public event EventHandler<ItemCollectionOperationArgs> AddItemCollectionEvent;
        public event EventHandler<SelectItemArgs> ItemSelectedEvent;
        public event EventHandler ReactiveEvent;

        protected StatisticItemSelectBase _nextHandler;
        protected IItemConfigureProcess _itemProcess;

        public StatisticItemSelectBase(IItemConfigureProcess itemProcess)
        {
            _itemProcess = itemProcess;
        }

        public void SetNextHandler(StatisticItemSelectBase handler)
        {
            _nextHandler = handler;
            _nextHandler.ReactiveEvent += OnReactive;
        }

        public abstract void ProceedSelectedItem(SelectedStatisticItemInfo selectedItem);
        public virtual void OnReactive(object sender, EventArgs e) { }

        protected void RaiseClearItemsEvent(ClearItemsArgs e)
        {
            if (ClearItemsEvent != null)
            {
                ClearItemsEvent(null, e);
            }
        }

        protected void RaiseAddItemCollectionEvent(ItemCollectionOperationArgs e)
        {
            if (AddItemCollectionEvent != null)
            {
                AddItemCollectionEvent(null, e);
            }
        }

        protected void RaiseItemSelectedEvent(SelectItemArgs e)
        {
            if (ItemSelectedEvent != null)
            {
                ItemSelectedEvent(null, e);
            }
        }

        protected void RaiseReactiveEvent()
        {
            if (ReactiveEvent != null)
            {
                ReactiveEvent(null, EventArgs.Empty);
            }
        }
    }

   
}
