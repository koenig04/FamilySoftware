using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.StatisticProcess.StatisticItemRelative;

namespace BLL.StatisticProcess
{
    public interface IStatiticProcess
    {
        //Diagram Relative
        void GetStatisticInfo(StaticSearchInfo info);
        event EventHandler<StatisticByTime> StatisticByTimeCallbackEvent;
        event EventHandler<StatisticBySort> StatisticBySortCallbackEvent;

        //Statistic Item Relative
        void ProceedSelectedItem(SelectedStatisticItemInfo selectedItem);
        void InitializeItemOnes();
        event EventHandler<ClearItemsArgs> ItemCollectionClearEvent;
        event EventHandler<SelectItemArgs> ItemSelectEvent;
        event EventHandler<ItemCollectionOperationArgs> ItemCollectionAddEvent;
    }
}
