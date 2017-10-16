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
            OrderCurveData(info.TimeIntervalType, ref curveData);
            if (DiagramDataDisplayEvent != null)
            {
                DiagramDataDisplayEvent(null, new DiagramData()
                {
                    CurveDataSet = curveData,
                    PieDataSet = pieData
                });
            }
        }

        /// <summary>
        /// The individual curveData detail doesn't always contain all the dates,
        /// so we need to add the missing date with zero amount.
        /// These ordered data can be shown easilly by UI.
        /// </summary>
        /// <param name="timeInterval"></param>
        /// <param name="curveData"></param>
        private void OrderCurveData(StatisticIntervalType timeInterval, ref CurveData curveData)
        {
            DateTime startDate = FindStartOrEndDate(curveData, true);
            DateTime endDate = FindStartOrEndDate(curveData, false);
            List<CurveDataDetail> additionalData = new List<CurveDataDetail>();
            foreach (CurveDataDetailSet item in curveData.CurveDataDetailCollectioion)
            {
                for (int i = 0; i < item.CurveDataDetailCollection.Count; i++)
                {
                    if (i == 0)
                    {
                        additionalData.AddRange(GenerateZeroData(timeInterval,
                            startDate, DateTime.Parse(item.CurveDataDetailCollection[0].TimeIntervalTitle)));
                    }
                    else if (i == item.CurveDataDetailCollection.Count - 1)
                    {
                        additionalData.AddRange(GenerateZeroData(timeInterval,
                            DateTime.Parse(item.CurveDataDetailCollection[i].TimeIntervalTitle), endDate));
                    }
                    else
                    {
                        additionalData.AddRange(GenerateZeroData(timeInterval,
                            DateTime.Parse(item.CurveDataDetailCollection[i].TimeIntervalTitle),
                            DateTime.Parse(item.CurveDataDetailCollection[i + 1].TimeIntervalTitle)));
                    }
                }
            }

        }
        
        private DateTime FindStartOrEndDate(CurveData data, bool isStart)
        {
            List<DateTime> dates = new List<DateTime>();
            foreach (CurveDataDetailSet item in data.CurveDataDetailCollectioion)
            {
                if (isStart)
                    dates.Add((from s in item.CurveDataDetailCollection
                               select DateTime.Parse(s.TimeIntervalTitle)).Min());
                else
                    dates.Add((from s in item.CurveDataDetailCollection
                               select DateTime.Parse(s.TimeIntervalTitle)).Max());
            }
            if (isStart)
                return dates.Min();
            else
                return dates.Max();
        }

        private List<CurveDataDetail> GenerateZeroData(StatisticIntervalType timeInterval, DateTime dateTimeEarlier, DateTime dateTimeLater)
        {
            List<CurveDataDetail> res = new List<CurveDataDetail>();
            int timeDiff = 0;
            if (timeInterval == StatisticIntervalType.Day)
            {
                timeDiff = (dateTimeLater - dateTimeEarlier).Days;
                for (int i = 0; i < timeDiff; i++)
                {
                    res.Add(new CurveDataDetail()
                    {
                        TimeIntervalTitle = dateTimeEarlier.AddDays(i + 1).ToString("yyyy-MM-dd"),
                        SumAmount = 0
                    });
                }
            }
            else if (timeInterval == StatisticIntervalType.Month)
            {
                timeDiff = (dateTimeLater.Year - dateTimeEarlier.Year) * 12 + (dateTimeLater.Month - dateTimeEarlier.Month);
                for (int i = 0; i < timeDiff; i++)
                {
                    res.Add(new CurveDataDetail()
                    {
                        TimeIntervalTitle = dateTimeEarlier.AddMonths(i + 1).ToString("yyyy-MM"),
                        SumAmount = 0
                    });
                }
            }
            else
            {
                timeDiff = dateTimeLater.Year - dateTimeEarlier.Year;
                for (int i = 0; i < timeDiff; i++)
                {
                    res.Add(new CurveDataDetail()
                    {
                        TimeIntervalTitle = dateTimeEarlier.AddYears(i + 1).ToString("yyyy"),
                        SumAmount = 0
                    });
                }
            }

            return res;
        }
    }
}
