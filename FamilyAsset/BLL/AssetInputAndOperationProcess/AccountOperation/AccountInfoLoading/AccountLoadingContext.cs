using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AssetInputAndOperationProcess.AccountOperation.AccountInfoLoading
{
    class AccountLoadingContext
    {
        public event EventHandler<AccountSearchedCollectionArgs> AccountSearchedResultEvent;
        private AccountInfoLoadingSuper _accountInfoLoadingStrategy;

        public void LoadAccountInfo(AccountOperationInfo info, DAL.JZItemOne itemOneDal,
            DAL.JZItemTwo itemTwoDal, DAL.AccountInfo accountDal)
        {
            switch (info.ItemInfo.ItemType)
            {
                case Common.ItemType.None:
                    _accountInfoLoadingStrategy = new AccountInfoLoadingAll(itemOneDal, itemTwoDal, accountDal);
                    break;
                case Common.ItemType.ItemOne:
                    _accountInfoLoadingStrategy = new AccountInfoLoadingItemOne(itemOneDal, itemTwoDal, accountDal);
                    break;
                case Common.ItemType.ItemTwo:
                    _accountInfoLoadingStrategy = new AccountInfoLoadingItemTwo(itemOneDal, itemTwoDal, accountDal);
                    break;
            }
            _accountInfoLoadingStrategy.AccountSearchedResultEvent -= OnAccountSearchedResult;
            _accountInfoLoadingStrategy.AccountSearchedResultEvent += OnAccountSearchedResult;
            _accountInfoLoadingStrategy.LoadAccountInfos(info);
        }

        private void OnAccountSearchedResult(object sender, AccountSearchedCollectionArgs e)
        {
            if (AccountSearchedResultEvent != null)
            {
                AccountSearchedResultEvent(sender, e);
            }
        }

    }
}
