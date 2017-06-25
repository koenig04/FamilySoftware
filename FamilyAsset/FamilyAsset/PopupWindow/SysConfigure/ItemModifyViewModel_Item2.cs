using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BLL;
using Common;
using FamilyAsset.Context;
using FamilyAsset.UICore;

namespace FamilyAsset.PopupWindow.SysConfigure
{
    class ItemModifyViewModel_Item2 : ItemModifyPopWindowViewModel
    {
        public override void SetContext(Common.IContext Context)
        {
            base.SetContext(Context);

            ItemModifyTitle += "二级分类";

            ItemConfigPopWindowContext context = Context as ItemConfigPopWindowContext;

            Item1 = new PopWindowItemViewModel()
            {
                ItemName = "一级分类名称",
                ItemValue = context.ItemOne.JZItemOneName,
                ItemVisibility = Visibility.Visible,
                IsEditable = false
            };

            Item2 = new PopWindowItemViewModel()
            {
                ItemName = "二级分类名称",
                ItemVisibility = Visibility.Visible,
                IsEditable = context.OpType == Common.OperationType.Delete ? false : true,
                ItemValue = context.OpType == Common.OperationType.Add ? string.Empty : context.ItemTwo.JZItemTwoName
            };

            Phrase = new PopWindowItemViewModel()
            {
                ItemVisibility = Visibility.Hidden
            };

            IsLoadImgAvailable = context.OpType == Common.OperationType.Delete ? false : true;
        }

        public override DelegateCommand CmdConfirm
        {
            get
            {
                if (base.CmdConfirm == null)
                {
                    base.CmdConfirm = new DelegateCommand(new Action<object>(
                        o =>
                        {
                            UpdateItemInfo();
                            _bussiness.HandleItemOperation(new ItemConfigureOperationInfo()
                            {
                                OperationType = _opType,
                                ItemInfo = _itemTwoModel
                            });
                        }));
                }
                return base.CmdConfirm;
            }
        }

        protected override void UpdateItemInfo()
        {
            base.UpdateItemInfo();
            _itemTwoModel.IconName = _iconName;
            _itemTwoModel.JZItemOneID = _itemOneModel.JZItemOneID;
            _itemTwoModel.JZItemTwoName = Item2.ItemValue;
            _itemTwoModel.IconNamePressed = _iconPressedName;
        }
    }
}
