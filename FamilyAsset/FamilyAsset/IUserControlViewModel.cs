using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace FamilyAsset
{
    interface IUserControlViewModel
    {
        event EventHandler<UserControlMessageEventArgs> UserControlMessageEvent;
        void HandleViewModelCallBack(ViewModelCallBackInfo callbackInfo);
    }

    class UserControlMessageEventArgs:EventArgs
    {
        public string Message { get; set; }
        public IContext Context { get; set; }
    }
}
