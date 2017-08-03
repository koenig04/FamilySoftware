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
        public event EventHandler<StatisticByTime> StatisticByTimeCallbackEvent;
        public event EventHandler<StatisticBySort> StatisticBySortCallbackEvent;

        public event EventHandler<StatisticItemRelative.ClearItemsArgs> ItemCollectionClearEvent;
        public event EventHandler<StatisticItemRelative.SelectItemArgs> ItemSelectEvent;
        public event EventHandler<StatisticItemRelative.ItemCollectionOperationArgs> ItemCollectionAddEvent;

        private DiagramProcessController _diagramProcess;
        private StatisticItemCotroller _statisticItemProcess;

        public StatisticProcessManager()
        {
            _diagramProcess = new DiagramProcessController();
            _statisticItemProcess = new StatisticItemCotroller();

            _diagramProcess.StatisticByTimeCallbackEvent += OnStatisticByTimeCallbackEvent;
            _diagramProcess.StatisticBySortCallbackEvent += OnStatisticBySortCallbackEvent;
            _statisticItemProcess.ItemCollectionAddEvent += OnItemCollectionAddEvent;
            _statisticItemProcess.ItemCollectionClearEvent += OnItemCollectionClearEvent;
            _statisticItemProcess.ItemSelectEvent += OnItemSelectEvent;
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

        public void GetStatisticInfo(StaticSearchInfo info)
        {
            _diagramProcess.GetStatisticInfo(info);
        }

        private void OnStatisticByTimeCallbackEvent(object sender, StatisticByTime e)
        {
            if (StatisticByTimeCallbackEvent != null)
            {
                StatisticByTimeCallbackEvent(sender, e);
            }
        }

        private void OnStatisticBySortCallbackEvent(object sender, StatisticBySort e)
        {
            if (StatisticBySortCallbackEvent != null)
            {
                StatisticBySortCallbackEvent(sender, e);
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
    }
}
