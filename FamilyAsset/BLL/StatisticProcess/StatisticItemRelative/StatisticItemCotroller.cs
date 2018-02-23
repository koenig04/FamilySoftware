using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ItemConfigureProcess;
using BLL.StatisticProcess.StatisticItemRelative.StatiticItemsSelectionStrategies;
using Common;

namespace BLL.StatisticProcess.StatisticItemRelative
{
    /// <summary>
    /// An controller to control the statistic items
    /// 1. Allincome and allcost can be selected in the same time.
    /// 2. Item one and item two which are in the same income/cost sort can not be selected in the same time.
    /// 3. Items in different income/cost sort can not be selected in the same time.
    /// 4. The items in the same item one/two sort can be multible selected.
    /// 5. When the count of selected items is changed to one, the item two of this selected item should be shown.
    /// </summary>
    class StatisticItemCotroller
    {
        public event EventHandler<ClearItemsArgs> ItemCollectionClearEvent;
        public event EventHandler<SelectItemArgs> ItemSelectEvent;
        public event EventHandler<ItemCollectionOperationArgs> ItemCollectionAddEvent;

        private IItemConfigureProcess _itemProcess;
        private ItemSelectionContext _itemSelector;
        private List<SelectedStatisticItemInfo> _lstSelectedItems = new List<SelectedStatisticItemInfo>();
        private bool _currentInOrOutForItemTwo;

        public StatisticItemCotroller()
        {
            _itemProcess = new ItemConfigureProcessManager();
            _itemProcess.ItemSearchedResultEvent += OnStatisticItemSearched;

            _itemSelector = new ItemSelectionContext(_itemProcess);
            _itemSelector.ClearItemsEvent += OnClearItems;
        }

        private void OnClearItems(object sender, ClearItemsArgs e)
        {
            _lstSelectedItems.RemoveAll(a => a.IsIncome == e.IsIncome && a.ItemType == e.ClearedItemType);
            if (ItemCollectionClearEvent != null)
            {
                ItemCollectionClearEvent(sender, e);
            }
        }

        private void OnItemSelected(object sender, SelectItemArgs e)
        {            
            if (ItemSelectEvent != null)
            {
                ItemSelectEvent(sender, e);
            }
        }

        private void RaiseItemCollectionAddEvent(ItemCollectionOperationArgs e)
        {
            if (ItemCollectionAddEvent != null)
            {
                ItemCollectionAddEvent(null, e);
            }
        }

        private void OnStatisticItemSearched(object sender, ItemSearchedCollectionArgs e)
        {
            ItemCollectionOperationArgs res = new ItemCollectionOperationArgs();
            switch (e.ItemType)
            {
                case ItemType.ItemOne:
                    List<Model.JZItemOne> lstOnes = e.ItemCollection["One"] as List<Model.JZItemOne>;
                    res.ItemOneCollection = new List<SelectedStatisticItemInfo>();
                    foreach (Model.JZItemOne item in lstOnes)
                    {
                        res.ItemOneCollection.Add(item);
                    }
                    if (res.ItemOneCollection.Count > 0)
                    {
                        res.IsIncome = res.ItemOneCollection[0].IsIncome;
                    }
                    break;
                case ItemType.ItemTwo:
                    List<Model.JZItemTwo> lstTwos = e.ItemCollection["Two"] as List<Model.JZItemTwo>;
                    res.ItemTwoCollection = new List<SelectedStatisticItemInfo>();
                    foreach (Model.JZItemTwo item in lstTwos)
                    {
                        res.ItemTwoCollection.Add(SelectedStatisticItemInfo.ConvertFromItemTwo(item, _currentInOrOutForItemTwo));
                    }
                    if (res.ItemTwoCollection.Count > 0)
                    {
                        res.IsIncome = res.ItemTwoCollection[0].IsIncome;
                    }
                    break;
            }
            if (e.ItemType == ItemType.ItemOne || e.ItemType == ItemType.ItemTwo)
                RaiseItemCollectionAddEvent(res);
        }

        public void InitializeItemOnes()
        {
            _itemProcess.HandleItemSelected(new ItemSelectedInfo()
            {
                IsIncome = true
            });
            _itemProcess.HandleItemSelected(new ItemSelectedInfo()
            {
                IsIncome = false
            });
        }

        public void ProceedSelectedItem(SelectedStatisticItemInfo selectedItem)
        {
            if (selectedItem.IsSelected)
            {
                _lstSelectedItems.Add(selectedItem);
            }
            else
            {
                _lstSelectedItems.RemoveAll(a => a.ItemID == selectedItem.ItemID);
            }
            _currentInOrOutForItemTwo = selectedItem.IsIncome;
            _itemSelector.ProceedSelectedItem(selectedItem);
        }

        public List<SelectedStatisticItemInfo> GetStatisticItems()
        {
            if (_lstSelectedItems.Where(a => a.ItemType == ItemType.ItemTwo).Count() > 0)
            {
                return _lstSelectedItems.Where(a => a.ItemType == ItemType.ItemTwo).ToList();
            }
            else
            {
                return _lstSelectedItems;
            }
        }
    }

    public class ClearItemsArgs : EventArgs
    {
        public bool IsIncome { get; set; }        
        public ItemType ClearedItemType { get; set; }
    }

    public class SelectItemArgs : EventArgs
    {       
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
        public string ItemName { get; set; }

        public static implicit operator SelectedStatisticItemInfo(Model.JZItemOne model)
        {
            SelectedStatisticItemInfo res = new SelectedStatisticItemInfo();
            res.ItemID = model.JZItemOneID;
            res.ItemType = ItemType.ItemOne;
            res.IsIncome = model.IncomeOrCost;
            res.ItemName = model.JZItemOneName;
            res.IsIncome = model.IncomeOrCost;
            return res;
        }

        public static SelectedStatisticItemInfo ConvertFromItemTwo(Model.JZItemTwo model, bool isIncome)
        {
            SelectedStatisticItemInfo res = new SelectedStatisticItemInfo();
            res.ItemID = model.JZItemTwoID;
            res.ItemType = ItemType.ItemOne;
            res.IsIncome = isIncome;
            res.ItemName = model.JZItemTwoName;
            return res;
        }
    }
}
