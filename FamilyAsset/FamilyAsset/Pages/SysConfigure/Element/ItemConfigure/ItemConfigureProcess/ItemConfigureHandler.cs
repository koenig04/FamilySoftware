using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyAsset.Pages.SysConfigure.Element.ItemConfigure.ItemConfigureProcess
{
    class ItemConfigureHandler
    {
        protected ItemConfigureHandler _nextHandler;

        public void SetNextHandler(ItemConfigureHandler nextHandler)
        {
            this._nextHandler = nextHandler;
        }

        public abstract void OnButtonsPressed(ItemConfigureButtonEventArgs e);
        public abstract void HandleViewModelCallBack(ViewModelCallBackInfo callbackInfo);
        public abstract void OnItemSelected(ItemClickedEventArgs e);
    }
}
