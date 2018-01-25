using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FamilyAsset.Context;
using FamilyAsset.PopupWindow.AccountRecord;
using FamilyAsset.PopupWindow.SysConfigure;
using FamilyAsset.UICore;

namespace FamilyAsset
{
    class MainMessage : MessageRegisterBase
    {
        public override void Register()
        {
            RegisterMsg<ItemConfigPopWindowContext>("PopItemConfigure", s =>
                {
                    new ItemModifyPopWindow(s,s.ItemType.ToString()).Show();
                });

            RegisterMsg<AccountRecordPopWindowContext>("PopAccountOperation", s =>
            {
                new AccountRecordWindows(s).Show();
            });
        }
    }
}
