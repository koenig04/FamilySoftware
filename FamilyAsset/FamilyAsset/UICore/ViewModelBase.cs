using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Common;

namespace FamilyAsset.UICore
{
    abstract class ViewModelBase : NotificationObject
    {
        private MessageManager _msgManager;
        /// <summary>
        /// Message调度
        /// </summary>
        public MessageManager MsgManager
        {
            get
            {
                if (_msgManager == null)
                    _msgManager = MessageManager.Default;
                return _msgManager;
            }
            set { _msgManager = value; }
        }

        private Dispatcher _UIDispatcher;
        /// <summary>
        /// UI线程调度
        /// </summary>
        public Dispatcher UIDispatcher
        {
            get
            {
                if (_UIDispatcher == null)
                    _UIDispatcher = DispatcherHelper.UIDispatcher;
                return _UIDispatcher;
            }
            set { _UIDispatcher = value; }
        }

        public abstract void SetContext(IContext Context);
        public abstract void ViewModelCallBack(ViewModelCallBackInfo Info);
    }
}
