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
    class AccountRecordPopWindowContext : IContext
    {
        public IAssetInputAndOperationProcess InputProcess { get; set; }
        public AccountInputInfo InputInfo { get; set; }
        public string ItemOneName { get; set; }
        public string ItemTwoName { get; set; }
        public OperationType OpType { get; set; }

        public static implicit operator AccountOperationInfo(AccountRecordPopWindowContext context)
        {
            return new AccountOperationInfo()
            {
                OperationType = context.OpType,
                AccountInfo = new AccountInputInfo()
                {
                    AccountID = context.InputInfo.AccountID,
                    AccountAmount = context.InputInfo.AccountAmount,
                    AccountDate = context.InputInfo.AccountDate,
                    ItemOneID = context.InputInfo.ItemOneID,
                    ItemTwoID = context.InputInfo.ItemTwoID,
                    Phrases = context.InputInfo.Phrases
                }
            };
        }
    }
}
