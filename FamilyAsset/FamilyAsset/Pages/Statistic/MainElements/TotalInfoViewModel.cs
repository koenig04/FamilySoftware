using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.Statistic.MainElements
{
    class TotalInfoViewModel : NotificationObject
    {
        private string _itemTitle;

        public string ItemTitle
        {
            get { return _itemTitle; }
            set
            {
                _itemTitle = value;
                RaisePropertyChanged("ItemTitle");
            }
        }

        private string _itemAmount;

        public string ItemAmount
        {
            get { return _itemAmount; }
            set
            {
                _itemAmount = value;
                RaisePropertyChanged("ItemAmount");
            }
        }

        private Color _itemColor;

        public Color ItemColor
        {
            get { return _itemColor; }
            set
            {
                _itemColor = value;
                RaisePropertyChanged("ItemColor");
            }
        }
    }
}
