using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.StatisticProcess.DiagramRelative.DiagramDataClassifier;
using Common;
using Model;

namespace BLL.StatisticProcess.DiagramRelative.DiagramDataGenerator
{
    class DiagramDataGeneratorForItemTwo : DiagramDataGeneratorBase
    {
        public DiagramDataGeneratorForItemTwo(DAL.Statistic dal) : base(dal) { }

        public override void GenerateDiagramData(StaticSearchInfo info, out CurveData curveData, out PieData pieData)
        {
            base.GenerateDiagramData(info, out curveData, out pieData);

            List<Model.AccountDetail> accountDetails = new List<Model.AccountDetail>();
            List<List<SingleCurveData>> curveForItemTwos = new List<List<SingleCurveData>>();
            List<SinglePieData> pieForItemTwos = new List<SinglePieData>();
            foreach (string item in info.ItemTwoIDs)
            {
                //Get account detail
                accountDetails.AddRange(dal.GetAccountDetails(info.StartDate, info.EndDate, (int)info.TimeIntervalType,
                    info.InOrOutFlag, null, item));
                //Get curve data
                curveForItemTwos.Add(dal.GetSignalCurveData(info.StartDate, info.EndDate, (int)info.TimeIntervalType,
                   info.InOrOutFlag, null, item));
                //Get Pie data
                pieForItemTwos.Add(new SinglePieData()
                {
                    ItemName = accountDetails[accountDetails.Count - 1].JZItemTwoName,
                    SumAmount = (from l in curveForItemTwos[curveForItemTwos.Count - 1]
                                 select l.CurveAmount).Sum()
                });
            }

            //assemble the data
            for (int i = 0; i < info.ItemTwoIDs.Count; i++)
            {
                curveData.CurveDataDetailCollectioion.Add(new CurveDataDetailSet()
                {
                    ItemName = pieForItemTwos[i].ItemName,
                    CurveDataDetailCollection = curveForItemTwos[i].ConvertAll(o => (CurveDataDetail)o)
                });
            }
            curveData.Details = DiagramDataClassifierFactory.CreateClassifier(StaticType.Time)
                .ClassifyTheDetails(accountDetails.ConvertAll(o => (AccountDetail)o))
                .ConvertAll(o => (AccountDetailByDate)o);

            pieData.PieDataDetailCollection = pieForItemTwos.ConvertAll(o => (PieDataDetail)o);
            pieData.Details = DiagramDataClassifierFactory.CreateClassifier(StaticType.Sort)
               .ClassifyTheDetails(accountDetails.ConvertAll(o => (AccountDetail)o), ItemType.ItemTwo)
               .ConvertAll(o => (AccountDetailBySort)o);
        }
    }
}
