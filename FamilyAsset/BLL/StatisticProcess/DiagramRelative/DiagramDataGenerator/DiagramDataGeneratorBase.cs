using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.StatisticProcess.DiagramRelative.DiagramDataGenerator
{
    abstract class DiagramDataGeneratorBase
    {
        protected DAL.Statistic dal;
        public DiagramDataGeneratorBase(DAL.Statistic dal)
        {
            this.dal = dal;
        }
        public virtual void GenerateDiagramData(StaticSearchInfo info, out CurveData curveData, out PieData pieData)
        {
            curveData = new CurveData();
            curveData.CurveDataDetailCollectioion = new List<CurveDataDetailSet>();
            curveData.Details = new List<AccountDetailByDate>();

            pieData = new PieData();
            pieData.PieDataDetailCollection = new List<PieDataDetail>();
            pieData.Details = new List<AccountDetailBySort>();
        }
    }
}
