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

            InOrOutItemSelectStrategy inOrOut = new InOrOutItemSelectStrategy(_itemProcess);
            ItemOneSelectStrategy itemOne = new ItemOneSelectStrategy(_itemProcess);
            ItemTwoSelectStrategy itemTwo = new ItemTwoSelectStrategy(_itemProcess);

            inOrOut.ItemSelectedEvent += OnItemSelected;
            itemOne.ItemSelectedEvent += OnItemSelected;
            itemTwo.ItemSelectedEvent += OnItemSelected;

            inOrOut.ClearItemsEvent += OnClearItems;
            itemOne.ClearItemsEvent += OnClearItems;
            itemTwo.ClearItemsEvent += OnClearItems;

            itemOne.SetNextHandler(itemTwo);
            itemTwo.ReactiveEvent += itemOne.OnReactive;

            inOrOut.SetNextHandler(itemOne);
            itemOne.ReactiveEvent += inOrOut.OnReactive;

            _statisticItemProcess = inOrOut;
        }

        private void OnClearItems(object sender, ClearItemsArgs e)
        {
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
                    break;
                case ItemType.ItemTwo:
                    List<Model.JZItemTwo> lstTwos = e.ItemCollection["Two"] as List<Model.JZItemTwo>;
                    res.ItemOneCollection = new List<SelectedStatisticItemInfo>();
                    foreach (Model.JZItemTwo item in lstTwos)
                    {
                        res.ItemOneCollection.Add(SelectedStatisticItemInfo.ConvertFromItemTwo(item, _currentInOrOutForItemTwo));
                    }
                    break;
            }
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
            if (selectedItem.ItemType == ItemType.ItemOne)
            {
                _currentInOrOutForItemTwo = selectedItem.IsIncome;
            }
        }
    }

    public class ClearItemsArgs : EventArgs
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

        public static SelectedStatisticItemInfo ConvertFromItemTwo(Model.JZItemTwo model, bool isIncome)
        {
            SelectedStatisticItemInfo res = new SelectedStatisticItemInfo();
            res.ItemID = model.JZItemTwoID;
            res.ItemType = ItemType.ItemOne;
            res.IsIncome = isIncome;
            return res;
        }
    }
}
