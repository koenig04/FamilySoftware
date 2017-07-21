using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FamilyAsset.UICore;

namespace FamilyAsset.PopupWindow
{
    class PopWindowItemViewModel : NotificationObject
    {
        private string _itemName;

        public string ItemName
        {
            get { return _itemName; }
            set
            {
                _itemName = value;
                RaisePropertyChanged("ItemName");
            }
        }

        private string _itemValue;

        public string ItemValue
        {
            get { return _itemValue; }
            set
            {
                _itemValue = value;
                RaisePropertyChanged("ItemValue");
            }
        }

        private Visibility _itemVisibility;

        public Visibility ItemVisibility
        {
            get { return _itemVisibility; }
            set
            {
                _itemVisibility = value;
                RaisePropertyChanged("ItemVisibility");
            }
        }

        private bool _isEditable;

        public bool IsEditable
        {
            get { return _isEditable; }
            set
            {
                _isEditable = value;
                RaisePropertyChanged("IsEditable");
            }
        }
    }
}
