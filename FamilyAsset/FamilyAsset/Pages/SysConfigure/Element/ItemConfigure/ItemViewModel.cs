using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BLL;
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
                            if (!string.IsNullOrEmpty(this.SelectedItem.ItemIconPressed))
                                this.ItemImg = new BitmapImage(new Uri(Common.GlobalVariables.iconPath.Replace("\\", "/") + this.SelectedItem.ItemIconPressed, UriKind.RelativeOrAbsolute));
                            this.ItemColor = _inOrOut ? Colors.LimeGreen : Colors.Firebrick;
                            this.ItemForeColor = Colors.White;
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

        private Color _itemForeColor;

        public Color ItemForeColor
        {
            get { return _itemForeColor; }
            set
            {
                _itemForeColor = value;
                RaisePropertyChanged("ItemForeColor");
            }
        }

        private bool _inOrOut;

        public ItemViewModel() { }
        public ItemViewModel(Common.ItemType itemType, object itemInfo, bool inOrOut)
        {
            string iconName, iconPressedName, itemID;
            iconName = iconPressedName = itemID = string.Empty;
            switch (itemType)
            {
                case Common.ItemType.ItemOne:
                    ItemName = ((Model.JZItemOne)itemInfo).JZItemOneName;
                    iconName = ((Model.JZItemOne)itemInfo).IconName;
                    iconPressedName = ((Model.JZItemOne)itemInfo).IconNamePressed;
                    itemID = ((Model.JZItemOne)itemInfo).JZItemOneID;
                    break;
                case Common.ItemType.ItemTwo:
                    ItemName = ((Model.JZItemTwo)itemInfo).JZItemTwoName;
                    iconName = ((Model.JZItemTwo)itemInfo).IconName;
                    iconPressedName = ((Model.JZItemTwo)itemInfo).IconNamePressed;
                    itemID = ((Model.JZItemTwo)itemInfo).JZItemTwoID;
                    break;
                case Common.ItemType.Phrase:
                    ItemName = ((Model.Phrase)itemInfo).PhraseContent;
                    itemID = ((Model.Phrase)itemInfo).PhraseID;
                    break;
            }
            this._inOrOut = inOrOut;
            if (!string.IsNullOrEmpty(iconName))
                this.ItemImg = new BitmapImage(new Uri(Common.GlobalVariables.iconPath.Replace("\\", "/") + iconName, UriKind.RelativeOrAbsolute));

            this.ItemColor = Colors.White;
            this.ItemForeColor = _inOrOut ? Colors.LimeGreen : Colors.Firebrick;
            this.SelectedItem = new ItemSelectedModel(itemType, itemID, ItemName, iconName, iconPressedName);
        }

        public void ConvertToUnPressed()
        {
            if (!string.IsNullOrEmpty(SelectedItem.ItemIcon))
                this.ItemImg = new BitmapImage(new Uri(Common.GlobalVariables.iconPath.Replace("\\", "/") + SelectedItem.ItemIcon, UriKind.RelativeOrAbsolute));
            this.ItemColor = Colors.White;
            this.ItemForeColor = _inOrOut ? Colors.LimeGreen : Colors.Firebrick;
        }

        private void RaiseEvent(ItemClickedEventArgs e)
        {
            if (ItemClickedEvent != null)
            {
                ItemClickedEvent(this, e);
            }
        }

        public void ChangeToUnselected()
        {
            this.ItemImg = new BitmapImage(new Uri(Common.GlobalVariables.iconPath.Replace("\\", "/") + this.SelectedItem.ItemIcon, UriKind.RelativeOrAbsolute));
            this._itemColor = Colors.White;
            this._itemForeColor = _inOrOut ? Colors.LimeGreen : Colors.Firebrick;
        }
    }

    class ItemSelectedModel
    {
        public Common.ItemType ItemType { get; private set; }
        public string ItemID { get; private set; }
        public string ItemName { get; private set; }
        public string ItemIcon { get; private set; }
        public string ItemIconPressed { get; set; }

        public ItemSelectedModel(Common.ItemType ItemType, string ItemID, string ItemName, string ItemIcon, string ItemIconPressed)
        {
            this.ItemType = ItemType;
            this.ItemID = ItemID;
            this.ItemName = ItemName;
            this.ItemIcon = ItemIcon;
            this.ItemIconPressed = ItemIconPressed;
        }

        public static implicit operator ItemSelectedInfo(ItemSelectedModel model)
        {
            return new ItemSelectedInfo()
            {
                ItemType = model.ItemType,
                ItemID = model.ItemID,
                ItemName = model.ItemName,
                ItemIcon = model.ItemIcon,
                ItemIconPressed = model.ItemIconPressed
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
