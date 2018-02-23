using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ItemConfigureProcess;

namespace BLL.StatisticProcess.StatisticItemRelative.StatiticItemsSelectionStrategies
{
    class ItemTwoSelectionStrategy : StatisticItemSelectionBase
    {
        public ItemTwoSelectionStrategy(IItemConfigureProcess itemProcess) : base(itemProcess) { }

        public override void ProceedSelectedItem(SelectedStatisticItemInfo selectedItem)
        {
           
        }
    }
}
