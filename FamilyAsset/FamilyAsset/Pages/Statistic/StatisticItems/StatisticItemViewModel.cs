using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BLL.StatisticProcess.StatisticItemRelative;
using Common;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.Statistic.StatisticItems
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

        public void SwitchSelectionStatus(bool isSelected)
        {
            if (isSelected)
            {
                isSelected = true;
                BorderColor = _isIncome ? Colors.LimeGreen : Colors.Firebrick;
            }
            else
            {
                IsSelected = false;
                BorderColor = Colors.Black;
            }
        }

        public void SwitchSelectable(bool selectable)
        {
            if (!selectable)
            {
                SwitchSelectionStatus(false);
            }
            _canSelected = selectable;
        }

        public static implicit operator StatisticItemViewModel(SelectedStatisticItemInfo model)
        {
            return new StatisticItemViewModel(model.IsIncome, model.ItemID);
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

        public static implicit operator SelectedStatisticItemInfo(SelectStatisticItemEventArgs e)
        {
            SelectedStatisticItemInfo res = new SelectedStatisticItemInfo();
            res.IsIncome = e.IsIncome;
            res.IsSelected = e.IsSelected;
            res.ItemID = e.ItemID;
            res.ItemType = e.ItemType;
            return res;
        }
    }
}
