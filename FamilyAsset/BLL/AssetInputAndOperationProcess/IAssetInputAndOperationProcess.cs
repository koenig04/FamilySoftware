using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace BLL.AssetInputAndOperationProcess
{
    public interface IAssetInputAndOperationProcess
    {
        void HandleItemSelected(ItemSelectedInfo info);
        void HandleAccountInput(AccountInputInfo info);
        /// <summary>
        /// 当Loading账目信息时，根据ItemSelectedInfo中的 ItemType来区分，
        /// 为None，代表Load所有的账目信息
        /// 为ItemOne，代表Load某一个一级条目下的账目
        /// 为ItemTwo，代表Load某一个二级条目下的账目
        /// </summary>
        /// <param name="info"></param>
        void HandleAccountOperation(AccountOperationInfo info);
        Model.AccountInfo GetAccountModel(string accountID);

        event EventHandler<ItemSearchedCollectionArgs> ItemSearchedResultEvent;
        event EventHandler<AccountSearchedCollectionArgs> AccountSearchedResultEvent;
        event EventHandler<BoolenEventArgs> AccountOperationResultEvent;
    }
}
