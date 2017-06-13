using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace BLL.BudgetProcess
{
    public class BudgetProcessManager : IBudgetProcess
    {
        public event EventHandler<BudgetCollectionArgs> BudgetSearchedEvent;
        public event EventHandler<BudgetWithExpenditureCollectionArgs> BudgetWithExpenditureSearchedEvent;

        private DAL.Budget _budgetDal = new DAL.Budget();
        private DAL.Expenditure _expenditureDal = new DAL.Expenditure();
        private DAL.JZItemOne _itemOneDal = new DAL.JZItemOne();
        private DAL.JZItemTwo _itemTwoDal = new DAL.JZItemTwo();

        public void GetBudgetList(int budgetYear, int budgetMonth)
        {
            List<Model.Budget> lstBudget = _budgetDal.GetList(budgetYear, budgetMonth);
            if (BudgetSearchedEvent != null)
            {
                BudgetSearchedEvent(null, new BudgetCollectionArgs() { BudgetCollection = lstBudget });
            }
        }

        public void GetBudgetWithExpenditure(int budgetYear, int budgetMonth)
        {
            List<Model.Budget> lstBudget = _budgetDal.GetList(budgetYear, budgetMonth);
            List<Model.Expenditure> lstExpenditure = _expenditureDal.GetList(budgetYear, budgetMonth);
            List<Model.JZItemOne> _lstItemOnes = new List<Model.JZItemOne>();
            Hashtable _hstItemTwos = new Hashtable();
            //获得所有的支出一级条目信息
            _lstItemOnes = (from d in _itemOneDal.GetList(false).AsEnumerable()
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

            //生成已有预算的支出情况
            List<BudgetWithExpenditure> lstHasBudget = (from d in lstBudget
                                                        select new BudgetWithExpenditure()
                                                        {
                                                            ItemType = string.IsNullOrEmpty(d.ItemTwoID) ?
                                                                        ItemType.ItemOne : ItemType.ItemTwo,
                                                            ItemName = string.IsNullOrEmpty(d.ItemTwoID) ?
                                                                        _lstItemOnes.Where(a => a.JZItemOneID == d.ItemOneID).FirstOrDefault().JZItemOneName :
                                                                        ((_hstItemTwos[d.ItemOneID] as List<Model.JZItemTwo>).Where(a => a.JZItemTwoID == d.ItemTwoID)).FirstOrDefault().JZItemTwoName,
                                                            IconName = string.IsNullOrEmpty(d.ItemTwoID) ?
                                                                        _lstItemOnes.Where(a => a.JZItemOneID == d.ItemOneID).FirstOrDefault().IconName :
                                                                        ((_hstItemTwos[d.ItemOneID] as List<Model.JZItemTwo>).Where(a => a.JZItemTwoID == d.ItemTwoID)).FirstOrDefault().IconName,
                                                            ExpenditureAmount = (decimal)lstExpenditure.Where(a => a.ItemOneID == d.ItemOneID && a.ItemTwoID == d.ItemTwoID).FirstOrDefault().ExpenditureAmount,
                                                            BudgetAmount = (decimal)d.BudgetAmount,
                                                            ItemOneID = d.ItemOneID,
                                                            ItemTwoID = d.ItemTwoID
                                                        }).ToList<BudgetWithExpenditure>();
            //生成未设预算的支出情况
            List<BudgetWithExpenditure> lstWithoutBudget = (from d in lstExpenditure
                                                            where !(from e in lstBudget
                                                                    select e.ItemOneID).Contains(d.ItemOneID) ||
                                                                    !(from e in lstBudget
                                                                      select e.ItemTwoID).Contains(d.ItemTwoID)
                                                            select new BudgetWithExpenditure()
                                                            {
                                                                ItemType = string.IsNullOrEmpty(d.ItemTwoID) ?
                                                                            ItemType.ItemOne : ItemType.ItemTwo,
                                                                ItemName = string.IsNullOrEmpty(d.ItemTwoID) ?
                                                                            _lstItemOnes.Where(a => a.JZItemOneID == d.ItemOneID).FirstOrDefault().JZItemOneName :
                                                                            ((_hstItemTwos[d.ItemOneID] as List<Model.JZItemTwo>).Where(a => a.JZItemTwoID == d.ItemTwoID)).FirstOrDefault().JZItemTwoName,
                                                                IconName = string.IsNullOrEmpty(d.ItemTwoID) ?
                                                                            _lstItemOnes.Where(a => a.JZItemOneID == d.ItemOneID).FirstOrDefault().IconName :
                                                                            ((_hstItemTwos[d.ItemOneID] as List<Model.JZItemTwo>).Where(a => a.JZItemTwoID == d.ItemTwoID)).FirstOrDefault().IconName,
                                                                ExpenditureAmount = (decimal)d.ExpenditureAmount,
                                                                BudgetAmount = 0,
                                                                ItemOneID = d.ItemOneID,
                                                                ItemTwoID = d.ItemTwoID
                                                            }).ToList<BudgetWithExpenditure>();
            //合并两个List，并按照ItemOneID进行排序
            List<BudgetWithExpenditure> lstTotal = (from d in lstHasBudget.Concat(lstWithoutBudget)
                                                    orderby d.ItemOneID
                                                    select d).ToList<BudgetWithExpenditure>();

            if (BudgetWithExpenditureSearchedEvent != null)
            {
                BudgetWithExpenditureSearchedEvent(null, new BudgetWithExpenditureCollectionArgs() { BudgetWithExpenditureCollection = lstTotal });
            }
        }
    }
}
