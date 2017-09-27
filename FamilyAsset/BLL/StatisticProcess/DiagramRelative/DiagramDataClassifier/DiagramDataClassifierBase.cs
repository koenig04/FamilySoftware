using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace BLL.StatisticProcess.DiagramRelative.DiagramDataClassifier
{
    abstract class DiagramDataClassifierBase
    {
        public abstract List<object> ClassifyTheDetails(List<AccountDetail> oriDetails, ItemType sortType = ItemType.None);
    }
}
