using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ItemConfigureProcess;

namespace BLL.StatisticProcess.StatisticItemRelative.StatiticItemsSelectionStrategies
{
    class ItemSelectionContext
    {
        public event EventHandler<ClearItemsArgs> ClearItemsEvent;
        public event EventHandler<SelectItemArgs> ItemSelectedEvent;

        private StatisticItemSelectionBase _inOutStrategy, _itemOneStrategy, _itemTwoStrategy;

        public ItemSelectionContext(IItemConfigureProcess itemProcess)
        {
            _inOutStrategy = new InOrOutItemSelectionStrategy(itemProcess);
            _itemOneStrategy = new ItemOneSelectionStrategy(itemProcess);
            _itemTwoStrategy = new ItemTwoSelectionStrategy(itemProcess);

            _inOutStrategy.ClearItemsEvent += OnClearItems;
            _itemOneStrategy.ClearItemsEvent += OnClearItems;
            _itemTwoStrategy.ClearItemsEvent += OnClearItems;

            _inOutStrategy.ItemSelectedEvent += OnItemSelected;
            _itemOneStrategy.ItemSelectedEvent += OnItemSelected;
            _itemTwoStrategy.ItemSelectedEvent += OnItemSelected;
        }

        private void OnItemSelected(object sender, SelectItemArgs e)
        {
            if (ItemSelectedEvent != null)
            {
                ItemSelectedEvent(sender, e);
            }
        }

        private void OnClearItems(object sender, ClearItemsArgs e)
        {
            if (ClearItemsEvent != null)
            {
                ClearItemsEvent(sender, e);
            }
        }

        public void ProceedSelectedItem(SelectedStatisticItemInfo selectedItem)
        {
            CreateStrategy(selectedItem).ProceedSelectedItem(selectedItem);
        }

        private StatisticItemSelectionBase CreateStrategy(SelectedStatisticItemInfo selectedItem)
        {
            switch (selectedItem.ItemType)
            {
                case Common.ItemType.None:
                    return _inOutStrategy;
                case Common.ItemType.ItemOne:
                    return _itemOneStrategy;
                case Common.ItemType.ItemTwo:
                    return _itemTwoStrategy;
                default:
                    return null;
            }
        }
    }
}
