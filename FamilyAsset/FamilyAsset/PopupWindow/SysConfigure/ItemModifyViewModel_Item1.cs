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
    class ItemModifyViewModel_Item1 : ItemModifyPopWindowViewModel
    {
        public ItemModifyViewModel_Item1()
            : base()
        {

        }

        public override void SetContext(Common.IContext Context)
        {
            base.SetContext(Context);

            //弹窗标题
            ItemModifyTitle += "一级分类";

            ItemConfigPopWindowContext context = Context as ItemConfigPopWindowContext;

            Item1 = new PopWindowItemViewModel()
            {
                ItemName = "一级分类名称",
                IsEditable = context.OpType == Common.OperationType.Delete ? false : true,
                ItemVisibility = Visibility.Visible,
                ItemValue = context.OpType == Common.OperationType.Add ? string.Empty : context.ItemOne.JZItemOneName
            };
            Item2 = new PopWindowItemViewModel()
            {
                ItemVisibility = Visibility.Hidden
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
                                OperationType = this._opType,
                                ItemInfo = _itemOneModel
                            });
                            //_tmWaitForCallback.Start();
                        }));
                }
                return base.CmdConfirm;
            }
        }

        protected override void UpdateItemInfo()
        {
            base.UpdateItemInfo();
            _itemOneModel.JZItemOneName = Item1.ItemValue;
            _itemOneModel.IconName = _iconName;
            _itemOneModel.IconNamePressed = _iconPressedName;
        }        
    }
}
