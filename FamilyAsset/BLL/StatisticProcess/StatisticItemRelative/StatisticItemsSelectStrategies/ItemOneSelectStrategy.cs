﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.StatisticProcess.StatisticItemRelative;
using Common;

namespace BLL.StatisticProcess.StatisticItemRelative.StatisticItemsSelectStrategies
{
    class ItemOneSelectStrategy : StatisticItemSelectBase
    {
        private List<SelectedStatisticItemInfo> _lstSelectedItemOnes = new List<SelectedStatisticItemInfo>();

        public override void ProceedSelectedItem(SelectedStatisticItemInfo selectedItem)
        {
            if (selectedItem.ItemType == ItemType.ItemOne)
            {
                if (selectedItem.IsSelected)
                {
                    DealSelectionOperation(selectedItem);
                }
                else
                {
                    DealUnselectionOperation(selectedItem);
                }
            }
        }

        private void DealSelectionOperation(SelectedStatisticItemInfo selectedItem)
        {
            if (_lstSelectedItemOnes.Count > 0)
            {
                if (selectedItem.IsIncome == _lstSelectedItemOnes[0].IsIncome)//同类的一级条目被选中
                {
                    if (_lstSelectedItemOnes.Count == 1)
                    {
                        RaiseClearItemsEvent(new ClearItemsArgs()
                        {
                            ClearedItemType = ItemType.ItemTwo,
                            IsIncome = selectedItem.IsIncome
                        });
                    }
                    _lstSelectedItemOnes.Add(selectedItem);
                    RaiseItemSelectedEvent(new SelectItemArgs()
                    {
                        ItemInfo = selectedItem
                    });
                }
                else
                {
                    RaiseClearItemsEvent(new ClearItemsArgs()
                    {
                        ClearedItemType = ItemType.ItemTwo,
                        IsIncome = !selectedItem.IsIncome
                    });//清除不同收支的二级条目
                    RaiseClearItemsEvent(new ClearItemsArgs()
                    {
                        ClearedItemType = ItemType.ItemOne,
                        IsIncome = !selectedItem.IsIncome,
                        ConvertToUnselected = true
                    });//清除不同收支的一级条目
                    _lstSelectedItemOnes.Clear();//清除内部list
                    _lstSelectedItemOnes.Add(selectedItem);//将选中的条目添加到内部list
                    RaiseItemSelectedEvent(new SelectItemArgs()
                    {
                        ItemInfo = selectedItem
                    });//通知前端选中
                    _itemProcess.HandleItemSelected(new Model.JZItemOne()
                    {
                        IncomeOrCost = selectedItem.IsIncome,
                        JZItemOneID = selectedItem.ItemID
                    });
                }
            }
        }

        private void DealUnselectionOperation(SelectedStatisticItemInfo selectedItem)
        {
            if (_lstSelectedItemOnes.Count > 1)
            {

            }
        }

    }
}
