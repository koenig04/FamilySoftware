using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.Statistic.MainElements
{
    class TotalInAndOutViewModel : NotificationObject
    {
        private TotalInfoViewModel _totalIncome;

        public TotalInfoViewModel TotalIncome
        {
            get { return _totalIncome; }
            set
            {
                _totalIncome = value;
                RaisePropertyChanged("totalIncome");
            }
        }

        private TotalInfoViewModel _totalCost;

        public TotalInfoViewModel TotalCost
        {
            get { return _totalCost; }
            set
            {
                _totalCost = value;
                RaisePropertyChanged("TotalCost");
            }
        }

        private TotalInfoViewModel _diff;

        public TotalInfoViewModel Diff
        {
            get { return _diff; }
            set
            {
                _diff = value;
                RaisePropertyChanged("Diff");
            }
        }

        public TotalInAndOutViewModel()
        {
            TotalIncome = new TotalInfoViewModel();
            TotalCost = new TotalInfoViewModel();
            Diff = new TotalInfoViewModel();
            TotalIncome.ItemColor = Colors.LimeGreen;
            TotalIncome.ItemTitle = "总收入";
            TotalCost.ItemColor = Colors.Firebrick;
            TotalCost.ItemTitle = "总支出";
            Diff.ItemColor = Colors.DeepSkyBlue;
            Diff.ItemTitle = "结余";
        }

        public void InputIncomeAndCost(decimal income, decimal cost)
        {
            TotalIncome.ItemAmount = income.ToString();
            TotalCost.ItemAmount = cost.ToString();
            Diff.ItemAmount = (income - cost).ToString();
        }
    }
}
