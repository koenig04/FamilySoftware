using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.StatisticProcess.DiagramRelative.DiagramDataClassifier;
using Model;

namespace BLL.StatisticProcess.DiagramRelative
{
    class DiagramProcessController
    {
        public event EventHandler<CurveData> CurveDataDisplayEvent;
        public event EventHandler<PieData> PieDataDisplayEvent;

        private List<AccountDetail> _lstDetails;
        private CurveData _curveData;
        private PieData _pieDataDetail;

        private DAL.Statistic _dal = new DAL.Statistic();


        public DiagramProcessController()
        {
            _curveData = new CurveData();
            _pieDataDetail = new PieData();
        }

        public void SearchDiagramData(StaticSearchInfo info)
        {
            //判断是否为看总收入或者总支出
            if ((info.ItemOneIDs == null || info.ItemOneIDs.Count == 0) &&
                (info.ItemTwoIDs == null || info.ItemTwoIDs.Count == 0))
            {

            }
            //List<SingleCurveData> curveDataSet=_dal.GetSignalCurveData(
        }

        private void GenerateAllData(StaticSearchInfo info)
        {
            //generate curve data
            _curveData = new CurveData();
            List<SingleCurveData> curveDataSet;
            if (info.InOrOutFlag == 0 || info.InOrOutFlag == 1)
            {
                curveDataSet = _dal.GetSignalCurveData(info.StartDate, info.EndDate,
                        (int)info.TimeIntervalType, info.InOrOutFlag, null, null);
                _curveData.CurveDataDetailCollectioion = curveDataSet;
            }
            switch (info.InOrOutFlag)
            {
                case 0://All out
                    curveDataSet = _dal.GetSignalCurveData(info.StartDate, info.EndDate,
                        (int)info.TimeIntervalType, info.InOrOutFlag, null, null);
                    break;
                case 1:
            }
        }

        private void GenerateCurveData(StaticSearchInfo info)
        {
            //List<SingleCurveData> singleCurveData;
            //if (info.ItemOneIDs != null)
            //{
            //    for (int i = 0; i < info.ItemOneIDs.Count; i++)
            //    {
            //        singleCurveData = _dal.GetSignalCurveData(info.StartDate, info.EndDate, (int)info.TimeIntervalType, 
            //            info.IsIncome?0:1, info.ItemOneIDs[i], null);   
            //    }
            //}
        }

        public void UpdateDiagramData(bool IsCurve)
        {
            List<object> updatedData = DiagramDataClassifierFactory.CreateClassifier((IsCurve ? StaticType.Time : StaticType.Sort))
                .ClassifyTheDetails(_lstDetails);

        }

        private void RaiseCurveDataDisplayEvent(CurveData e)
        {
            if (CurveDataDisplayEvent != null)
            {
                CurveDataDisplayEvent(null, e);
            }
        }

        private void RaisePieDataDisplayEvent(PieData e)
        {
            if (PieDataDisplayEvent != null)
            {
                PieDataDisplayEvent(null, e);
            }
        }
    }
}
