using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Common;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.Statistic.NaviBar
{
    class StatisticItemViewModel : NotificationObject
    {
        public event EventHandler<SelectStatisticItemEventArgs> StatisticItemIsSelected;

        private Color _borderColor;

        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                RaisePropertyChanged("BorderColor");
            }
        }

        private DelegateCommand _itemClicked;

        public DelegateCommand ItemClicked
        {
            get
            {
                if (_itemClicked == null)
                {
                    _itemClicked = new DelegateCommand(new Action<object>(
                        o =>
                        {
                            if (_canSelected)
                            {
                                if (_isSelected)
                                {
                                    BorderColor = Colors.Black;
                                }
                                else
                                {
                                    BorderColor = _isIncome ? Colors.LimeGreen : Colors.Firebrick;
                                }
                                RaiseStatisticItemSelectedEvent();
                            }
                            else
                            {
                                IsSelected = !IsSelected;
                            }
                        }));
                }
                return _itemClicked;
            }
            set
            {
                _itemClicked = value;
                RaisePropertyChanged("ItemClicked");
            }
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
            }
        }


        private string _itemID;
        private bool _isIncome, _canSelected;

        public StatisticItemViewModel(bool isIncome, string itemID)
        {
            this._itemID = itemID;
            this._isIncome = isIncome;
            _canSelected = true;
        }

        public void ConverToUnselected()
        {
            IsSelected = false;
            BorderColor = Colors.Black;
        }

        public void SwitchSelectable(bool selectable)
        {
            if (!selectable)
            {
                ConverToUnselected();
            }
            _canSelected = selectable;
        }

        public static implicit operator StatisticItemViewModel(Model.JZItemOne model)
        {
            StatisticItemViewModel res = new StatisticItemViewModel(model.IncomeOrCost, model.JZItemOneID);
            return res;
        }

        public static StatisticItemViewModel ConvertFromJZTwo(Model.JZItemTwo model, bool isIncome)
        {
            StatisticItemViewModel res = new StatisticItemViewModel(isIncome, model.JZItemTwoID);
            return res;
        }

        private void RaiseStatisticItemSelectedEvent()
        {
            if (StatisticItemIsSelected != null)
            {
                StatisticItemIsSelected(null, new SelectStatisticItemEventArgs()
                {
                    IsIncome = _isIncome,
                    IsSelected = _isSelected,
                    ItemID = _itemID
                });
            }
        }
    }

    /// <summary>
    /// When AllIncome/AllCost is selected. ItemID is null
    /// </summary>
    class SelectStatisticItemEventArgs : EventArgs
    {
        public bool IsSelected { get; set; }
        public string ItemID { get; set; }
        public bool IsIncome { get; set; }
        public ItemType ItemType { get; set; }
    }
}
