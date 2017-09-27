using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.StatisticProcess.DiagramRelative.DiagramDataGenerator;
using Model;

namespace BLL.StatisticProcess.DiagramRelative
{
    class DiagramProcessController
    {
        public event EventHandler<DiagramData> DiagramDataDisplayEvent;       

        private DAL.Statistic _dal = new DAL.Statistic();       

        public void SearchDiagramData(StaticSearchInfo info)
        {
            CurveData curveData;
            PieData pieData;
            DiagramDataGeneratorFactory.CreateGenerator(info, _dal).GenerateDiagramData(info, out curveData, out pieData);
            if (DiagramDataDisplayEvent != null)
            {
                DiagramDataDisplayEvent(null, new DiagramData()
                {
                    CurveDataSet = curveData,
                    PieDataSet = pieData
                });
            }
        } 
    }
}
