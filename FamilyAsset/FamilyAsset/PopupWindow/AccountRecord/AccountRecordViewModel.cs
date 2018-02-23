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
        private string _operationType;

        public string OperationType
        {
            get { return _operationType; }
            set
            {
                _operationType = value;
                RaisePropertyChanged("OperationType");
            }
        }


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
                            OperateAccountRecord();
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

        private DelegateCommand _cmdCancel;
        /// <summary>
        /// 按下取消键对应的操作（关闭当前窗口）
        /// </summary>
        public DelegateCommand CmdCancel
        {
            get
            {
                if (_cmdCancel == null)
                {
                    _cmdCancel = new DelegateCommand(new Action<object>(
                        o =>
                        {
                            MsgManager.SendMsg<ViewModelCallBackInfo>("CloseWindow", null);
                        }));
                }
                return _cmdCancel;
            }
            set
            {
                _cmdCancel = value;
                RaisePropertyChanged("CmdConfirm");
            }
        }

        private IAssetInputAndOperationProcess _accountProcess;
        private AccountRecordPopWindowContext _context;

        public override void SetContext(Common.IContext Context)
        {
            AccountRecordPopWindowContext context = Context as AccountRecordPopWindowContext;
            AccountDate = AccountDate ?? new AccountItemViewModel("日期", context.InputInfo.AccountDate.ToString("yyyy-MM-dd"));
            AccountSort = AccountSort ?? new AccountItemViewModel("类别",
                context.ItemOneName + (context.ItemTwoName == null ? "" : ("-" + context.ItemTwoName)));
            AccountAmount = AccountAmount ?? new AccountItemViewModel("金额", context.InputInfo.AccountAmount.ToString());
            Notice = Notice ?? new AccountItemViewModel("备注", context.InputInfo.Phrases);
            switch (context.OpType)
            {
                case Common.OperationType.Add:
                    OperationType = "新增账目";
                    break;
                case Common.OperationType.Delete:
                    OperationType = "删除账目";
                    break;
                case Common.OperationType.Modify:
                    OperationType = "修改账目";
                    break;
            }

            _accountProcess = context.InputProcess;
            _accountProcess.AccountOperationResultEvent += OnAccountOperationResult;
            _context = context;
        }

        private void OperateAccountRecord()
        {
            switch (_context.OpType)
            {
                case Common.OperationType.Add:
                    _accountProcess.HandleAccountInput(_context.InputInfo);
                    break;
                default:
                    break;
                //_accountProcess.HandleAccountOperation(
            }
        }

        private void OnAccountOperationResult(object sender, Common.BoolenEventArgs e)
        {
            _accountProcess.AccountOperationResultEvent -= OnAccountOperationResult;
            string msg = OperationType + (e.Content ? "成功" : "失败");
            MsgManager.SendMsg<GeneralPopWindowContext>("ShowResult", new GeneralPopWindowContext() { Msg = msg, FuncType = FunctionType.None });
        }

        public override void ViewModelCallBack(ViewModelCallBackInfo Info)
        {
            MsgManager.SendMsg<ViewModelCallBackInfo>("CloseWindow",
                       new ViewModelCallBackInfo(FunctionType.ItemConfig,
                           FamilyAsset.Pages.UIControlNames.AccountRecord,
                           true
                           ));           
        }
    }
}
