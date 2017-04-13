using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Common;
using FamilyAsset.Context;
using FamilyAsset.UICore;

namespace FamilyAsset.PopupWindow.SysConfigure
{
    class ItemModifyViewModel_Phrase : ItemModifyPopWindowViewModel
    {
        public override void SetContext(Common.IContext Context)
        {
            base.SetContext(Context);

            ItemModifyTitle += "常用语";

            ItemConfigPopWindowContext context = Context as ItemConfigPopWindowContext;

            Item1 = new PopWindowItemViewModel()
            {
                ItemName = "一级分类名称",
                ItemValue = context.ItemOne.JZItemOneName,
                ItemVisibility = System.Windows.Visibility.Visible,
                IsEditable = false
            };

            if (context.ItemTwo != null)
            {
                Item2 = new PopWindowItemViewModel()
                {
                    ItemName = "二级分类名称",
                    ItemValue = context.ItemTwo.JZItemTwoName,
                    ItemVisibility = System.Windows.Visibility.Visible,
                    IsEditable = false
                };
            }

            Phrase = new PopWindowItemViewModel()
            {
                ItemName = "常用语",
                ItemVisibility = System.Windows.Visibility.Visible,
                IsEditable = context.OpType == Common.OperationType.Delete ? false : true,
                ItemValue = context.OpType == Common.OperationType.Add ? string.Empty : context.Phrase.PhraseContent
            };
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
                            m_bussiness.ItemConfigure(m_opType, Common.ItemType.ItemOne, m_phrase);
                        }));
                }
                return base.CmdConfirm;
            }
        }

        protected override void UpdateItemInfo()
        {
            base.UpdateItemInfo();
            m_phrase.PhraseContent = Phrase.ItemValue;
            m_phrase.ItemID = Item2 == null ? m_item1.JZItemOneID : m_item2.JZItemTwoID;
        }

        public override void ViewModelCallBack(ViewModelCallBackInfo Info)
        {
            base.ViewModelCallBack(Info);
            if (Info.IsSucceed == true)
            {
                MsgManager.SendMsg<ViewModelCallBackInfo>("CloseWindow",
                       new ViewModelCallBackInfo(FunctionType.ItemConfig,
                           true,
                           new ItemConfigCallBackContext(ItemType.Phrase)));
            }
        }
    }
}
