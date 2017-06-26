using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model;

namespace BLL.ItemConfigureProcess
{
    class ItemConfigureHandlerItemTwo : ItemConfigureHandlerBase
    {
        private JZItemTwo _selectedItem = null;

        public override void HandleItemSelected(ItemSelectedInfo info)
        {
            if (info.ItemType == ItemType.ItemOne)//一级条目被选择，二级条目需要刷新,记录下被选中的一级条目ID
            {
                _selectedItem = new JZItemTwo()
                {
                    JZItemOneID = info.ItemID
                };
            }
            else if (info.ItemType == ItemType.ItemTwo)//二级条目被选择，需要Load出他所属的常用语
            {
                if (_selectedItem != null)
                {
                    _selectedItem.JZItemTwoID = info.ItemID;
                    _selectedItem.JZItemTwoName = info.ItemName;
                    _selectedItem.IconName = info.ItemIcon;
                }

                List<Phrase> lstPhrase;
                _itemProcessDal.LoadPhrase(info.ItemID, out lstPhrase);
                //返回前端进行显示
                Hashtable hstPhrase = new Hashtable();
                hstPhrase.Add("Phrase", lstPhrase);
                RaiseItemSearchedResultEvent(new ItemSearchedCollectionArgs()
                {
                    ItemType = ItemType.Phrase,
                    ItemCollection = hstPhrase
                });
            }

            if (_nextHandler != null)
            {
                _nextHandler.HandleItemSelected(info);
            }
        }

        public override void HandleItemValidOperation(ItemConfigureOperationValidInfo info)
        {
            if (info.Itemtype == ItemType.ItemTwo && CheckValid(info))
            {
                RaiseItemPopWindowEvent(new ItemPopWindowInfoArgs()
                {
                    ItemType = info.Itemtype,
                    OperationType = info.Optype,
                    ItemTwo = _selectedItem
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

        /// <summary>
        /// 二级条目下的按钮校验有效性
        /// 只有在选择了一级条目后，可以进行增加操作
        /// 只有在选择了二级条目后，才可以进行修改和删除操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private bool CheckValid(ItemConfigureOperationValidInfo info)
        {
            bool res = false;
            if (info.Itemtype == ItemType.ItemTwo)
            {
                if (((info.Optype == OperationType.Modify || info.Optype == OperationType.Delete) && (_selectedItem != null && !string.IsNullOrEmpty(_selectedItem.JZItemTwoID)))
                    || (info.Optype == OperationType.Add && (_selectedItem != null && !string.IsNullOrEmpty(_selectedItem.JZItemOneID))))
                {
                    res = true;
                }
            }
            return res;
        }

        public override void HandleItemOperation(ItemConfigureOperationInfo info)
        {
            if (info.ItemInfo.ItemType == ItemType.ItemTwo)
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
            string itemTwoID;
            JZItemTwo model = info;
            bool res = _itemProcessDal.AddItemTwo(model, out itemTwoID);
            model.JZItemTwoID = itemTwoID;
            RaiseItemChangedEvent(new ItemChangedInfoArgs()
            {
                IsSucceed = res,
                OperationType = OperationType.Add,
                ItemType = ItemType.ItemTwo,
                ItemInfo = model
            });
        }

        private void HandleItemModifyOperation(ItemConfigureOperationInfo info)
        {
            bool res = _itemProcessDal.UpdateItemTwo(info);
            RaiseItemChangedEvent(new ItemChangedInfoArgs()
               {
                   IsSucceed = res,
                   OperationType = OperationType.Modify,
                   ItemType = ItemType.ItemTwo,
                   ItemInfo = info
               });

        }

        private void HandleItemDeleteOperation(ItemConfigureOperationInfo info)
        {
            bool res = _itemProcessDal.DelItemTwo(info.ItemInfo.ItemID);
            RaiseItemChangedEvent(new ItemChangedInfoArgs()
            {
                IsSucceed = res,
                OperationType = OperationType.Delete,
                ItemType = ItemType.ItemTwo,
                ItemInfo = info
            });

        }
    }
}
