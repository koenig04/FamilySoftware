using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace BLL.StatisticProcess.DiagramRelative.DiagramDataClassifier
{
    class DiagramDataClassifierForCurve : DiagramDataClassifierBase
    {
        public override List<object> ClassifyTheDetails(List<AccountDetail> oriDetails, ItemType sortType = ItemType.None)
        {
            List<AccountDetail> sortedList = null;
            List<AccountDetailByDate> result = null;
            if (oriDetails.Count > 0)
            {
                sortedList = oriDetails.OrderBy(o => o.AccountDate)
                    .ThenBy(o => sortType == ItemType.ItemOne ? o.ItemOneID : o.ItemTwoID).ToList();

                int startIndex = 0;
                DateTime sortDate = sortedList[0].AccountDate;
                List<AccountDetail> partialList;
                result = new List<AccountDetailByDate>();

                for (int i = 0; i < sortedList.Count; i++)
                {
                    if (sortedList[i].AccountDate != sortDate)
                    {
                        partialList = sortedList.GetRange(startIndex, i - startIndex);
                        result.Add(partialList);
                        startIndex = i;
                        sortDate = sortedList[i].AccountDate;
                    }
                }
                result.Add(sortedList.GetRange(startIndex, sortedList.Count - startIndex));

                return result.ConvertAll(o => (object)o);
            }
            else
            {
                return null;
            }
        }
    }
}
