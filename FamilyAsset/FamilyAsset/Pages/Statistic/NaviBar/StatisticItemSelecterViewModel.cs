using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ItemConfigureProcess;
using Common;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.Statistic.NaviBar
{
    class StatisticItemSelecterViewModel : NotificationObject
    {
        public event EventHandler<List<SelectStatisticItemEventArgs>> SelectedStatisticItemsEvent;

        private StatisticItemViewModel _allIncome;

        public StatisticItemViewModel AllIncome
        {
            get { return _allIncome; }
            set
            {
                _allIncome = value;
                RaisePropertyChanged("AllIncome");
            }
        }

        private StatisticItemViewModel _allCost;

        public StatisticItemViewModel AllCost
        {
            get { return _allCost; }
            set
            {
                _allCost = value;
                RaisePropertyChanged("AllCost");
            }
        }

        private ObservableCollection<StatisticItemViewModel> _incomeItemOnes;

        public ObservableCollection<StatisticItemViewModel> IncomeItemOnes
        {
            get { return _incomeItemOnes; }
            set
            {
                _incomeItemOnes = value;
                RaisePropertyChanged("IncomeItemOnes");
            }
        }

        private ObservableCollection<StatisticItemViewModel> _costItemOnes;

        public ObservableCollection<StatisticItemViewModel> CostItemOnes
        {
            get { return _costItemOnes; }
            set
            {
                _costItemOnes = value;
                RaisePropertyChanged("CostItemOnes");
            }
        }

        private ObservableCollection<StatisticItemViewModel> _incomeItemTwos;

        public ObservableCollection<StatisticItemViewModel> IncomeItemTwos
        {
            get { return _incomeItemTwos; }
            set
            {
                _incomeItemTwos = value;
                RaisePropertyChanged("IncomeItemTwos");
            }
        }

        private ObservableCollection<StatisticItemViewModel> _costItemTwos;

        public ObservableCollection<StatisticItemViewModel> CostItemTwos
        {
            get { return _costItemTwos; }
            set
            {
                _costItemTwos = value;
                RaisePropertyChanged("CostItemTwos");
            }
        }

        private List<SelectStatisticItemEventArgs> _lstSelectedStatisticItems;
        private IItemConfigureProcess _itemProcess;
        private bool _isIncomeSelected;

        public StatisticItemSelecterViewModel(IItemConfigureProcess process)
        {
            _itemProcess = process;
            _itemProcess.ItemSearchedResultEvent += OnItemSearchedResult;

            AllIncome = new StatisticItemViewModel(true, null);
            AllCost = new StatisticItemViewModel(false, null);
            AllIncome.StatisticItemIsSelected += OnAllItemSelected;
            AllCost.StatisticItemIsSelected += OnAllItemSelected;

        }

        private void OnAllItemSelected(object sender, SelectStatisticItemEventArgs e)
        {
            if (e.IsIncome)
            {
                if (e.IsSelected)//选中时，清空收入二级目录
                    IncomeItemTwos.Clear();
                foreach (StatisticItemViewModel item in IncomeItemOnes)//根据是否选中，改变收入一级目录是否使能
                {
                    item.SwitchSelectable(!e.IsSelected);
                }
            }
            else
            {
                if (e.IsSelected)//选中时，清空二级目录
                    CostItemTwos.Clear();
                foreach (StatisticItemViewModel item in CostItemOnes)//根据是否选中，改变支出一级目录是否使能
                {
                    item.SwitchSelectable(!e.IsSelected);
                }
            }
        }

        private void OnItemSearchedResult(object sender, BLL.ItemSearchedCollectionArgs e)
        {
            switch (e.ItemType)
            {
                case ItemType.ItemOne:
                    List<Model.JZItemOne> lstItemOnes = e.ItemCollection["One"] as List<Model.JZItemOne>;
                    if (lstItemOnes.Count > 0)
                    {
                        if (lstItemOnes[0].IncomeOrCost)
                        {
                            IncomeItemOnes.Clear();
                            foreach (Model.JZItemOne item in lstItemOnes)
                            {
                                IncomeItemOnes.Add(item);
                                IncomeItemOnes.Last().StatisticItemIsSelected += OnStatisticItemSelected;
                            }
                        }
                        else
                        {
                            CostItemOnes.Clear();
                            foreach (Model.JZItemOne item in lstItemOnes)
                            {
                                CostItemOnes.Add(item);
                                IncomeItemOnes.Last().StatisticItemIsSelected += OnStatisticItemSelected;
                            }
                        }
                    }
                    break;
                case ItemType.ItemTwo:
                    List<Model.JZItemTwo> lstItemTwos = e.ItemCollection["Two"] as List<Model.JZItemTwo>;
                    if (lstItemTwos.Count > 0)
                    {
                        if (_isIncomeSelected)
                        {
                            IncomeItemTwos.Clear();
                            foreach (Model.JZItemTwo item in lstItemTwos)
                            {
                                IncomeItemTwos.Add(StatisticItemViewModel.ConvertFromJZTwo(item, _isIncomeSelected));
                                IncomeItemOnes.Last().StatisticItemIsSelected += OnStatisticItemSelected;
                            }
                        }
                        else
                        {
                            CostItemTwos.Clear();
                            foreach (Model.JZItemTwo item in lstItemTwos)
                            {
                                CostItemTwos.Add(StatisticItemViewModel.ConvertFromJZTwo(item, _isIncomeSelected));
                                IncomeItemOnes.Last().StatisticItemIsSelected += OnStatisticItemSelected;
                            }
                        }
                    }
                    break;
            }
        }

        private void OnStatisticItemSelected(object sender, SelectStatisticItemEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
