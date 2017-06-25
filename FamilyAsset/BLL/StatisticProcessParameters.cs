using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SearchStatisticInfo
    {
        public bool IsIncome { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ItemOneID { get; set; }
        public string ItemTwoID { get; set; }
    }

    public class StatisticSearchedArgs : EventArgs
    {

    }
}
