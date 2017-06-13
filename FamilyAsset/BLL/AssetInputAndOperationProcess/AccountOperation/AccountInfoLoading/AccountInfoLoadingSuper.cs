using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AssetInputAndOperationProcess.AccountOperation.AccountInfoLoading
{
    class AccountInfoLoadingSuper
    {
        public event EventHandler<AccountSearchedCollectionArgs> AccountSearchedResultEvent;
        protected DAL.JZItemOne _itemOneDal;
        protected DAL.JZItemTwo _itemTwoDal;
        protected DAL.AccountInfo _accountInfoDal;
        public abstract void LoadAccountInfos(AccountOperationInfo info);

        public AccountInfoLoadingSuper(DAL.JZItemOne itemOneDal, DAL.JZItemTwo itemTwoDal, DAL.AccountInfo accountDal)
        {
            this._itemOneDal = itemOneDal;
            this._itemTwoDal = itemTwoDal;
            this._accountInfoDal = accountDal;
        }
        protected void RaiseAccountSearchedResultEvent(AccountSearchedCollectionArgs e)
        {
            if (AccountSearchedResultEvent != null)
            {
                AccountSearchedResultEvent(null, e);
            }
        }
    }
}
