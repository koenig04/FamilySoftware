using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Common;
using FamilyAsset.Context;
using FamilyAsset.UICore;

namespace FamilyAsset.PopupWindow
{
    class GeneralPopWindowViewModel : ViewModelBase
    {
        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }

        private Visibility _confirmVisibility = Visibility.Visible;

        public Visibility ConfirmVisibility
        {
            get { return _confirmVisibility; }
            set
            {
                _confirmVisibility = value;
                RaisePropertyChanged("ConfirmVisibility");
            }
        }

        private Visibility _cancelVisibility = Visibility.Hidden;

        public Visibility CancelVisibility
        {
            get { return _cancelVisibility; }
            set
            {
                _cancelVisibility = value;
                RaisePropertyChanged("CancelVisibility");
            }
        }

        private DelegateCommand _cmdConfirm;
        /// <summary>
        /// 按下确定键对应的操作
        /// </summary>
        public virtual DelegateCommand CmdConfirm
        {
            get
            {
                if (_cmdConfirm == null)
                {
                    _cmdConfirm = new DelegateCommand(new Action<object>(
                        o =>
                        {
                            MsgManager.SendMsg<ViewModelCallBackInfo>("CloseWindow",
                                new ViewModelCallBackInfo(_funcType, "", true));
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

        private FunctionType _funcType;

        public override void SetContext(Common.IContext Context)
        {
            GeneralPopWindowContext context = Context as GeneralPopWindowContext;
            Message = context.Msg;
            _funcType = context.FuncType;
        }

        public override void ViewModelCallBack(ViewModelCallBackInfo Info)
        {

        }
    }
}
