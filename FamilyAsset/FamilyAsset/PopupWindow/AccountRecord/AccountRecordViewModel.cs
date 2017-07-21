using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using BLL.AssetInputAndOperationProcess;
using Common;
using FamilyAsset.Context;
using FamilyAsset.UICore;

namespace FamilyAsset.PopupWindow.AccountRecord
{
    class AccountRecordViewModel : ViewModelBase
    {
        private AccountItemViewModel _accountDate;

        public AccountItemViewModel AccountDate
        {
            get { return _accountDate; }
            set
            {
                _accountDate = value;
                RaisePropertyChanged("AccountDate");
            }
        }

        private AccountItemViewModel _accountSort;

        public AccountItemViewModel AccountSort
        {
            get { return _accountSort; }
            set
            {
                _accountSort = value;
                RaisePropertyChanged("AccountSort");
            }
        }

        private AccountItemViewModel _accountAmount;

        public AccountItemViewModel AccountAmount
        {
            get { return _accountAmount; }
            set
            {
                _accountAmount = value;
                RaisePropertyChanged("AccountAmount");
            }
        }

        private AccountItemViewModel _notice;

        public AccountItemViewModel Notice
        {
            get { return _notice; }
            set
            {
                _notice = value;
                RaisePropertyChanged("Notice");
            }
        }

        private DelegateCommand _cmdConfirm;

        public DelegateCommand CmdConfirm
        {
            get
            {
                if (_cmdConfirm == null)
                {
                    _cmdConfirm = new DelegateCommand(new Action<object>(
                        o =>
                        {
                            _accountProcess.HandleAccountInput(_inputInfo);
                        }));
                }
                return _cmdConfirm;
            }
            set
            {
                _cmdConfirm = value;
                RaisePropertyChanged("CmdConfirm");
            }
        }

        private IAssetInputAndOperationProcess _accountProcess;
        private AccountInputInfo _inputInfo;

        public override void SetContext(Common.IContext Context)
        {
            AccountRecordPopWindowContext context = Context as AccountRecordPopWindowContext;
            AccountDate = AccountDate ?? new AccountItemViewModel("日期", context.InputInfo.AccountDate.ToString("yyyy-MM-dd"));
            AccountSort = AccountSort ?? new AccountItemViewModel("类别",
                context.ItemOneName + context.ItemTwoName == null ? "" : ("-" + context.ItemTwoName));
            AccountAmount = AccountAmount ?? new AccountItemViewModel("金额", context.InputInfo.AccountAmount.ToString());
            Notice = Notice ?? new AccountItemViewModel("备注", context.InputInfo.Phrases);

            _accountProcess = context.InputProcess;
            _accountProcess.AccountOperationResultEvent += OnAccountOperationResult;
            _inputInfo = context.InputInfo;
        }

        private void OnAccountOperationResult(object sender, Common.BoolenEventArgs e)
        {
            string msg = "记账" + (e.Content ? "成功" : "失败");
            MsgManager.SendMsg<GeneralPopWindowContext>("ShowResult", new GeneralPopWindowContext() { Msg = msg, FuncType = FunctionType.None });
        }

        public override void ViewModelCallBack(ViewModelCallBackInfo Info)
        {
            throw new NotImplementedException();
        }
    }
}
