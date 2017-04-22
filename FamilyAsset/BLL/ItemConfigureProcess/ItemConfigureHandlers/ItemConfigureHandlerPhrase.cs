using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model;

namespace BLL.ItemConfigureProcess
{
    class ItemConfigureHandlerPhrase : ItemConfigureHandlerBase
    {
        private Phrase _selectedItem = null;

        public override void HandleItemSelected(ItemSelectedInfo info)
        {
            if (info.ItemType == ItemType.ItemOne || info.ItemType == ItemType.ItemTwo)
            {
                _selectedItem = new Phrase()
                {
                    ItemID = info.ItemID
                };

            }
            else
            {
                if (_selectedItem != null)
                {
                    _selectedItem.PhraseID = info.ItemID;
                    _selectedItem.PhraseContent = info.ItemName;
                }
            }
        }

        public override void HandleItemValidOperation(ItemConfigureOperationValidInfo info)
        {
            if (info.Itemtype == ItemType.Phrase && CheckValid(info))
            {
                RaiseItemPopWindowEvent(new ItemPopWindowInfoArgs()
                {
                    ItemType = info.Itemtype,
                    OperationType = info.Optype,
                    Phrase = _selectedItem
                });
            }
        }

        private bool CheckValid(ItemConfigureOperationValidInfo info)
        {
            bool res = false;
            if (info.Itemtype == ItemType.ItemOne)
            {
                if (((info.Optype == OperationType.Modify || info.Optype == OperationType.Delete) && (_selectedItem != null && !string.IsNullOrEmpty(_selectedItem.PhraseID)))
                    || (info.Optype == OperationType.Add && (_selectedItem != null && !string.IsNullOrEmpty(_selectedItem.ItemID))))
                {
                    res = true;
                }
            }
            return res;
        }

        public override void HandleItemOperation(ItemConfigureOperationInfo info)
        {
            switch (info.OperationType)
            {
                case OperationType.Add:
                    HandleItemAddOperation(info);
                    break;
                case OperationType.Modify:
                    HandleItemModifyOperation(info);
                    break;
                case OperationType.Delete:
                    HandleItemDeleteOperation(info);
                    break;
            }
        }

        private void HandleItemAddOperation(ItemConfigureOperationInfo info)
        {
            string phraseID;
            Phrase model = info;
            if (_itemProcessDal.AddPhrase(model, out phraseID))
            {
                model.PhraseID = phraseID;
                RaiseItemChangedEvent(new ItemChangedInfoArgs()
                {
                    OperationType = OperationType.Add,
                    ItemType = ItemType.Phrase,
                    ItemInfo = model
                });
            }
        }

        private void HandleItemModifyOperation(ItemConfigureOperationInfo info)
        {
            if (_itemProcessDal.UpdatePhrase(info))
            {
                RaiseItemChangedEvent(new ItemChangedInfoArgs()
                {
                    OperationType = OperationType.Modify,
                    ItemType = ItemType.Phrase,
                    ItemInfo = info
                });
            }
        }

        private void HandleItemDeleteOperation(ItemConfigureOperationInfo info)
        {
            if (_itemProcessDal.DelPhrase(info.ItemInfo.ItemID))
            {
                RaiseItemChangedEvent(new ItemChangedInfoArgs()
                {
                    OperationType = OperationType.Delete,
                    ItemType = ItemType.Phrase,
                    ItemInfo = info
                });
            }
        }
    }
}
