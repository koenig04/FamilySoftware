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

            IsLoadImgAvailable = false;
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
                                ItemInfo = _phraseModel
                            });
                            
                        }));
                }
                return base.CmdConfirm;
            }
        }

        protected override void UpdateItemInfo()
        {
            base.UpdateItemInfo();
            _phraseModel.PhraseContent = Phrase.ItemValue;
            _phraseModel.ItemID = string.IsNullOrEmpty(Item2.ItemValue) ? _itemOneModel.JZItemOneID : _itemTwoModel.JZItemTwoID;
        }
    }
}
