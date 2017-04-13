using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FamilyAsset.UICore;

namespace FamilyAsset.PopupWindow.Message
{
    class MessageBase : MessageRegisterBase
    {
        public override void Register()
        {
            //关闭窗口
            RegisterMsg<ViewModelCallBackInfo>("CloseWindow", i =>
            {
                ((Window)RegInstance).Close();
                ViewModelManager.CloseWindow(i);
            });
        }
    }
}
