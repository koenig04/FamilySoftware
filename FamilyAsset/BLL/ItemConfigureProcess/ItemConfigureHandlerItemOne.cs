using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model;

namespace BLL.ItemConfigureProcess
{
    class ItemConfigureHandlerItemOne : ItemConfigureHandler
    {
        private Model.JZItemOne _selectItem;
        private bool _isSelected;

        public override void HandleItemSelected(ItemSelectedInfo info)
        {
            if (info.ItemType == ItemType.ItemOne)
            {
                _selectItem = new JZItemOne()
                {
                    JZItemOneID = info.ItemID,
                    JZItemOneName = info.ItemName,
                    IconName = info.ItemIcon
                };
            }
            else
            {
                if (_nextHandler != null)
                {
                    _nextHandler.HandleItemSelected(info);
                }
            }
        }

        public override void HandleItemOperation(ItemConfigureOperationInfo info)
        {
            if (info.Itemtype == ItemType.ItemOne)
            {

            }
        }
    }
}
