using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TimeStatistic
    {
        public string StatisticTime { get; set; }
        public decimal StatisticAmount { get; set; }
    }

    public class SortStatistic
    {
        public string SortID { get; set; }
        public string SortName { get; set; }
        public decimal SortAmount { get; set; }
    }
}
