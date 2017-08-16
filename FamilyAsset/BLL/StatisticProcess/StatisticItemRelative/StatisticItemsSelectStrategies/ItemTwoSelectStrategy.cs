using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace BLL.StatisticProcess.StatisticItemRelative.StatisticItemsSelectStrategies
{
    class ItemTwoSelectStrategy : StatisticItemSelectBase
    {
        private List<SelectedStatisticItemInfo> _lstSelectedItemTwos = new List<SelectedStatisticItemInfo>();

        public ItemTwoSelectStrategy(BLL.ItemConfigureProcess.IItemConfigureProcess itemProcess)
            : base(itemProcess)
        {

        }

        public override void ProceedSelectedItem(SelectedStatisticItemInfo selectedItem)
        {
            if (selectedItem.ItemType == ItemType.ItemTwo)
            {
                if (selectedItem.IsSelected)
                {
                    DealSelectionOperation(selectedItem);
                }
                else
                {
                    DealUnseletionOperation(selectedItem);
                }

                RaiseItemSelectedEvent(new SelectItemArgs()
                {
                    ItemInfo = selectedItem
                });
            }
        }

        private void DealSelectionOperation(SelectedStatisticItemInfo selectedItem)
        {
            if (_lstSelectedItemTwos.Count > 0)
            {
                RaiseClearItemsEvent(new ClearItemsArgs()
                {
                    IsIncome = selectedItem.IsIncome,
                    ClearedItemType = selectedItem.ItemType,
                    OnlyClearFromList = true
                });
            }
            
            _lstSelectedItemTwos.Add(selectedItem);
        }

        private void DealUnseletionOperation(SelectedStatisticItemInfo selectedItem)
        {
            if (_lstSelectedItemTwos.Count == 1)
            {
                RaiseReactiveEvent();
            }
            _lstSelectedItemTwos.Remove(_lstSelectedItemTwos.Where(a => a.ItemID == selectedItem.ItemID).First());
        }
    }
}
