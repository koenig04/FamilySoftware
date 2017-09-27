using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.StatisticProcess.DiagramRelative.DiagramDataGenerator
{
    class DiagramDataGeneratorFactory
    {
        public static DiagramDataGeneratorBase CreateGenerator(StaticSearchInfo info, DAL.Statistic dal)
        {
            if ((info.ItemOneIDs == null || info.ItemOneIDs.Count == 0) &&
               (info.ItemTwoIDs == null || info.ItemTwoIDs.Count == 0))
            {
                return new DiagramDataGeneratorForAll(dal);
            }
            else if (info.ItemOneIDs != null && info.ItemOneIDs.Count > 0)
            {
                return new DiagramDataGeneratorForItemOne(dal);
            }
            else
            {
                return new DiagramDataGeneratorForItemTwo(dal);
            }
        }
    }
}
