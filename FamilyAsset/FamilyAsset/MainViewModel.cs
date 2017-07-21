using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using FamilyAsset.Context;
using FamilyAsset.Pages;
using FamilyAsset.Pages.AccountRecord;
using FamilyAsset.Pages.SysConfigure;
using FamilyAsset.UICore;

namespace FamilyAsset
{
    class MainViewModel : ViewModelBase
    {
        private SysConfigureViewModel _SysConfig;

        public SysConfigureViewModel SysConfig
        {
            get
            {
                if (_SysConfig == null)
                {
                    _SysConfig = new SysConfigureViewModel();
                    _SysConfig.UserControlMessageEvent += _SysConfig_UserControlMessageEvent;
                    UserControlManager.Register(_SysConfig, UIControlNames.SysConfigure);
                }
                return _SysConfig;
            }
            set
            {
                _SysConfig = value;
                RaisePropertyChanged("SysConfig");
            }
        }

        private AccountRecordViewModel _accountRecord;

        public AccountRecordViewModel AccountRecord
        {
            get
            {
                if (_accountRecord == null)
                {
                    _accountRecord = new AccountRecordViewModel();
                    _accountRecord.UserControlMessageEvent += OnAccountRecordMessage;
                    UserControlManager.Register(_accountRecord, UIControlNames.AccountRecord);
                }
                return _accountRecord;
            }
            set
            {
                _accountRecord = value;
                RaisePropertyChanged("AccountRecord");
            }
        }

        private void OnAccountRecordMessage(object sender, UserControlMessageEventArgs e)
        {
            throw new NotImplementedException();
        }


        void _SysConfig_UserControlMessageEvent(object sender, UserControlMessageEventArgs e)
        {
            MsgManager.SendMsg<ItemConfigPopWindowContext>("PopItemConfigure",
                e.Context as ItemConfigPopWindowContext);
        }

        private DelegateCommand _addAccount;

        public DelegateCommand AddAccount
        {
            get {
                if (_addAccount == null)
                {
                    _addAccount = new DelegateCommand(new Action<object>(
                        o =>
                        {
                            UserControlManager.ControlVisibility(UIControlNames.AccountRecord, true);
                        }));
                }
                return _addAccount; }
            set
            {
                _addAccount = value;
                RaisePropertyChanged("AddAccount");
            }
        }


        public MainViewModel()
        {

        }

        public override void SetContext(Common.IContext Context)
        {

        }

        public override void ViewModelCallBack(ViewModelCallBackInfo Info)
        {
            UserControlManager.SendCallBack(Info);
        }
    }
}
