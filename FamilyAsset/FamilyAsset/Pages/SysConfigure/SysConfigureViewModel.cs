using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using FamilyAsset.Pages.SysConfigure.Element.ItemConfigure;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.SysConfigure
{
    class SysConfigureViewModel : NotificationObject, IUserControlViewModel
    {
        public event EventHandler<UserControlMessageEventArgs> UserControlMessageEvent;

        private ItemConfigureViewModel _ItemConfig;

        public ItemConfigureViewModel ItemConfig
        {
            get
            {
                if (_ItemConfig == null)
                {
                    _ItemConfig = new ItemConfigureViewModel();
                    _ItemConfig.UserControlMessageEvent += _ItemConfig_UserControlMessageEvent;
                }
                return _ItemConfig;
            }
            set
            {
                _ItemConfig = value;
                RaisePropertyChanged("ItemConfig");
            }
        }

        void _ItemConfig_UserControlMessageEvent(object sender, UserControlMessageEventArgs e)
        {
            if (UserControlMessageEvent != null)
            {
                UserControlMessageEvent(null, e);
            }
        }

        

        public SysConfigureViewModel()
        {
            
        }

        public void HandleViewModelCallBack(ViewModelCallBackInfo callbackInfo)
        {
            switch (callbackInfo.FuncType)
            {
                case Common.FunctionType.ItemConfig:
                    _ItemConfig.HandleViewModelCallBack(callbackInfo);
                    break;
            }
        }
    }
}
