﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BLL;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.Statistic.AccountDetail
{
    class AccountSortDetailViewModel : AccountDetailBase
    {
        private BitmapImage _itemImg;

        public BitmapImage ItemImg
        {
            get { return _itemImg; }
            set
            {
                _itemImg = value;
                RaisePropertyChanged("ItemImg");
            }
        }

        private int _recLength;

        public int RecLength
        {
            get { return _recLength; }
            set
            {
                _recLength = value;
                RaisePropertyChanged("RecLength");
            }
        }

        private Color _recColor;

        public Color RecColor
        {
            get { return _recColor; }
            set
            {
                _recColor = value;
                RaisePropertyChanged("RecColor");
            }
        }

        private string _itemName;

        public string ItemName
        {
            get { return _itemName; }
            set
            {
                _itemName = value;
                RaisePropertyChanged("ItemName");
            }
        }

        private string _itemPrecent;

        public string itemPrecent
        {
            get { return _itemPrecent; }
            set
            {
                _itemPrecent = value;
                RaisePropertyChanged("ItemPrecent");
            }
        }

        private string _itemTotal;

        public string ItemTotal
        {
            get { return _itemTotal; }
            set
            {
                _itemTotal = value;
                RaisePropertyChanged("ItemTotal");
            }
        }

        private Color _itemTotalColor;

        public Color ItemTotalColor
        {
            get { return _itemTotalColor; }
            set
            {
                _itemTotalColor = value;
                RaisePropertyChanged("ItemTotalColor");
            }
        }

        private string _itemAccountCount;

        public string ItemAccountCount
        {
            get { return _itemAccountCount; }
            set
            {
                _itemAccountCount = value;
                RaisePropertyChanged("ItemAccountCount");
            }
        }

        private DelegateCommand _itemClicked;

        public DelegateCommand ItemClicked
        {
            get
            {
                if (_itemClicked == null)
                {
                    _itemClicked = new DelegateCommand(
                        o =>
                        {
                            RaiseItemClickedEvent(new StatisticItemClickedEvnetArgs()
                            {
                                ItemID = _sortID,
                                ItemType = 0
                            });
                        });
                }
                return _itemClicked;
            }
            set
            {
                _itemClicked = value;
                RaisePropertyChanged("ItemClicked");
            }
        }


        private int _totalRecLength = 450;
        private string _sortID;

        public AccountSortDetailViewModel(StatisticBySortListItem sortInfo, decimal totalAmount, Color recColor, int accountCount)
        {
            ItemImg = new BitmapImage(new Uri(Common.GlobalVariables.iconPath.Replace("\\", "/") + sortInfo.ItemIcon,
                UriKind.RelativeOrAbsolute));
            RecLength = (int)(sortInfo.ItemAmount / totalAmount * _totalRecLength);
            RecColor = RecColor;
            ItemName = sortInfo.ItemName;
            itemPrecent = (sortInfo.ItemAmount / totalAmount).ToString() + "%";
            ItemTotal = sortInfo.ItemAmount.ToString() + "元";
            ItemTotalColor = sortInfo.isIncome ? Colors.Firebrick : Colors.LimeGreen;
            ItemAccountCount = accountCount.ToString() + "笔";
            _sortID = sortInfo.ItemID;
        }
    }
}