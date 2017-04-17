using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyAsset.Pages.SysConfigure.Element.ItemConfigure.ItemConfigureProcess
{
    class ItemConfigureHandlerItemOne : ItemConfigureHandler
    {
        private Model.JZItemOne _selectItem;
        private bool _isSelected;

        public override void OnItemSelected(ItemClickedEventArgs e)
        {
            if (e.SelectedItem.ItemType == Common.ItemType.ItemOne)
            {
                _selectItem = new Model.JZItemOne()
                {
                    JZItemOneID = e.SelectedItem.ItemID,
                    JZItemOneName = e.SelectedItem.ItemName,
                    IconName = e.SelectedItem.ItemIcon
                };
            }
            else
            {
                if (_nextHandler != null)
                {
                    _nextHandler.OnItemSelected(e);
                }
            }
        }

        public override void OnButtonsPressed(ItemConfigureButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
