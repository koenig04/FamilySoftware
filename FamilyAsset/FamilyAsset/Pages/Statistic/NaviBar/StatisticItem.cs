using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.Statistic.NaviBar
{
    class StatisticItem : NotificationObject
    {
        private Color _borderColor;

        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                RaisePropertyChanged("BorderColor");
            }
        }



        private string _itemID;
        private bool _isIncome;
    }

    class SelectStatisticItem
    {
        public string ItemID { get; set; }
        public bool IsIncome { get; set; }
    }
}
