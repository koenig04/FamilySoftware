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
    class DiagramDataGeneratorForItemOne : DiagramDataGeneratorBase
    {
        public DiagramDataGeneratorForItemOne(DAL.Statistic dal) : base(dal) { }

        public override void GenerateDiagramData(StaticSearchInfo info, out CurveData curveData, out PieData pieData)
        {
            base.GenerateDiagramData(info, out curveData, out pieData);


            List<Model.AccountDetail> accountDetails = new List<Model.AccountDetail>();
            List<List<SingleCurveData>> curveForItemOnes = new List<List<SingleCurveData>>();
            List<SinglePieData> pieForItemOnes = new List<SinglePieData>();
            foreach (string item in info.ItemOneIDs)
            {
                //Get account detail
                accountDetails.AddRange(dal.GetAccountDetails(info.StartDate, info.EndDate, (int)info.TimeIntervalType,
                    info.InOrOutFlag, item, null));
                //Get curve data
                curveForItemOnes.Add(dal.GetSignalCurveData(info.StartDate, info.EndDate, (int)info.TimeIntervalType,
                   info.InOrOutFlag, item, null));
                //Get Pie data
                if (info.ItemOneIDs.Count > 1)
                {
                    pieForItemOnes.Add(new SinglePieData()
                  {
                      ItemName = accountDetails[accountDetails.Count - 1].JZItemOneName,
                      SumAmount = (from l in curveForItemOnes[curveForItemOnes.Count - 1]
                                   select l.CurveAmount).Sum()
                  });
                }
                else
                {
                    pieForItemOnes = dal.GetPieData(info.StartDate, info.EndDate, (int)info.TimeIntervalType,
                        info.InOrOutFlag, info.ItemOneIDs[0]);
                }
            }

            //assemble the data
            for (int i = 0; i < info.ItemOneIDs.Count; i++)
            {
                curveData.CurveDataDetailCollectioion.Add(new CurveDataDetailSet()
                {
                    ItemName = pieForItemOnes[i].ItemName,
                    CurveDataDetailCollection = curveForItemOnes[i].ConvertAll(o => (CurveDataDetail)o)
                });
            }
            curveData.Details = DiagramDataClassifierFactory.CreateClassifier(StaticType.Time)
                .ClassifyTheDetails(accountDetails.ConvertAll(o => (AccountDetail)o))
                .ConvertAll(o => (AccountDetailByDate)o);

            pieData.PieDataDetailCollection = pieForItemOnes.ConvertAll(o => (PieDataDetail)o);
            pieData.Details = DiagramDataClassifierFactory.CreateClassifier(StaticType.Sort)
               .ClassifyTheDetails(accountDetails.ConvertAll(o => (AccountDetail)o), ItemType.ItemOne)
               .ConvertAll(o => (AccountDetailBySort)o);
        }
    }
}
