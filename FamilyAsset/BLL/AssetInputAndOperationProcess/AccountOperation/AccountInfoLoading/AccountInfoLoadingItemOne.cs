using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AssetInputAndOperationProcess.AccountOperation.AccountInfoLoading
{
    class AccountInfoLoadingItemOne : AccountInfoLoadingSuper
    {
        public AccountInfoLoadingItemOne(DAL.JZItemOne itemOneDal, DAL.JZItemTwo itemTwoDal, DAL.AccountInfo accountDal)
            : base(itemOneDal, itemTwoDal, accountDal)
        {

        }

        public override void LoadAccountInfos(AccountOperationInfo info)
        {
            //获取此一级条目的信息
            Model.JZItemOne itemOne = _itemOneDal.GetModel(info.ItemInfo.ItemID);
            //获取此一级条目对应的二级条目的信息
            List<Model.JZItemTwo> lstItemTwo = (from d in _itemTwoDal.GetList(info.ItemInfo.ItemID).AsEnumerable()
                                                select new Model.JZItemTwo()
                                                {
                                                    JZItemOneID = d.Field<string>("JZItemOneID"),
                                                    JZItemTwoID = d.Field<string>("JZItemTwoID"),
                                                    JZItemTwoName = d.Field<string>("JZItemTwoName"),
                                                    IconName = d.Field<string>("IconName")
                                                }).ToList<Model.JZItemTwo>();
            //获取记账信息
            List<AccountSearchedResultInfo> lstAccount = (from d in _accountInfoDal.GetList(info.ItemInfo.ItemID, null, info.StartDate, info.EndDate, info.ItemInfo.IsIncome ? 1 : 0)
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
                                                              ItemOneName = info.ItemInfo.ItemName,
                                                              ItemTwoName = lstItemTwo.Count > 0 ?
                                                                            (from e in lstItemTwo
                                                                             where e.JZItemTwoID == d.ItemTwoID
                                                                             select e.JZItemTwoName).FirstOrDefault()
                                                                           : string.Empty,
                                                              Icon = lstItemTwo.Count > 0 ?
                                                                    (from e in lstItemTwo
                                                                     where e.JZItemTwoID == d.ItemTwoID
                                                                     select e.IconName).FirstOrDefault()
                                                                    : itemOne.IconName
                                                          }).ToList<AccountSearchedResultInfo>();
            RaiseAccountSearchedResultEvent(new AccountSearchedCollectionArgs() { AccountCollection = lstAccount });
        }
    }
}
