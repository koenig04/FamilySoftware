using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FamilyAsset.UICore
{
    /// <summary>
    /// Message Manager
    /// </summary>
    class MessageManager
    {
        private static MessageManager _default;

        public static MessageManager Default
        {
            get
            {
                if (_default == null)
                    _default = new MessageManager();
                return _default;
            }
        }

        /// <summary>
        /// restore all the message action
        /// </summary>
        private readonly List<MsgActionInfo> _messageList = new List<MsgActionInfo>();

        /// <summary>
        /// registe the message
        /// </summary>
        /// <param name="regInstance"></param>
        /// <param name="msgName"></param>
        /// <param name="action"></param>
        /// <param name="group"></param>
        public void Register(object regInstance, string msgName, Action action)
        {
            _messageList.Add(new MsgActionInfo
            {
                RegInstance = regInstance,
                MsgName = msgName,
                Action = action
            });
        }

        /// <summary>
        /// 带类型的Message注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="regInstance"></param>
        /// <param name="msgName"></param>
        /// <param name="action"></param>
        public void Register<T>(object regInstance, string msgName, Action<T> action)
        {
            _messageList.Add(new MsgActionInfo<T>
            {
                RegInstance = regInstance,
                MsgName = msgName,
                Action = action
            });
        }

        /// <summary>
        /// 发送Message（即执行Message的Action）
        /// </summary>
        /// <param name="msgName"></param>
        /// <param name="targetType"></param>
        public void SendMsg(string msgName, Type targetType = null)
        {
            var actions = GetMsgActionInfo(msgName, targetType);

            foreach (var item in actions)
            {
                item.Execute();
            }
        }

        /// <summary>
        /// 发送带类型的Message（即执行Message的Action）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msgName"></param>
        /// <param name="msgArgs"></param>
        /// <param name="targetType"></param>
        public void SendMsg<T>(string msgName, T msgArgs, Type targetType = null)
        {
            var actions = GetMsgActionInfo(msgName, targetType);
            foreach (var item in actions)
            {
                var msgAction = item as MsgActionInfo<T>;
                if (msgAction != null)
                    msgAction.Execute(msgArgs);
            }
        }

        /// <summary>
        /// 取消注册
        /// </summary>
        /// <param name="regInstance"></param>
        public void UnRegister(object regInstance)
        {
            var msgActions = _messageList.Where(m => m.RegInstance == regInstance).ToList();
            foreach (var item in msgActions)
            {
                _messageList.Remove(item);
            }
        }

        public void Clear()
        {
            _messageList.Clear();
        }

        private IEnumerable<MsgActionInfo> GetMsgActionInfo(string msgName, Type targetType)
        {
            if (targetType == null)
                return _messageList.Where(m =>
                    m.MsgName == msgName);
            else
            {
                return _messageList.Where(m =>
                    m.MsgName == msgName
                    && m.RegInstance.GetType() == targetType);
            }
        }

        public void WindowClose(object sender, EventArgs e)
        {
            //注销窗体的消息
            UnRegister(sender);
            //注销ViewModel的消息
            var win = sender as FrameworkElement;
            if (win != null)
                UnRegister(win.DataContext);
        }
    }
}
