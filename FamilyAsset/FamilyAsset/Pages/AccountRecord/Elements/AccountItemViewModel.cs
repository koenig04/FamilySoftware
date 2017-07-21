using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Common;
using FamilyAsset.Pages.SysConfigure.Element.ItemConfigure;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.AccountRecord.Elements
{
    class AccountItemViewModel : ItemViewModel
    {
        public AccountItemViewModel(ItemType itemType, object itemInfo, bool inOrOut)
            : base(itemType, itemInfo, inOrOut)
        {
            if (inOrOut && itemType == ItemType.ItemTwo)
            {
                ItemForeColor = Colors.Lime;
            }
            else if (!inOrOut && itemType == ItemType.ItemTwo)
            {
                ItemForeColor = Colors.Tomato;
            }
        }

        public override void ConvertToUnPressed()
        {
            base.ConvertToUnPressed();

            if (_inOrOut && SelectedItem.ItemType == ItemType.ItemTwo)
            {
                ItemForeColor = Colors.Lime;
            }
            else if (!_inOrOut && SelectedItem.ItemType == ItemType.ItemTwo)
            {
                ItemForeColor = Colors.Tomato;
            }
        }

        public override DelegateCommand ItemClicked
        {
            get
            {
                if (base.ItemClicked == null)
                {
                    base.ItemClicked = new DelegateCommand(new Action<object>(
                        o =>
                        {
                            if (!string.IsNullOrEmpty(this.SelectedItem.ItemIconPressed))
                                this.ItemImg = new BitmapImage(new Uri(Common.GlobalVariables.iconPath.Replace("\\", "/") + this.SelectedItem.ItemIconPressed, UriKind.RelativeOrAbsolute));
                            if (_inOrOut)
                            {
                                if (SelectedItem.ItemType == ItemType.ItemOne)
                                {
                                    ItemColor = Colors.LimeGreen;
                                }
                                else
                                {
                                    ItemColor = Colors.Lime;
                                }
                            }
                            else
                            {
                                if (SelectedItem.ItemType == ItemType.ItemOne)
                                {
                                    ItemColor = Colors.Firebrick;
                                }
                                else
                                {
                                    ItemColor = Colors.Tomato;
                                }
                            }                            
                            this.ItemForeColor = Colors.White;
                            RaiseEvent(new ItemClickedEventArgs(this.SelectedItem));
                        }));
                }
                return base.ItemClicked;
            }
            set
            {
                base.ItemClicked = value;
            }
        }
    }
}
