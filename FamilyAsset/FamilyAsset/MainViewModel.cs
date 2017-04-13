using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using FamilyAsset.Context;
using FamilyAsset.Pages.SysConfigure;
using FamilyAsset.UICore;

namespace FamilyAsset
{
    class MainViewModel : ViewModelBase
    {
        private IBussiness m_bussiness = new BussinessManager();

        private SysConfigureViewModel _SysConfig;

        public SysConfigureViewModel SysConfig
        {
            get
            {
                if (_SysConfig == null)
                {
                    _SysConfig = new SysConfigureViewModel(ref m_bussiness);
                    _SysConfig.UserControlMessageEvent += _SysConfig_UserControlMessageEvent;
                }
                return _SysConfig;
            }
            set
            {
                _SysConfig = value;
                RaisePropertyChanged("SysConfig");
            }
        }

        void _SysConfig_UserControlMessageEvent(object sender, UserControlMessageEventArgs e)
        {
            MsgManager.SendMsg<ItemConfigPopWindowContext>("PopItemConfigure", 
                e.Context as ItemConfigPopWindowContext);
        }

        //private string _TotalLimit = "1000";

        //public string TotalLimit
        //{
        //    get { return _TotalLimit; }
        //    set
        //    {
        //        _TotalLimit = value;
        //        RaisePropertyChanged("TotalLimit");
        //    }
        //}

        public override void SetContext(Common.IContext Context)
        {
            
        }

        public override void ViewModelCallBack(ViewModelCallBackInfo Info)
        {
            switch (Info.FuncType)
            {
                case Common.FunctionType.ItemConfig:

                    break;
            }
        }
    }
}
