using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BLL.StatisticProcess;
using FamilyAsset.Pages.Statistic.AccountDetail;
using FamilyAsset.Pages.Statistic.MainElements;
using FamilyAsset.Pages.Statistic.NaviBar;

namespace FamilyAsset.Pages.Statistic
{
    class StatisticViewModel : UserControlViewModelBase
    {
        private NaviBarViewModel _naviBar;

        public NaviBarViewModel NaviBar
        {
            get { return _naviBar; }
            set
            {
                _naviBar = value;
                RaisePropertyChanged("NaviBar");
            }
        }

        private TotalInAndOutViewModel _totalInOut;

        public TotalInAndOutViewModel TotalInOut
        {
            get { return _totalInOut; }
            set
            {
                _totalInOut = value;
                RaisePropertyChanged("TotalInOut");
            }
        }

        private AccountTimeCollectionViewModel _timeDetail;

        public AccountTimeCollectionViewModel TimeDetail
        {
            get { return _timeDetail; }
            set
            {
                _timeDetail = value;
                RaisePropertyChanged("TimeDetail");
            }
        }

        private AccountSortCollectionViewModel _sortDetail;

        public AccountSortCollectionViewModel SortDetail
        {
            get { return _sortDetail; }
            set
            {
                _sortDetail = value;
                RaisePropertyChanged("SortDetail");
            }
        }

        private AccountItemModifyViewModel _accountModify;

        public AccountItemModifyViewModel AccountModify
        {
            get { return _accountModify; }
            set
            {
                _accountModify = value;
                RaisePropertyChanged("AccountModify");
            }
        }


        IStatiticProcess _statisticProcess;

        public StatisticViewModel()
        {
            Vis = Visibility.Hidden;

            _statisticProcess = new StatisticProcessManager();
            _statisticProcess.DiagramDataDisplayEvent += OnDiagramDataDisplay;

            NaviBar = new NaviBarViewModel(_statisticProcess);
            NaviBar.StatisticTypeChanged += OnStatisticTypeChanged;
            TimeDetail = new AccountTimeCollectionViewModel();
            SortDetail = new AccountSortCollectionViewModel();
            AccountModify = new AccountItemModifyViewModel();
            AccountModify.AccountInfoOperationEvent += OnAccountInfoOperation;
        }

        private void OnAccountInfoOperation(object sender, AccountInfoOperationInfoArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnStatisticTypeChanged(object sender, Common.BoolenEventArgs e)
        {
            if (e.Content)//curve
            {
                //show curve chart area
                //show curve detail area
                TimeDetail.TimeVis = Visibility.Visible;
                SortDetail.SortVis = Visibility.Collapsed;
            }
            else
            {
                //show pie chart area
                //show pie detail area
                SortDetail.SortVis = Visibility.Visible;
                TimeDetail.TimeVis = Visibility.Collapsed;
            }
        }

        private void OnDiagramDataDisplay(object sender, BLL.StatisticProcess.DiagramRelative.DiagramData e)
        {
            TimeDetail.UpdateCurveData(e.CurveDataSet);
            SortDetail.UpdatePieData(e.PieDataSet);
            TotalInOut.InputIncomeAndCost(e);
        }

        public override void HandleViewModelCallBack(ViewModelCallBackInfo callbackInfo)
        {
            throw new NotImplementedException();
        }
    }
}
