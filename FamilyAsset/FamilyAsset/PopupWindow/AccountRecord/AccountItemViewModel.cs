using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyAsset.UICore;

namespace FamilyAsset.PopupWindow.AccountRecord
{
    class AccountItemViewModel : NotificationObject
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

        private string _itemContent;

        public string ItemContent
        {
            get { return _itemContent; }
            set
            {
                _itemContent = value;
                RaisePropertyChanged("ItemContent");
            }
        }

        public AccountItemViewModel(string itemTitle, string itemContent)
        {
            ItemTitle = itemTitle;
            ItemContent = itemContent;
        }
    }
}
