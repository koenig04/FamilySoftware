using System;
using BLL.ItemConfigureProcess;

namespace BLL.StatisticProcess.StatisticItemRelative.StatiticItemsSelectionStrategies
{
    abstract class StatisticItemSelectionBase
    {
        /// <summary>
        /// Clear this sort of items
        /// </summary>
        public event EventHandler<ClearItemsArgs> ClearItemsEvent;
        /// <summary>
        /// Convert this sort of items to selected(only for shown) 
        /// </summary>
        public event EventHandler<SelectItemArgs> ItemSelectedEvent;

        protected IItemConfigureProcess _itemProcess;

        public StatisticItemSelectionBase(IItemConfigureProcess itemProcess)
        {
            _itemProcess = itemProcess;
        }

        public abstract void ProceedSelectedItem(SelectedStatisticItemInfo selectedItem);


        protected void RaiseClearItemsEvent(ClearItemsArgs e)
        {
            if (ClearItemsEvent != null)
            {
                ClearItemsEvent(null, e);
            }
        }       

        protected void RaiseItemSelectedEvent(SelectItemArgs e)
        {
            if (ItemSelectedEvent != null)
            {
                ItemSelectedEvent(null, e);
            }
        }
    }
}
