using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.StatisticProcess.StatisticItemRelative;
using Common;

namespace FamilyAsset.Pages.Statistic.StatisticItems
{
    class ItemCollectionController
    {
        public event EventHandler<StatisticItemsListOperationEventArgs> StatisticItemsListOperation;
        public event EventHandler<SelectStatisticItemEventArgs> StatisticItemSelected;

        private ObservableCollection<StatisticItemViewModel> _itemCollection;
        private bool _isIncome;
        private ItemType _itemType;

        public ItemCollectionController(ObservableCollection<StatisticItemViewModel> collection, bool isIncome, ItemType itemType)
        {
            this._itemCollection = collection;
            this._isIncome = isIncome;
            this._itemType = itemType;
        }

        public ItemCollectionController(StatisticItemViewModel model, bool isIncome, ItemType itemType)
        {
            this._itemCollection = new ObservableCollection<StatisticItemViewModel>() { model };
            this._isIncome = isIncome;
            this._itemType = itemType;
        }

        public void AddItems(ItemCollectionOperationArgs e)
        {
            _itemCollection.Clear();

            foreach (StatisticItemViewModel item in
                e.ItemOneCollection != null ? e.ItemOneCollection : e.ItemTwoCollection)
            {
                _itemCollection.Add(item);
                item.StatisticItemIsSelected += OnStatisticItemIsSelected;
            }

        }

        private void OnStatisticItemIsSelected(object sender, SelectStatisticItemEventArgs e)
        {
            if (StatisticItemSelected != null)
            {
                StatisticItemSelected(null, e);
            }
        }

        public void ClearItems(ClearItemsArgs e)
        {
            if (e.ConvertToUnselected)
            {
                foreach (StatisticItemViewModel item in _itemCollection)
                {
                    item.SwitchSelectionStatus(false);
                }
            }
            else if (e.OnlyClearFromList)
            {
                RaiseStatisticItemsListOperation(
                    new StatisticItemsListOperationEventArgs(StatisticItemsListOperationType.Remove,
                        null,
                        _isIncome,
                        _itemType));
            }
            else
            {
                _itemCollection.Clear();
                RaiseStatisticItemsListOperation(
                    new StatisticItemsListOperationEventArgs(StatisticItemsListOperationType.Remove,
                        null,
                        _isIncome,
                        _itemType));
            }
        }        

        private void RaiseStatisticItemsListOperation(StatisticItemsListOperationEventArgs args)
        {
            if (StatisticItemsListOperation != null)
            {
                StatisticItemsListOperation(null, args);
            }
        }        
    }

    enum StatisticItemsListOperationType
    {
        Add,
        Remove
    }

    class StatisticItemsListOperationEventArgs : EventArgs
    {
        public StatisticItemsListOperationType OperationType { get; set; }
        public List<SelectStatisticItemEventArgs> OperationItems { get; set; }
        public bool IsIncome { get; set; }
        public ItemType StatisticItemType { get; set; }

        public StatisticItemsListOperationEventArgs() { }
        public StatisticItemsListOperationEventArgs(StatisticItemsListOperationType operationType,
            List<SelectStatisticItemEventArgs> operationItems, bool isIncome = false, ItemType statisticItemType = ItemType.None)
        {
            this.OperationItems = operationItems;
            this.OperationType = operationType;
            this.IsIncome = isIncome;
            this.StatisticItemType = statisticItemType;
        }
    }
}
