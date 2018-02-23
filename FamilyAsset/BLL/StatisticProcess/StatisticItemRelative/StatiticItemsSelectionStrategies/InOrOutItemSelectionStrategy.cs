using BLL.ItemConfigureProcess;

namespace BLL.StatisticProcess.StatisticItemRelative.StatiticItemsSelectionStrategies
{
    class InOrOutItemSelectionStrategy : StatisticItemSelectionBase
    {
        public InOrOutItemSelectionStrategy(IItemConfigureProcess itemProcess) : base(itemProcess) { }

        public override void ProceedSelectedItem(SelectedStatisticItemInfo selectedItem)
        {
            if (selectedItem.IsSelected)//when allincome or allcost is selected, all the itemone and itemtwo must be removed.
            {
                RaiseClearItemsEvent(new ClearItemsArgs()
                {
                    ClearedItemType = Common.ItemType.ItemOne,
                    IsIncome = true
                });
                RaiseClearItemsEvent(new ClearItemsArgs()
                {
                    ClearedItemType = Common.ItemType.ItemTwo,
                    IsIncome = true
                });
                RaiseClearItemsEvent(new ClearItemsArgs()
                {
                    ClearedItemType = Common.ItemType.ItemOne,
                    IsIncome = false
                });
                RaiseClearItemsEvent(new ClearItemsArgs()
                {
                    ClearedItemType = Common.ItemType.ItemTwo,
                    IsIncome = false
                });
            }            
        }
    }
}
