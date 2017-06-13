using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BudgetProcess
{
    public interface IBudgetProcess
    {
        event EventHandler<BudgetCollectionArgs> BudgetSearchedEvent;
        event EventHandler<BudgetWithExpenditureCollectionArgs> BudgetWithExpenditureSearchedEvent;
        void GetBudgetList(int budgetYear, int budgetMonth);
        void GetBudgetWithExpenditure(int budgetYear, int budgetMonth);
    }
}
