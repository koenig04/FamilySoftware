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
                            m_bussiness.ItemConfigure(m_opType, Common.ItemType.ItemOne, m_item1);
                        }));
                }
                return base.CmdConfirm;
            }
        }

        protected override void UpdateItemInfo()
        {
            base.UpdateItemInfo();
            m_item1.JZItemOneName = Item1.ItemValue;
            m_item1.IconName = m_iconName;
        }

        public override void ViewModelCallBack(ViewModelCallBackInfo Info)
        {
            base.ViewModelCallBack(Info);
            if (Info.IsSucceed == true)
            {
                MsgManager.SendMsg<ViewModelCallBackInfo>("CloseWindow",
                       new ViewModelCallBackInfo(FunctionType.ItemConfig,
                           true,
                           new ItemConfigCallBackContext(ItemType.ItemOne)));
            }
        }
    }
}
