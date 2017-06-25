using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model;

namespace BLL.ItemConfigureProcess
{
    class ItemConfigureHandlerItemOne : ItemConfigureHandlerBase
    {
        private JZItemOne _selectedItem = null;
        private bool _isIncome;

        public override void HandleItemSelected(ItemSelectedInfo info)
        {
            if (info.ItemType == ItemType.ItemOne)
            {
                //选择了一级条目，此时需要Load出此一级条目所对应的二级条目和常用语
                _selectedItem = new JZItemOne()
                {
                    JZItemOneID = info.ItemID,
                    JZItemOneName = info.ItemName,
                    IconName = info.ItemIcon
                };

                List<Phrase> lstPhrase;
                List<JZItemTwo> lstItemTwo;
                _itemProcessDal.LoadItemTwoAndPhrase(info.ItemID, out lstItemTwo, out lstPhrase);

                //返回前端进行显示
                Hashtable hstItemTwo = new Hashtable();
                hstItemTwo.Add("Two", lstItemTwo);
                RaiseItemSearchedResultEvent(new ItemSearchedCollectionArgs()
                {
                    ItemType = ItemType.ItemTwo,
                    ItemCollection = hstItemTwo
                });

                Hashtable hstPhrase = new Hashtable();
                hstPhrase.Add("Phrase", lstPhrase);
                RaiseItemSearchedResultEvent(new ItemSearchedCollectionArgs()
                {
                    ItemType = ItemType.Phrase,
                    ItemCollection = hstPhrase
                });
            }
            else if (info.ItemType == ItemType.None)
            {
                _isIncome = info.IsIncome;

                //需要Load所有一级条目
                List<JZItemOne> lstItemOne;
                _itemProcessDal.LoadItemOne(info.IsIncome, out lstItemOne);

                //返回前端显示
                Hashtable hstItemOne = new Hashtable();
                hstItemOne.Add("One", lstItemOne);
                RaiseItemSearchedResultEvent(new ItemSearchedCollectionArgs()
                {
                    ItemType = ItemType.ItemOne,
                    ItemCollection = hstItemOne
                });
            }

            if (_nextHandler != null)
            {
                _nextHandler.HandleItemSelected(info);
            }
        }

        public override void HandleItemValidOperation(ItemConfigureOperationValidInfo info)
        {
            if (info.Itemtype == ItemType.ItemOne && CheckValid(info))
            {
                RaiseItemPopWindowEvent(new ItemPopWindowInfoArgs()
                {
                    ItemType = info.Itemtype,
                    OperationType = info.Optype,
                    ItemOne = _selectedItem
                });
            }
            else
            {
                if (_nextHandler != null)
                {
                    _nextHandler.HandleItemValidOperation(info);
                }
            }
        }

        private bool CheckValid(ItemConfigureOperationValidInfo info)
        {
            bool res = false;
            if (info.Itemtype == ItemType.ItemOne)
            {
                if (((info.Optype == OperationType.Modify || info.Optype == OperationType.Delete) && _selectedItem != null)
                    || info.Optype == OperationType.Add)
                {
                    res = true;
                }
            }
            return res;
        }

        public override void HandleItemOperation(ItemConfigureOperationInfo info)
        {
            if (info.ItemInfo.ItemType == ItemType.ItemOne)
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
            else
            {
                if (_nextHandler != null)
                {
                    _nextHandler.HandleItemOperation(info);
                }
            }
        }

        private void HandleItemAddOperation(ItemConfigureOperationInfo info)
        {
            string itemOneID;
            JZItemOne model = info;
            bool res = _itemProcessDal.AddItemOne(model, out itemOneID);
            model.JZItemOneID = itemOneID;
            RaiseItemChangedEvent(new ItemChangedInfoArgs()
            {
                IsSucceed = res,
                OperationType = OperationType.Add,
                ItemType = ItemType.ItemOne,
                ItemInfo = model
            });
        }

        private void HandleItemModifyOperation(ItemConfigureOperationInfo info)
        {
            bool res = _itemProcessDal.UpdateItemOne(info);
            RaiseItemChangedEvent(new ItemChangedInfoArgs()
            {
                IsSucceed = res,
                OperationType = OperationType.Modify,
                ItemType = ItemType.ItemOne,
                ItemInfo = info
            });
        }

        private void HandleItemDeleteOperation(ItemConfigureOperationInfo info)
        {
            bool res = _itemProcessDal.DelItemOne(info.ItemInfo.ItemID);
            RaiseItemChangedEvent(new ItemChangedInfoArgs()
            {
                IsSucceed = res,
                OperationType = OperationType.Delete,
                ItemType = ItemType.ItemOne,
                ItemInfo = info
            });
        }
    }
}
