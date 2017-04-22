using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ItemConfigureProcess
{
    public interface IItemConfigureProcess
    {
        void HandleItemValidOperation(ItemConfigureOperationValidInfo info);
        void HandleItemOperation(ItemConfigureOperationInfo info);
        void HandleCallBack(CallBackInfo info);
        void HandleItemSelected(ItemSelectedInfo info);
        event EventHandler<ItemPopWindowInfoArgs> ItemPopWindowEvent;
        event EventHandler<ItemChangedInfoArgs> ItemChangedEvent;
        event EventHandler<ItemSearchedCollectionArgs> ItemSearchedResultEvent;
    }
}
