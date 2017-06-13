using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AssetInputAndOperationProcess.AccountOperation.AccountInfoLoading
{
    class AccountInfoLoadingItemTwo : AccountInfoLoadingSuper
    {
        public AccountInfoLoadingItemTwo(DAL.JZItemOne itemOneDal, DAL.JZItemTwo itemTwoDal, DAL.AccountInfo accountDal)
            : base(itemOneDal, itemTwoDal, accountDal)
        {

        }

        public override void LoadAccountInfos(AccountOperationInfo info)
        {
            //获取二级条目信息
            Model.JZItemTwo itemTwo = _itemTwoDal.GetModel(info.ItemInfo.ItemID);

            //获取此一级条目的信息
            Model.JZItemOne itemOne = _itemOneDal.GetModel(itemTwo.JZItemOneID);

            //获得在指定时间段内的所有账目信息
            List<AccountSearchedResultInfo> lstAccount = (from d in _accountInfoDal.GetList(null, null, info.StartDate, info.EndDate, info.ItemInfo.IsIncome ? 1 : 0)
                                                          select new AccountSearchedResultInfo()
                                                          {
                                                              AccountInfo = new AccountInputInfo()
                                                              {
                                                                  AccountDate = d.AccountDate,
                                                                  AccountAmount = d.AccountAmount,
                                                                  Phrases = d.Notice,
                                                                  ItemOneID = d.ItemOneID,
                                                                  ItemTwoID = d.ItemTwoID
                                                              },
                                                              ItemOneName = itemOne.JZItemOneName,
                                                              ItemTwoName = itemTwo.JZItemTwoName,
                                                              Icon = itemTwo.IconName
                                                          }).ToList<AccountSearchedResultInfo>();
            RaiseAccountSearchedResultEvent(new AccountSearchedCollectionArgs() { AccountCollection = lstAccount });
        }
    }
}
