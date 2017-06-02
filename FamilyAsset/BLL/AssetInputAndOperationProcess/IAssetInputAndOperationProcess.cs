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
        void HandleAccountOperation(AccountOperationInfo info);

        event EventHandler<ItemSearchedCollectionArgs> ItemSearchedResultEvent;
        event EventHandler<AccountSearchedCollectionArgs> AccountSearchedResultEvent;
        event EventHandler<BoolenEventArgs> AccountOperationResultEvent;
    }
}
