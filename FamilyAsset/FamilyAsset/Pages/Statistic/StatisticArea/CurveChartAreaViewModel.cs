using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FamilyAsset.UICore;
using BLL.StatisticProcess.DiagramRelative;

namespace FamilyAsset.Pages.Statistic.StatisticArea
{
    class CurveChartAreaViewModel : NotificationObject
    {
        private Visibility _curveVis;

        public Visibility CurveVis
        {
            get { return _curveVis; }
            set
            {
                _curveVis = value;
                RaisePropertyChanged("CurveVis");
            }
        }

        private SeriesCollection _curveSeries;

        public SeriesCollection CurveSeries
        {
            get { return _curveSeries; }
            set
            {
                _curveSeries = value;
                RaisePropertyChanged("CurveSeries");
            }
        }

        private string[] _xLables;

        public string[] XLabels
        {
            get { return _xLables; }
            set
            {
                _xLables = value;
                RaisePropertyChanged("XLabels");
            }
        }


        public Func<double, string> YFormatter { get; set; }

        public CurveChartAreaViewModel()
        {
            YFormatter = value => value.ToString("C");
        }

        public void UpdateCurveData(CurveData data)
        {
            XLabels = data.CurveDataDetailCollectioion[0].OutputDateStringArray();
            CurveSeries = new SeriesCollection();
            foreach (CurveDataDetailSet item in data.CurveDataDetailCollectioion)
            {
                CurveSeries.Add(new LineSeries
                {
                    Title = item.ItemName,
                    Values = ConvertToChartValues(item.OutputAmounts())
                });
            }
        }

        private ChartValues<decimal> ConvertToChartValues(List<decimal> lst)
        {
            ChartValues<decimal> res = new ChartValues<decimal>();
            foreach (decimal item in lst)
            {
                res.Add(item);
            }
            return res;
        }
    }
}
