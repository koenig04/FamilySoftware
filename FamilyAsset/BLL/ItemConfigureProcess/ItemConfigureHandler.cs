using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ItemConfigureProcess
{
    class ItemConfigureHandler
    {
        protected ItemConfigureHandler _nextHandler;

        public void SetNextHandler(ItemConfigureHandler nextHandler)
        {
            this._nextHandler = nextHandler;
        }

        public abstract void HandleItemOperation(ItemConfigureOperationInfo info);
        public abstract void HandleCallBack(CallBackInfo info);
        public abstract void HandleItemSelected(ItemSelectedInfo info);
    }
}
