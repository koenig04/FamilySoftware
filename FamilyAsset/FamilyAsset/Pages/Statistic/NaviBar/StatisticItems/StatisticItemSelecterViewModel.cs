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
    /// <summary>
    /// View Model for
    /// Statistic Item Selection Interface
    /// </summary>
    class StatisticItemSelecterViewModel : NotificationObject
    {
        //this event is never used. check wether this event is necessary later
        public event EventHandler<List<SelectStatisticItemEventArgs>> SelectedStatisticItemsEvent;
        public event EventHandler<StatisticItemsListOperationEventArgs> StatisticItemOperatedEvent;

        private Visibility _vis = Visibility.Collapsed;
        /// <summary>
        /// control the visibility of interface.
        /// It is controled by filter button on navi bar
        /// </summary>
        public Visibility Vis
        {
            get { return _vis; }
            set { _vis = value; }
        }


        private StatisticItemViewModel _allIncome;
        /// <summary>
        /// all income item(it is always shown)
        /// </summary>
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
        /// <summary>
        /// all cost item(it is always shown)
        /// </summary>
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
        /// <summary>
        /// all the item one which belongs income(they are shown when all income item is selected)
        /// </summary>
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
        /// <summary>
        /// all the item one which belongs cost(they are shown when all cost item is selected)
        /// </summary>
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
        /// <summary>
        /// all the item two which belong to selected income item one
        /// </summary>
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
        /// <summary>
        /// all the item two which belong to selected cost item one
        /// </summary>
        public ObservableCollection<StatisticItemViewModel> CostItemTwos
        {
            get { return _costItemTwos; }
            set
            {
                _costItemTwos = value;
                RaisePropertyChanged("CostItemTwos");
            }
        } 

        /// <summary>
        /// bll statistic process
        /// </summary>
        private IStatiticProcess _statisticProcess;
        private bool _isIncomeSelected, _ItemOneMultiSelected;
        private ItemCollectionController _incomeItemOneController, _incomeItemTwoController,
            _costItemOneController, _costItemTwoController,
            _allIncomeController, _allCostController;

        public StatisticItemSelecterViewModel(IStatiticProcess process)
        {
            _statisticProcess = process;
            _statisticProcess.ItemCollectionAddEvent += OnItemCollectionAdd;
            _statisticProcess.ItemCollectionClearEvent += OnItemCollectionClear;
            _statisticProcess.ItemSelectEvent += OnItemSelected;

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

            _incomeItemOneController.StatisticItemsListOperation += OnStatisticItemOperated;
            _incomeItemTwoController.StatisticItemsListOperation += OnStatisticItemOperated;
            _costItemOneController.StatisticItemsListOperation += OnStatisticItemOperated;
            _costItemTwoController.StatisticItemsListOperation += OnStatisticItemOperated;
            _allIncomeController.StatisticItemsListOperation += OnStatisticItemOperated;
            _allCostController.StatisticItemsListOperation += OnStatisticItemOperated;

            _statisticProcess.InitializeItemOnes();
        }

        /// <summary>
        /// After bll process, some statistic item is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemSelected(object sender, SelectItemArgs e)
        {
            ItemCollectionIdentify(e.ItemInfo.IsIncome, e.ItemInfo.ItemType).SelectItems(e);
        }

        /// <summary>
        /// This method is called when available statistic item is seleted or unseleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnStatisticItemOperated(object sender, StatisticItemsListOperationEventArgs e)
        {
            if (StatisticItemOperatedEvent != null)
            {
                StatisticItemOperatedEvent(sender, e);
            }
        }        

        /// <summary>
        /// Get the suitable item colletion controller
        /// </summary>
        /// <param name="isIncome"></param>
        /// <param name="itemType"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Find the suitable controller, then clear all the items which it controls.
        /// This method is called when bll process raise the clear event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemCollectionClear(object sender, ClearItemsArgs e)
        {
            ItemCollectionIdentify(e.IsIncome, e.ClearedItemType).ClearItems(e);
        }

        /// <summary>
        /// Find the suitable controller, then add the items.
        /// This method is called when allitem or item one is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemCollectionAdd(object sender, ItemCollectionOperationArgs e)
        {
            ItemCollectionIdentify(e.IsIncome, e.ItemOneCollection != null ? ItemType.ItemOne : ItemType.ItemTwo).AddItems(e);
        }

        /// <summary>
        /// When the statistic item is selected,this method will be called.
        /// This is from item collection controller.
        /// bll process will do the following work
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnStatisticItemSelected(object sender, SelectStatisticItemEventArgs e)
        {
            _statisticProcess.ProceedSelectedItem(e);
        }
    }
}
