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

        private Color _itemBackColor;

        public Color ItemBackColor
        {
            get { return _itemBackColor; }
            set
            {
                _itemBackColor = value;
                RaisePropertyChanged("ItemBackColor");
            }
        }

        private Color _itemForeColor;

        public Color ItemForeColor
        {
            get { return _itemForeColor; }
            set
            {
                _itemForeColor = value;
                RaisePropertyChanged("ItemForeColor");
            }
        }


        private string _itemContent;

        public string ItemContent
        {
            get { return _itemContent; }
            set
            {
                _itemContent = value;
                RaisePropertyChanged("ItemContent");
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
                                _isSelected = !_isSelected;
                                if (_isSelected)
                                {
                                    ItemForeColor = Colors.White;
                                    ItemBackColor = _isIncome ? Colors.Lime : Colors.Firebrick;
                                }
                                else
                                {
                                    ItemForeColor = _isIncome ? Colors.Lime : Colors.Firebrick;
                                    ItemBackColor = Colors.White;
                                }                                
                                RaiseStatisticItemSelectedEvent();
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
        public string ItemID;
        private string _itemID;
        private bool _isIncome, _canSelected;

        public StatisticItemViewModel(bool isIncome, string itemID, string itemName)
        {
            this._itemID = itemID;
            ItemID = itemID;
            this._isIncome = isIncome;
            _canSelected = true;
            ItemContent = itemName;
            ItemBackColor = Colors.White;
            if (isIncome)
            {
                BorderColor = Colors.Lime;
                ItemForeColor = Colors.Lime;
            }
            else
            {
                BorderColor = Colors.Tomato;
                ItemForeColor = Colors.Tomato;
            }
        }

        public void SwitchSelectionStatus(bool isSelected)
        {
            if (isSelected)
            {
                isSelected = true;
                ItemForeColor = Colors.White;
                ItemBackColor = _isIncome ? Colors.LimeGreen : Colors.Firebrick;
            }
            else
            {
                _isSelected = false;
                ItemForeColor = _isIncome ? Colors.LimeGreen : Colors.Firebrick;
                ItemBackColor = Colors.White;
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
            return new StatisticItemViewModel(model.IsIncome, model.ItemID, model.ItemName);
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
