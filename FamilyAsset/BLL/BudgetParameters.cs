using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace BLL
{
    public class BudgetWithExpenditure
    {
        public ItemType ItemType { get; set; }
        public string ItemOneID { get; set; }
        public string ItemTwoID { get; set; }
        public string ItemName { get; set; }
        public string IconName { get; set; }
        public decimal BudgetAmount { get; set; }
        public decimal ExpenditureAmount { get; set; }
    }

    public class BudgetCollectionArgs : EventArgs
    {
        public List<Model.Budget> BudgetCollection { get; set; }
    }

    public class BudgetWithExpenditureCollectionArgs : EventArgs
    {
        public List<BudgetWithExpenditure> BudgetWithExpenditureCollection { get; set; }
    }
}
