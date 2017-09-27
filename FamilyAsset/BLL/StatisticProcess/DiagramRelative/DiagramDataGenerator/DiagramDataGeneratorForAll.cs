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
    class DiagramDataGeneratorForAll : DiagramDataGeneratorBase
    {
        public DiagramDataGeneratorForAll(DAL.Statistic dal) : base(dal) { }

        public override void GenerateDiagramData(StaticSearchInfo info, out CurveData curveData, out PieData pieData)
        {
            base.GenerateDiagramData(info, out curveData, out pieData);

            //Get curve data
            List<SingleCurveData> curveForIncome, curveForCost;
            curveForIncome = new List<SingleCurveData>();
            curveForCost = new List<SingleCurveData>();
            if (info.InOrOutFlag == 0 || info.InOrOutFlag == 2)
            {
                curveForCost = dal.GetSignalCurveData(info.StartDate, info.EndDate, (int)info.TimeIntervalType,
                    0, null, null);
            }
            else if (info.InOrOutFlag == 1 || info.InOrOutFlag == 2)
            {
                curveForIncome = dal.GetSignalCurveData(info.StartDate, info.EndDate, (int)info.TimeIntervalType,
                    1, null, null);
            }

            //get pie data
            List<SinglePieData> pieForAll;
            if (info.InOrOutFlag == 2)//同时显示总收和总支时，饼图只显示这两者的情况
            {
                pieForAll = new List<SinglePieData>();
                pieForAll.Add(new SinglePieData()
                {
                    IsIncome = true,
                    SumAmount = (from l in curveForIncome
                                 select l.CurveAmount).Sum()
                });
                pieForAll.Add(new SinglePieData()
                {
                    IsIncome = false,
                    SumAmount = (from l in curveForCost
                                 select l.CurveAmount).Sum()
                });
            }
            else
            {
                pieForAll = dal.GetPieData(info.StartDate, info.EndDate, (int)info.TimeIntervalType, info.InOrOutFlag, null);
            }

            //get detail
            List<Model.AccountDetail> accountDetails = new List<Model.AccountDetail>();
            if (info.InOrOutFlag == 0 || info.InOrOutFlag == 2)
            {
                accountDetails.AddRange(dal.GetAccountDetails(info.StartDate, info.EndDate, (int)info.TimeIntervalType,
                    0, null, null));
            }
            else if (info.InOrOutFlag == 1 || info.InOrOutFlag == 2)
            {
                accountDetails.AddRange(dal.GetAccountDetails(info.StartDate, info.EndDate, (int)info.TimeIntervalType,
                    1, null, null));
            }

            //assemble the data
            if (curveForIncome.Count > 0)
            {
                curveData.CurveDataDetailCollectioion.Add(new CurveDataDetailSet()
                {
                    ItemName = "收入",
                    CurveDataDetailCollection = curveForIncome.ConvertAll(o => (CurveDataDetail)o)
                });
            }
            if (curveForCost.Count > 0)
            {
                curveData.CurveDataDetailCollectioion.Add(new CurveDataDetailSet()
                {
                    ItemName = "支出",
                    CurveDataDetailCollection = curveForCost.ConvertAll(o => (CurveDataDetail)o)
                });
            }

            curveData.Details = DiagramDataClassifierFactory.CreateClassifier(StaticType.Time)
                .ClassifyTheDetails(accountDetails.ConvertAll(o => (AccountDetail)o))
                .ConvertAll(o => (AccountDetailByDate)o);

            pieData.PieDataDetailCollection = pieForAll.ConvertAll(o => (PieDataDetail)o);
            pieData.Details = DiagramDataClassifierFactory.CreateClassifier(StaticType.Sort)
                .ClassifyTheDetails(accountDetails.ConvertAll(o => (AccountDetail)o), ItemType.ItemOne)
                .ConvertAll(o => (AccountDetailBySort)o);
        }
    }
}
