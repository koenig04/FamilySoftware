using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BLL.StatisticProcess;
using BLL.StatisticProcess.StatisticItemRelative;
using Common;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.Statistic.StatisticItems
{
    class StatisticItemSelecterViewModel : NotificationObject
    {
        public event EventHandler<List<SelectStatisticItemEventArgs>> SelectedStatisticItemsEvent;

        private Visibility _vis = Visibility.Hidden;

        public Visibility Vis
        {
            get { return _vis; }
            set { _vis = value; }
        }


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

        private DelegateCommand _confirm;

        public DelegateCommand Confirm
        {
            get
            {
                if (_confirm == null)
                {
                    _confirm = new DelegateCommand(new Action<object>(
                        o =>
                        {
                            if (SelectedStatisticItemsEvent != null)
                            {
                                SelectedStatisticItemsEvent(null, _lstSelectedItems);
                            }
                            Vis = Visibility.Hidden;
                        }));
                }
                return _confirm;
            }
            set
            {
                _confirm = value;
                RaisePropertyChanged("Confirm");
            }
        }

        private DelegateCommand _cancel;

        public DelegateCommand Cancel
        {
            get
            {
                if (_cancel == null)
                {
                    _cancel = new DelegateCommand(new Action<object>(
                        o =>
                        {
                            Vis = Visibility.Hidden;
                        }));
                }
                return _cancel;
            }
            set
            {
                _cancel = value;
                RaisePropertyChanged("Cancel");
            }
        }


        private IStatiticProcess _statisticProcess;
        private bool _isIncomeSelected, _ItemOneMultiSelected;
        private List<SelectStatisticItemEventArgs> _lstSelectedItems;
        private ItemCollectionController _incomeItemOneController, _incomeItemTwoController,
            _costItemOneController, _costItemTwoController,
            _allIncomeController, _allCostController;

        public StatisticItemSelecterViewModel(IStatiticProcess process)
        {
            _statisticProcess = process;
            _statisticProcess.ItemCollectionAddEvent += OnItemCollectionAdd;
            _statisticProcess.ItemCollectionClearEvent += OnItemCollectionClear;

            _lstSelectedItems = new List<SelectStatisticItemEventArgs>();

            AllIncome = new StatisticItemViewModel(true, null);
            AllCost = new StatisticItemViewModel(false, null);

            _incomeItemOneController = new ItemCollectionController(IncomeItemOnes, true, ItemType.ItemOne);
            _incomeItemTwoController = new ItemCollectionController(IncomeItemTwos, true, ItemType.ItemTwo);
            _costItemOneController = new ItemCollectionController(CostItemOnes, true, ItemType.ItemOne);
            _costItemTwoController = new ItemCollectionController(CostItemTwos, true, ItemType.ItemTwo);
            _allIncomeController = new ItemCollectionController(AllIncome, true, ItemType.None);
            _allCostController = new ItemCollectionController(AllCost, false, ItemType.None);

            _incomeItemOneController.StatisticItemSelected += OnStatisticItemSelected;
            _incomeItemTwoController.StatisticItemSelected += OnStatisticItemSelected;
            _costItemOneController.StatisticItemSelected += OnStatisticItemSelected;
            _costItemTwoController.StatisticItemSelected += OnStatisticItemSelected;
            _allIncomeController.StatisticItemSelected += OnStatisticItemSelected;
            _allCostController.StatisticItemSelected += OnStatisticItemSelected;

            _incomeItemOneController.StatisticItemsListOperation += OnStatisticItemsListOperation;
            _incomeItemTwoController.StatisticItemsListOperation += OnStatisticItemsListOperation;
            _costItemOneController.StatisticItemsListOperation += OnStatisticItemsListOperation;
            _costItemTwoController.StatisticItemsListOperation += OnStatisticItemsListOperation;
            _allIncomeController.StatisticItemsListOperation += OnStatisticItemsListOperation;
            _allCostController.StatisticItemsListOperation += OnStatisticItemsListOperation;

            _statisticProcess.InitializeItemOnes();
        }

        private void OnStatisticItemsListOperation(object sender, StatisticItemsListOperationEventArgs e)
        {
            switch (e.OperationType)
            {
                case StatisticItemsListOperationType.Add:
                    foreach (SelectStatisticItemEventArgs item in e.OperationItems)
                    {
                        _lstSelectedItems.Add(item);
                    }
                    break;
                case StatisticItemsListOperationType.Remove:
                    while (_lstSelectedItems.Where(a => a.IsIncome == e.IsIncome && a.ItemType == e.StatisticItemType).First() != null)
                    {
                        _lstSelectedItems.Remove(_lstSelectedItems.Where(a => a.IsIncome == e.IsIncome && a.ItemType == e.StatisticItemType).First());
                    }
                    break;
            }
        }

        private ItemCollectionController ItemCollectionIdentify(bool isIncome, ItemType itemType)
        {
            if (isIncome)
            {
                if (itemType == ItemType.ItemOne)
                {
                    return _incomeItemOneController;
                }
                else if (itemType == ItemType.ItemTwo)
                {
                    return _incomeItemTwoController;
                }
                else
                {
                    return _allIncomeController;
                }
            }
            else
            {
                if (itemType == ItemType.ItemOne)
                {
                    return _costItemOneController;
                }
                else if (itemType == ItemType.ItemTwo)
                {
                    return _costItemTwoController;
                }
                else
                {
                    return _allCostController;
                }
            }
        }

        private void OnItemCollectionClear(object sender, ClearItemsArgs e)
        {
            ItemCollectionIdentify(e.IsIncome, e.ClearedItemType).ClearItems(e);
        }

        private void OnItemCollectionAdd(object sender, ItemCollectionOperationArgs e)
        {
            ItemCollectionIdentify(e.IsIncome, e.ItemOneCollection != null ? ItemType.ItemOne : ItemType.ItemTwo).AddItems(e);
        }

        private void OnStatisticItemSelected(object sender, SelectStatisticItemEventArgs e)
        {
            _statisticProcess.ProceedSelectedItem(e);
        }
    }
}
