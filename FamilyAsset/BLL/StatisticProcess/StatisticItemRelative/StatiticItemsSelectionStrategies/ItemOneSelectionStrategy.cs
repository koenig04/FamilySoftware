using System.Collections.Generic;
using System.Linq;
using BLL.ItemConfigureProcess;

namespace BLL.StatisticProcess.StatisticItemRelative.StatiticItemsSelectionStrategies
{
    class ItemOneSelectionStrategy : StatisticItemSelectionBase
    {
        private List<SelectedStatisticItemInfo> _lstSelectedItemOnes = new List<SelectedStatisticItemInfo>();

        public ItemOneSelectionStrategy(IItemConfigureProcess itemProcess) : base(itemProcess) { }

        public override void ProceedSelectedItem(SelectedStatisticItemInfo selectedItem)
        {
            if (selectedItem.IsSelected)
            {
                if (_lstSelectedItemOnes.Count == 0)//itemone is never selected, so the itemtwobelongs to this must be shown.
                {
                    _itemProcess.HandleItemSelected(new Model.JZItemOne()
                    {
                        IncomeOrCost = selectedItem.IsIncome,
                        JZItemOneID = selectedItem.ItemID
                    });
                }
                else
                {
                    //more than one itemone is selected. 
                    //Firstly we must remove all the itemone and itemtwo in different income/cost sort
                    //and itemtwo in the same income/cost sort.
                    //Then we remove all the itemone in different income/cost sort in this class.
                    RaiseClearItemsEvent(new ClearItemsArgs()
                    {
                        IsIncome = selectedItem.IsIncome,
                        ClearedItemType = Common.ItemType.ItemTwo
                    });
                    RaiseClearItemsEvent(new ClearItemsArgs()
                    {
                        IsIncome = !selectedItem.IsIncome,
                        ClearedItemType = Common.ItemType.ItemTwo
                    });
                    RaiseClearItemsEvent(new ClearItemsArgs()
                    {
                        IsIncome = !selectedItem.IsIncome,
                        ClearedItemType = Common.ItemType.ItemOne
                    });

                    if (_lstSelectedItemOnes[0].IsIncome != selectedItem.IsIncome)
                    {//if we select another item one in different income/cost sort, we must load the itemtwo which belong to it.
                        _itemProcess.HandleItemSelected(new Model.JZItemOne()
                        {
                            IncomeOrCost = selectedItem.IsIncome,
                            JZItemOneID = selectedItem.ItemID
                        });
                    }
                    _lstSelectedItemOnes = _lstSelectedItemOnes.Where(a => a.IsIncome == selectedItem.IsIncome).ToList();
                }
                _lstSelectedItemOnes.Add(selectedItem);
                RaiseClearItemsEvent(new ClearItemsArgs()
                {
                    ClearedItemType = Common.ItemType.None,
                    IsIncome = false
                });
                RaiseClearItemsEvent(new ClearItemsArgs()
                {
                    ClearedItemType = Common.ItemType.None,
                    IsIncome = true
                });
            }
            else
            {
                if (_lstSelectedItemOnes.Count == 1)
                {
                    RaiseClearItemsEvent(new ClearItemsArgs()
                    {
                        IsIncome = selectedItem.IsIncome,
                        ClearedItemType = Common.ItemType.ItemTwo
                    });
                }
                else if (_lstSelectedItemOnes.Count == 2)
                {
                    _itemProcess.HandleItemSelected(new Model.JZItemOne()
                    {
                        IncomeOrCost = selectedItem.IsIncome,
                        JZItemOneID = _lstSelectedItemOnes.Where(a => a.ItemID != selectedItem.ItemID).First().ItemID
                    });
                }
                _lstSelectedItemOnes.RemoveAll(a => a.ItemID == selectedItem.ItemID);
            }
        }
    }
}
