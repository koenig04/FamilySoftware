using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IBussiness
    {
        //System Item Configure
        void ItemConfigure(Common.OperationType OpType, Common.ItemType ItemType, object ItemInfo, string IconFullPath = "");
        event EventHandler<ItemConfigureEventArgs> ItemConfigureEvent;
    }
}
