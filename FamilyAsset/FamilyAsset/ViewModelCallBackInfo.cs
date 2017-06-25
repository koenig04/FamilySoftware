using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using BLL.ItemConfigureProcess;
using Common;

namespace FamilyAsset
{
    /// <summary>
    /// 用于ViewModel之间的回调信息封装
    /// </summary>
    class ViewModelCallBackInfo
    {
        public FunctionType FuncType { get; private set; }
        public string Target { get; private set; }
        public bool IsSucceed { get; private set; }
        public IContext Context { get; private set; }

        public ViewModelCallBackInfo(FunctionType FuncType,string Target, bool isSucceed, IContext context = null)
        {
            this.FuncType = FuncType;
            this.IsSucceed = isSucceed;
            this.Target = Target;
            this.Context = context;
        }

        public static implicit operator CallBackInfo(ViewModelCallBackInfo model)
        {
            return new CallBackInfo()
            {
                Context = model.Context,
                FuncType = model.FuncType,
                IsSucceed = model.IsSucceed
            };
        }
    }
}
