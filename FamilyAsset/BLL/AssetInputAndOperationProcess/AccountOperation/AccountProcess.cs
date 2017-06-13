using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Common;
using BLL.AssetInputAndOperationProcess.AccountOperation.AccountInfoLoading;

namespace BLL.AssetInputAndOperationProcess.AccountOperation
{
    class AccountProcess
    {
        public event EventHandler<AccountSearchedCollectionArgs> AccountSearchedResultEvent;
        public event EventHandler<BoolenEventArgs> AccountOperationResultEvent;

        private DAL.JZItemOne _itemOneDal;
        private DAL.JZItemTwo _itemTwoDal;
        private DAL.AccountInfo _accountInfoDal;

        private AccountLoadingContext _accountLoading;

        public AccountProcess()
        {
            this._itemOneDal = new DAL.JZItemOne();
            this._itemTwoDal = new DAL.JZItemTwo();
            this._accountInfoDal = new DAL.AccountInfo();
            this._accountLoading = new AccountLoadingContext();
            this._accountLoading.AccountSearchedResultEvent += OnAccountSearchedResult;
        }

        private void OnAccountSearchedResult(object sender, AccountSearchedCollectionArgs e)
        {
            if (AccountSearchedResultEvent != null)
            {
                AccountSearchedResultEvent(sender, e);
            }
        }

        public void HandleAccountInput(AccountInputInfo info)
        {
            bool res = _accountInfoDal.Add(info);
            RaiseAccountOperationResultEvent(new BoolenEventArgs(res));
        }

        public void HandleAccountOperation(AccountOperationInfo info)
        {
            switch (info.OperationType)
            {
                case OperationType.Search:
                    _accountLoading.LoadAccountInfo(info,_itemOneDal,_itemTwoDal,_accountInfoDal);
                    break;
            }
        }

        private void UpdateAccount(AccountOperationInfo info)
        {
            bool res = _accountInfoDal.Update(info.AccountInfo);            
        }

        private void DelAccount(AccountOperationInfo info)
        {
            bool res = _accountInfoDal.Del(info.AccountInfo);
        }

        private void RaiseAccountSearchedResultEvent(AccountSearchedCollectionArgs e)
        {
            if (AccountSearchedResultEvent != null)
            {
                AccountSearchedResultEvent(null, e);
            }
        }

        private void RaiseAccountOperationResultEvent(BoolenEventArgs e)
        {
            if (AccountOperationResultEvent != null)
            {
                AccountOperationResultEvent(null, e);
            }
        }
    }
}
