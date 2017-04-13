using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyAsset.UICore
{
    internal class ViewModelInfo
    {
        public Type ViewType { get; private set; }
        public Type ViewModelType { get; private set; }
        public Type MsgRegisterType { get; private set; }
        public string IdentityFlag { get; private set; }

        public ViewModelInfo(Type viewType, Type viewModelType, Type msgRegisterType, string identityFlag = "")
        {
            ViewType = viewType;
            ViewModelType = viewModelType;
            MsgRegisterType = msgRegisterType;
            IdentityFlag = identityFlag;
        }

        /// <summary>
        /// 获得ViewModel实例
        /// </summary>
        /// <returns></returns>
        public ViewModelBase GetViewModelInstance()
        {
            if (ViewModelType == null) return null;
            return ViewModelType
                .Assembly
                .CreateInstance(ViewModelType.FullName)
                as ViewModelBase;
        }

        /// <summary>
        /// 获得Message实例
        /// </summary>
        /// <returns></returns>
        public MessageRegisterBase GetMsgRegisterInstance()
        {
            if (MsgRegisterType == null) return null;
            return MsgRegisterType
                 .Assembly
                 .CreateInstance(MsgRegisterType.FullName)
                 as MessageRegisterBase;
        }
    }
}
