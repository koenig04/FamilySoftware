using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using BLL.AssetInputAndOperationProcess;
using Common;

namespace FamilyAsset.Context
{
    class AccountRecordPopWindowContext:IContext
    {
        public IAssetInputAndOperationProcess InputProcess { get; set; }
        public AccountInputInfo InputInfo { get; set; }
        public string ItemOneName { get; set; }
        public string ItemTwoName { get; set; }
    }
}
