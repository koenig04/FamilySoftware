using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.StatisticProcess.DiagramRelative;
using BLL.StatisticProcess.StatisticItemRelative;

namespace BLL.StatisticProcess
{
    public class StatisticProcessManager : IStatiticProcess
    {
        public event EventHandler<DiagramData> DiagramDataDisplayEvent;

        public event EventHandler<StatisticItemRelative.ClearItemsArgs> ItemCollectionClearEvent;
        public event EventHandler<StatisticItemRelative.SelectItemArgs> ItemSelectEvent;
        public event EventHandler<StatisticItemRelative.ItemCollectionOperationArgs> ItemCollectionAddEvent;

        private DiagramProcessController _diagramProcess;
        private StatisticItemCotroller _statisticItemProcess;

        public StatisticProcessManager()
        {
            _diagramProcess = new DiagramProcessController();
            _statisticItemProcess = new StatisticItemCotroller();

            _diagramProcess.DiagramDataDisplayEvent += OnDiagramDataDisplay;
            _statisticItemProcess.ItemCollectionAddEvent += OnItemCollectionAddEvent;
            _statisticItemProcess.ItemCollectionClearEvent += OnItemCollectionClearEvent;
            _statisticItemProcess.ItemSelectEvent += OnItemSelectEvent;
        }

        private void OnDiagramDataDisplay(object sender, DiagramData e)
        {
            if (DiagramDataDisplayEvent != null)
            {
                DiagramDataDisplayEvent(sender, e);
            }
        }

        private void OnItemSelectEvent(object sender, SelectItemArgs e)
        {
            if (ItemSelectEvent != null)
            {
                ItemSelectEvent(null, e);
            }
        }

        private void OnItemCollectionClearEvent(object sender, ClearItemsArgs e)
        {
            if (ItemCollectionClearEvent != null)
            {
                ItemCollectionClearEvent(null, e);
            }
        }

        private void OnItemCollectionAddEvent(object sender, ItemCollectionOperationArgs e)
        {
            if (ItemCollectionAddEvent != null)
            {
                ItemCollectionAddEvent(null, e);
            }
        }

        public void InitializeItemOnes()
        {
            _statisticItemProcess.InitializeItemOnes();
        }

        public void ProceedSelectedItem(StatisticItemRelative.SelectedStatisticItemInfo selectedItem)
        {
            _statisticItemProcess.ProceedSelectedItem(selectedItem);
        }

        public void SearchDiagramData(StaticSearchInfo info)
        {
            List<SelectedStatisticItemInfo> itemList = _statisticItemProcess.GetStatisticItems();
            if (itemList.Count > 0)
            {
                if (itemList.Count == 2 && itemList[0].ItemType == Common.ItemType.None && itemList[1].ItemType == Common.ItemType.None)
                {
                    info.InOrOutFlag = 2;
                }
                else
                {
                    info.InOrOutFlag = itemList[0].IsIncome ? 1 : 0;
                }
                switch (itemList[0].ItemType)
                {
                    case Common.ItemType.ItemOne:
                        info.ItemOneIDs = (from d in itemList
                                           select d.ItemID).ToList<string>();
                        break;
                    case Common.ItemType.ItemTwo:
                        info.ItemTwoIDs = (from d in itemList
                                           select d.ItemID).ToList<string>();
                        break;
                }
                _diagramProcess.SearchDiagramData(info);
            }
        }

    }
}
