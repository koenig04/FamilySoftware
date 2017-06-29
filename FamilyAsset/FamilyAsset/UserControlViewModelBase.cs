using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FamilyAsset.UICore;

namespace FamilyAsset
{
    abstract class UserControlViewModelBase : NotificationObject, IUserControlViewModel
    {
        public event EventHandler<UserControlMessageEventArgs> UserControlMessageEvent;

        private Visibility _vis = Visibility.Visible;

        public Visibility Vis
        {
            get { return _vis; }
            set
            {
                _vis = value;
                RaisePropertyChanged("Vis");
            }
        }

        public abstract void HandleViewModelCallBack(ViewModelCallBackInfo callbackInfo);
        public void HandleVisablilityControl(bool visibility)
        {
            this.Vis = visibility ? Visibility.Visible : Visibility.Hidden;
        }
        protected void RaiseUserControlMessageEvent(UserControlMessageEventArgs e)
        {
            if (UserControlMessageEvent != null)
            {
                UserControlMessageEvent(null, e);
            }
        }
    }
}
