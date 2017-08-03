using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace FamilyAsset.Pages.Statistic.NaviBar
{
    class StatisticItemsFilter
    {
        public event EventHandler<List<SelectStatisticItemEventArgs>> SelectedStatisticItemsEvent;
        public event EventHandler<ClearItemTwosEventArgs> ClearItemTwosEvent;

        private List<SelectStatisticItemEventArgs> _lstSelectedStatisticItems;

        public StatisticItemsFilter()
        {
            _lstSelectedStatisticItems = new List<SelectStatisticItemEventArgs>();
        }

        public void ItemSelected(SelectStatisticItemEventArgs itemInfo)
        {
            
        }

        private void ClearItemTwos(bool isIncome, bool needToClear = false)
        {
            while (_lstSelectedStatisticItems.Where(a => a.IsIncome == isIncome && a.ItemType == ItemType.ItemTwo).FirstOrDefault() != null)
            {
                _lstSelectedStatisticItems.Remove(
                   _lstSelectedStatisticItems.Where(a => a.IsIncome == isIncome && a.ItemType == ItemType.ItemTwo).First());
            }
            RaiseClearItemTwosEvent(new ClearItemTwosEventArgs() { IsIncome = isIncome, NeedToClear = needToClear });
            RaiseSelectedStatisticItemsEvent(_lstSelectedStatisticItems);
        }

        private void RaiseSelectedStatisticItemsEvent(List<SelectStatisticItemEventArgs> e)
        {
            if (SelectedStatisticItemsEvent != null)
            {
                SelectedStatisticItemsEvent(null, e);
            }
        }

        private void RaiseClearItemTwosEvent(ClearItemTwosEventArgs isIncome)
        {
            if (ClearItemTwosEvent != null)
            {
                ClearItemTwosEvent(null, isIncome);
            }
        }
    }

    class ClearItemTwosEventArgs : EventArgs
    {
        public bool IsIncome { get; set; }
        public bool NeedToClear { get; set; }
    }
}
