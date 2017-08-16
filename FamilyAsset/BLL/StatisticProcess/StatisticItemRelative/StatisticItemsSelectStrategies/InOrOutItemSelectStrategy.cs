using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.StatisticProcess.StatisticItemRelative;
using Common;

namespace BLL.StatisticProcess.StatisticItemRelative.StatisticItemsSelectStrategies
{
    class InOrOutItemSelectStrategy : StatisticItemSelectBase
    {
        private bool _isAllIncomeSelected, _isAllCostSelected;

        public InOrOutItemSelectStrategy(BLL.ItemConfigureProcess.IItemConfigureProcess itemProcess)
            : base(itemProcess)
        {

        }

        public override void ProceedSelectedItem(SelectedStatisticItemInfo selectedItem)
        {
            if (string.IsNullOrEmpty(selectedItem.ItemID))//代表按下的是AllIncome/AllCost
            {
                if (selectedItem.IsSelected)
                {
                    if (selectedItem.IsIncome)
                        _isAllIncomeSelected = true;
                    else
                        _isAllCostSelected = true;
                    RaiseClearItemsEvent(new ClearItemsArgs()
                    {
                        ClearedItemType = ItemType.ItemOne,
                        ConvertToUnselected = true,
                        IsIncome = selectedItem.IsIncome ? false : true
                    });//将另一种的ItemOne全置为未选
                    RaiseClearItemsEvent(new ClearItemsArgs()
                    {
                        ClearedItemType = ItemType.ItemTwo,
                        IsIncome = selectedItem.IsIncome ? false : true
                    });//将另一种的ItemTwo全部清除                    
                }
                else
                {
                    if (selectedItem.IsIncome)
                        _isAllIncomeSelected = false;
                    else
                        _isAllCostSelected = false;
                }
                RaiseItemSelectedEvent(new SelectItemArgs()
                {
                    AddToList = true,
                    ItemInfo = selectedItem
                });
            }
            else if (selectedItem.ItemType == ItemType.ItemOne)
            {
                if (((selectedItem.IsIncome && !_isAllIncomeSelected) || (!selectedItem.IsIncome && !_isAllCostSelected))
                    && selectedItem.IsSelected)
                {
                    RaiseItemSelectedEvent(new SelectItemArgs()
                    {
                        ConverToSelected = true,
                        ItemInfo = selectedItem
                    });
                }
            }
            if (_nextHandler != null)
            {
                _nextHandler.ProceedSelectedItem(selectedItem);
            }

        }

        public override void OnReactive(object sender, EventArgs e)
        {
            RaiseItemSelectedEvent(new SelectItemArgs()
            {
                AddToList = true,
                ItemInfo = new SelectedStatisticItemInfo()
                {
                    IsIncome = _isAllIncomeSelected ? true : false
                }
            });
        }
    }
}
