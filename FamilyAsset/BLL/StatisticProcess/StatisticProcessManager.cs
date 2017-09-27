﻿using System;
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
            _diagramProcess.SearchDiagramData(info);
        }
        
    }
}
