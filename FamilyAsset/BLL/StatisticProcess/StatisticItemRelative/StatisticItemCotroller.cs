using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ItemConfigureProcess;
using BLL.StatisticProcess.StatisticItemRelative.StatisticItemsSelectStrategies;
using Common;

namespace BLL.StatisticProcess.StatisticItemRelative
{
    class StatisticItemCotroller
    {
        public event EventHandler<ClearItemsArgs> ItemCollectionClearEvent;
        public event EventHandler<SelectItemArgs> ItemSelectEvent;
        public event EventHandler<ItemCollectionOperationArgs> ItemCollectionAddEvent;

        private IItemConfigureProcess _itemProcess;
        private StatisticItemSelectBase _statisticItemProcess;
        private bool _currentInOrOutForItemTwo;

        public StatisticItemCotroller()
        {
            _itemProcess = new ItemConfigureProcessManager();
            _itemProcess.ItemSearchedResultEvent += OnStatisticItemSearched;

        }

        private void OnStatisticItemSearched(object sender, ItemSearchedCollectionArgs e)
        {
            switch (e.ItemType)
            {
                case ItemType.ItemOne:

                    break;
                case ItemType.ItemTwo:

                    break;
            }
        }

        public void InitializeItemOnes()
        {

        }

        public void ProceedSelectedItem(SelectedStatisticItemInfo selectedItem)
        {
            if (selectedItem.ItemType == ItemType.ItemOne)
            {
                _currentInOrOutForItemTwo = selectedItem.IsIncome;
            }
        }
    }

   public  class ClearItemsArgs : EventArgs
    {
        public bool IsIncome { get; set; }
        public bool OnlyClearFromList { get; set; }
        public bool ConvertToUnselected { get; set; }
        public ItemType ClearedItemType { get; set; }
    }

    public class SelectItemArgs : EventArgs
    {
        public bool AddToList { get; set; }
        public bool ConverToSelected { get; set; }
        public SelectedStatisticItemInfo ItemInfo { get; set; }
    }

    public class ItemCollectionOperationArgs : EventArgs
    {
        public bool IsIncome { get; set; }
        public List<SelectedStatisticItemInfo> ItemTwoCollection { get; set; }
        public List<SelectedStatisticItemInfo> ItemOneCollection { get; set; }
    }

    public class SelectedStatisticItemInfo
    {
        public bool IsSelected { get; set; }
        public string ItemID { get; set; }
        public bool IsIncome { get; set; }
        public ItemType ItemType { get; set; }

        public static implicit operator SelectedStatisticItemInfo(Model.JZItemOne model)
        {
            SelectedStatisticItemInfo res = new SelectedStatisticItemInfo();
            res.ItemID = model.JZItemOneID;
            res.ItemType = ItemType.ItemOne;
            res.IsIncome = model.IncomeOrCost;
            return res;
        }
    }
}
