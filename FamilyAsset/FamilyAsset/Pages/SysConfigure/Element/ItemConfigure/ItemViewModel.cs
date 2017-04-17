using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BLL.ItemConfigureProcess;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.SysConfigure.Element.ItemConfigure
{
    class ItemViewModel : NotificationObject
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


        public ItemViewModel() { }
        public ItemViewModel(Common.ItemType ItemType, string ItemID, string IconName, string ItemName)
        {
            this.ItemImg = new BitmapImage(new Uri(Common.GlobalVariables.iconPath.Replace("\\", "/") + IconName, UriKind.RelativeOrAbsolute));
            this.ItemName = ItemName;
            this.SelectedItem = new ItemSelectedModel(ItemType, ItemID, ItemName, IconName);
        }

        private void RaiseEvent(ItemClickedEventArgs e)
        {
            if (ItemClickedEvent != null)
            {
                ItemClickedEvent(this, e);
            }
        }
    }

    class ItemSelectedModel
    {
        public Common.ItemType ItemType { get; private set; }
        public string ItemID { get; private set; }
        public string ItemName { get; private set; }
        public string ItemIcon { get; private set; }

        public ItemSelectedModel(Common.ItemType ItemType, string ItemID, string ItemMame, string ItemIcon)
        {
            this.ItemType = ItemType;
            this.ItemID = ItemID;
            this.ItemName = ItemName;
            this.ItemIcon = ItemIcon;
        }

        public static implicit operator ItemSelectedInfo(ItemSelectedModel model)
        {
            return new ItemSelectedInfo()
            {
                ItemType = model.ItemType,
                ItemID = model.ItemID,
                ItemName = model.ItemName,
                ItemIcon = model.ItemIcon
            };
        }
    }

    class ItemClickedEventArgs : EventArgs
    {
        public ItemSelectedModel SelectedItem { get; private set; }

        public ItemClickedEventArgs(ItemSelectedModel model)
        {
            this.SelectedItem = model;
        }
    }
}
