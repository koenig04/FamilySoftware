using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace BLL.ItemConfigureProcess
{
    public class ItemConfigureOperationInfo
    {
        public OperationType Optype { get; set; }
        public ItemType Itemtype { get; set; }
    }

    public class ItemSelectedInfo
    {
        public ItemType ItemType { get; set; }
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemIcon { get; set; }
    }

    public class CallBackInfo
    {
        public FunctionType FuncType { get;  set; }
        public bool IsSucceed { get;  set; }
        public IContext Context { get;  set; }
    }

    public class ItemPopWindowInfoArgs : EventArgs
    {

    }
}
