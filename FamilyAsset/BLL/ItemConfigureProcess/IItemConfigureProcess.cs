using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ItemConfigureProcess
{
    interface IItemConfigureProcess
    {
        void HandleItemOperation(ItemConfigureOperationInfo info);
        void HandleCallBack(CallBackInfo info);
        void HandleItemSelected(ItemSelectedInfo info);
    }
}
