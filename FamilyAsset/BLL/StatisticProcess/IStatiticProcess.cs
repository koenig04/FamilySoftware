using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.StatisticProcess.DiagramRelative;
using BLL.StatisticProcess.StatisticItemRelative;

namespace BLL.StatisticProcess
{
    public interface IStatiticProcess
    {
        //Diagram Relative
        void SearchDiagramData(StaticSearchInfo info);
        event EventHandler<DiagramData> DiagramDataDisplayEvent;

        //Statistic Item Relative
        void ProceedSelectedItem(SelectedStatisticItemInfo selectedItem);
        void InitializeItemOnes();
        event EventHandler<ClearItemsArgs> ItemCollectionClearEvent;
        event EventHandler<SelectItemArgs> ItemSelectEvent;
        event EventHandler<ItemCollectionOperationArgs> ItemCollectionAddEvent;
    }
}
