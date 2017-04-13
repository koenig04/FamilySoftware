using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyAsset.Context;

namespace FamilyAsset.PopupWindow.Message
{
    class ItemConfigPopWindowMessage : MessageBase
    {
        public override void Register()
        {
            base.Register();

            RegisterMsg<GeneralPopWindowContext>("ShowResult",
                g =>
                {
                    new GeneralPopWindow(g).Show();
                });
        }
    }
}
