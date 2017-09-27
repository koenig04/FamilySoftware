using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace BLL.StatisticProcess.DiagramRelative.DiagramDataClassifier
{
    class DiagramDataClassifierForPie : DiagramDataClassifierBase
    {
        public override List<object> ClassifyTheDetails(List<AccountDetail> oriDetails, ItemType sortType = ItemType.None)
        {
            List<AccountDetail> sortedList = null;
            List<AccountDetailBySort> result = null;
            if (sortType == ItemType.ItemOne)
            {
                sortedList = oriDetails.OrderBy(o => o.ItemOneID)
                    .ThenBy(o => o.AccountDate).ToList();
            }
            else if (sortType == ItemType.ItemTwo)
            {
                sortedList = oriDetails.OrderBy(o => o.ItemTwoID)
                    .ThenBy(o => o.AccountDate).ToList();
            }
            if (sortedList != null)
            {
                int startIndex = 0;
                string sortID;
                List<AccountDetail> partialList;
                result = new List<AccountDetailBySort>();

                if (sortType == ItemType.ItemOne)
                {
                    sortID = sortedList[0].ItemOneID;
                    for (int i = 0; i < sortedList.Count; i++)
                    {
                        if (sortedList[i].ItemOneID != sortID)
                        {
                            partialList = sortedList.GetRange(startIndex, i - startIndex);
                            result.Add(AccountDetailBySort.ConvertFromAccountDetails(partialList, sortType));
                            startIndex = i;
                            sortID = sortedList[i].ItemOneID;
                        }
                    }
                    result.Add(AccountDetailBySort.ConvertFromAccountDetails(sortedList.GetRange(startIndex, sortedList.Count - startIndex), sortType));
                }
                else
                {
                    sortID = sortedList[0].ItemTwoID;
                    for (int i = 0; i < sortedList.Count; i++)
                    {
                        if (sortedList[i].ItemTwoID != sortID)
                        {
                            partialList = sortedList.GetRange(startIndex, i - startIndex);
                            result.Add(AccountDetailBySort.ConvertFromAccountDetails(partialList, sortType));
                            startIndex = i;
                            sortID = sortedList[i].ItemTwoID;
                        }
                    }
                    result.Add(AccountDetailBySort.ConvertFromAccountDetails(sortedList.GetRange(startIndex, sortedList.Count - startIndex), sortType));
                }
                return result.ConvertAll(o => (object)o);
            }

            return null;
        }
    }
}
