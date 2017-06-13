using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AssetInputAndOperationProcess.AccountOperation.AccountInfoLoading
{
    class AccountInfoLoadingAll : AccountInfoLoadingSuper
    {
        public AccountInfoLoadingAll(DAL.JZItemOne itemOneDal, DAL.JZItemTwo itemTwoDal, DAL.AccountInfo accountDal)
            : base(itemOneDal, itemTwoDal, accountDal)
        {

        }

        public override void LoadAccountInfos(AccountOperationInfo info)
        {
            List<Model.JZItemOne> _lstItemOnes = new List<Model.JZItemOne>();
            Hashtable _hstItemTwos = new Hashtable();
            //获得所有的一级条目信息
            _lstItemOnes = (from d in _itemOneDal.GetList(info.ItemInfo.IsIncome).AsEnumerable()
                            select new Model.JZItemOne()
                            {
                                JZItemOneID = d.Field<string>("JZItemOneID"),
                                JZItemOneName = d.Field<string>("JZItemOneName"),
                                IconName = d.Field<string>("IconName")
                            }).ToList<Model.JZItemOne>();

            //根据一级条目，获得其下属的所有二级条目的信息
            foreach (Model.JZItemOne item in _lstItemOnes)
            {
                List<Model.JZItemTwo> lst = (from d in _itemTwoDal.GetList(item.JZItemOneID).AsEnumerable()
                                             select new Model.JZItemTwo()
                                             {
                                                 JZItemTwoID = d.Field<string>("JZItemTwoID"),
                                                 JZItemTwoName = d.Field<string>("JZItemTwoName"),
                                                 IconName = d.Field<string>("IconName")
                                             }).ToList<Model.JZItemTwo>();
                _hstItemTwos.Add(item.JZItemOneID, lst);
            }

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
                                                              ItemOneName = (from e in _lstItemOnes
                                                                             where e.JZItemOneID == d.ItemOneID
                                                                             select e.JZItemOneName).FirstOrDefault(),
                                                              ItemTwoName = (from e in _hstItemTwos[d.ItemOneID] as List<Model.JZItemTwo>
                                                                             where e.JZItemTwoID == d.ItemTwoID
                                                                             select e.JZItemTwoName).FirstOrDefault(),
                                                              Icon = (from e in _lstItemOnes
                                                                      where e.JZItemOneID == d.ItemOneID
                                                                      select e.IconName).FirstOrDefault()
                                                          }).ToList<AccountSearchedResultInfo>();
            RaiseAccountSearchedResultEvent(new AccountSearchedCollectionArgs() { AccountCollection = lstAccount });
        }
    }
}
