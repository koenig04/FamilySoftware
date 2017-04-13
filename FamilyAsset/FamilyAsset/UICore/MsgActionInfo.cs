using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyAsset.UICore
{
    /// <summary>
    /// Message类
    /// </summary>
    class MsgActionInfo
    {
        public object RegInstance { get; internal set; }    //Message的注册主体（此处用的是对应的View）
        public string MsgName { get; internal set; }        //Message的名称（此为Message的主键）
        public Action Action { get; internal set; }         //该Message对应的Action

        public void Execute()
        {
            if (Action != null)
                Action();
        }
    }

    /// <summary>
    /// 带类型的Message类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class MsgActionInfo<T> : MsgActionInfo
    {
        public new Action<T> Action { get; internal set; }

        public new void Execute()
        {
            Execute(default(T));
        }

        public void Execute(T args)
        {
            if (Action != null)
                Action(args);
        }
    }
}
