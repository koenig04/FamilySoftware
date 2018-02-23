using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyAsset.Pages
{
    class UserControlManager
    {
        private static Dictionary<string, IUserControlViewModel> _dicUserControl = new Dictionary<string, IUserControlViewModel>();

        public static void Register(IUserControlViewModel usercontrol, string usercontrolName)
        {
            if (_dicUserControl.ContainsKey(usercontrolName))
            {
                _dicUserControl[usercontrolName] = usercontrol;
            }
            else
            {
                _dicUserControl.Add(usercontrolName, usercontrol);
            }
        }

        public static void SendCallBack(ViewModelCallBackInfo info)
        {
            if (info != null && _dicUserControl.ContainsKey(info.Target))
            {
                _dicUserControl[info.Target].HandleViewModelCallBack(info);
            }
        }

        public static void ControlVisibility(string usercontrolerName, bool visibility)
        {
            if (_dicUserControl.ContainsKey(usercontrolerName))
            {
                _dicUserControl[usercontrolerName].HandleVisablilityControl(visibility);
                if (visibility)
                {
                    foreach (IUserControlViewModel item in (from uc in _dicUserControl
                                                            where uc.Key != usercontrolerName
                                                            select uc.Value))
                    {
                        item.HandleVisablilityControl(false);
                    }
                }
            }
        }
    }


}
