using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.StatisticProcess.DiagramRelative.DiagramDataGenerator
{
    class DiagramDataGeneratorFactory
    {
        private static DiagramDataGeneratorBase _all, _itemOne, _itemTwo;
        public static DiagramDataGeneratorBase CreateGenerator(StaticSearchInfo info, DAL.Statistic dal)
        {
            if ((info.ItemOneIDs == null || info.ItemOneIDs.Count == 0) &&
               (info.ItemTwoIDs == null || info.ItemTwoIDs.Count == 0))
            {
                if (_all == null)
                {
                    _all = new DiagramDataGeneratorForAll(dal);
                }
                return _all;
            }
            else if (info.ItemOneIDs != null && info.ItemOneIDs.Count > 0)
            {
                if (_itemOne == null)
                {
                    _itemOne = new DiagramDataGeneratorForItemOne(dal);
                }
                return _itemOne;
            }
            else
            {
                if (_itemTwo == null)
                {
                    _itemTwo = new DiagramDataGeneratorForItemTwo(dal);
                }
                return _itemTwo;
            }
        }
    }
}
