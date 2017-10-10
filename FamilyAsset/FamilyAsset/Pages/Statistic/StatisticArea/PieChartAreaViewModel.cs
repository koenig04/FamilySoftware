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
using System.Windows.Media;

namespace FamilyAsset.Pages.Statistic.StatisticArea
{
    class PieChartAreaViewModel : NotificationObject
    {
        private Visibility _pieVis;

        public Visibility PieVis
        {
            get { return _pieVis; }
            set
            {
                _pieVis = value;
                RaisePropertyChanged("PieVis");
            }
        }

        private SeriesCollection _pieSeries;

        public SeriesCollection PieSeries
        {
            get { return _pieSeries; }
            set
            {
                _pieSeries = value;
                RaisePropertyChanged("PieSeries");
            }
        }


        public Func<ChartPoint, string> PointLabel { get; set; }

        public PieChartAreaViewModel()
        {
            PointLabel = chartPoint =>
            string.Format("{0}:{1}(2:P)", chartPoint.X, chartPoint.Y, chartPoint.Sum);
        }

        public void UpdatePieData(PieData data)
        {
            List<Color> colors = StatisticColorSet.GetSeperateColors(data.PieDataDetailCollection.Count);
            PieSeries = new SeriesCollection();
            for (int i = 0; i < data.PieDataDetailCollection.Count; i++)
            {
                PieSeries.Add(new PieSeries()
                {
                    Values = new ChartValues<decimal> { data.PieDataDetailCollection[i].SumAmount },
                    Title = data.PieDataDetailCollection[i].ItemName,
                    DataLabels = true,
                    LabelPoint = PointLabel,
                    LabelPosition = PieLabelPosition.OutsideSlice,
                    Foreground = new SolidColorBrush(colors[i])
                });
            }
        }
    }
}
