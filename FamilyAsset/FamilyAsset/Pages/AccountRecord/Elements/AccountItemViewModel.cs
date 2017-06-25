using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Common;
using FamilyAsset.Pages.SysConfigure.Element.ItemConfigure;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.AccountRecord.Elements
{
    class AccountItemViewModel : NotificationObject
    {
        public event EventHandler<ItemClickedEventArgs> ItemClickedEvent;
        public ItemSelectedModel SelectedItem { get; private set; }

        private BitmapImage _ItemImg;

        public BitmapImage ItemImg
        {
            get
            {
                return _ItemImg;
            }
            set
            {
                _ItemImg = value;
                RaisePropertyChanged("ItemImg");
            }
        }

        private string _ItemName;

        public string ItemName
        {
            get { return _ItemName; }
            set
            {
                _ItemName = value;
                RaisePropertyChanged("ItemName");
            }
        }

        private DelegateCommand _itemClicked;

        public DelegateCommand ItemClicked
        {
            get
            {
                if (_itemClicked == null)
                {
                    _itemClicked = new DelegateCommand(new Action<object>(
                        o =>
                        {
                            new BitmapImage(new Uri(Common.GlobalVariables.iconPath.Replace("\\", "/") + SelectedItem.ItemIconPressed, UriKind.RelativeOrAbsolute));
                            RaiseEvent(new ItemClickedEventArgs(this.SelectedItem));
                        }));
                }
                return _itemClicked;
            }
            set
            {
                _itemClicked = value;
                RaisePropertyChanged("ItemClicked");
            }
        }

        public AccountItemViewModel(ItemType itemType, object itemModel)
        {
            switch (itemType)
            {
                case ItemType.ItemOne:
                    SelectedItem = new ItemSelectedModel(itemType, ((Model.JZItemOne)itemModel).JZItemOneID,
                        ((Model.JZItemOne)itemModel).JZItemOneName, ((Model.JZItemOne)itemModel).IconName, ((Model.JZItemOne)itemModel).IconNamePressed);
                    break;
                case ItemType.ItemTwo:
                    SelectedItem = new ItemSelectedModel(itemType, ((Model.JZItemTwo)itemModel).JZItemTwoID,
                        ((Model.JZItemTwo)itemModel).JZItemTwoName, ((Model.JZItemTwo)itemModel).IconName, ((Model.JZItemTwo)itemModel).IconNamePressed);
                    break;
                case ItemType.Phrase:
                    SelectedItem = new ItemSelectedModel(itemType, ((Model.Phrase)itemModel).PhraseID,
                        ((Model.Phrase)itemModel).PhraseContent, null, null);
                    break;
            }
            this.ItemImg = new BitmapImage(new Uri(Common.GlobalVariables.iconPath.Replace("\\", "/") + SelectedItem.ItemIcon, UriKind.RelativeOrAbsolute));
        }

        private void RaiseEvent(ItemClickedEventArgs e)
        {
            if (ItemClickedEvent != null)
            {
                ItemClickedEvent(this, e);
            }
        }
    }
}
