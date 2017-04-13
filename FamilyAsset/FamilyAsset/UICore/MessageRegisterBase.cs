using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FamilyAsset.UICore
{
    /// <summary>
    /// Message注册基类
    /// </summary>
    abstract class MessageRegisterBase
    {
        private object _regInstance;
        /// <summary>
        /// Message的注册主体（此处用的是对应的View）
        /// </summary>
        public object RegInstance
        {
            get
            {
                if (_regInstance == null)
                    _regInstance = Application.Current.MainWindow;
                return _regInstance;
            }
            set { _regInstance = value; }
        }

        private MessageManager _msgManager;
        /// <summary>
        /// Message管理类
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

        public MessageRegisterBase(object regInstance = null, MessageManager msgManager = null)
        {
            if (regInstance != null) RegInstance = regInstance;
            if (msgManager != null) MsgManager = msgManager;
        }

        public abstract void Register();
        /// <summary>
        /// 注册信息
        /// </summary>
        /// <param name="msgName"></param>
        /// <param name="action"></param>
        protected void RegisterMsg(string msgName, Action action)
        {
            MsgManager.Register(RegInstance, msgName, action);
        }

        protected void RegisterMsg<T>(string msgName, Action<T> action)
        {
            MsgManager.Register(RegInstance, msgName, action);
        }
    }
}
