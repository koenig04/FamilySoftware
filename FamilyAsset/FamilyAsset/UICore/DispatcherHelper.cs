using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace FamilyAsset.UICore
{
    /// <summary>
    /// 线程调度帮助类（跨线程调度UI）
    /// </summary>
    class DispatcherHelper
    {
        public static Dispatcher UIDispatcher { get; set; }
    }
}
