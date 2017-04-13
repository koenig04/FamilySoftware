using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.SysConfigure.Element.ItemConfigure
{
    class InOutSwitchViewModel : NotificationObject
    {
        public event Action<bool> IsIncomeEvent;

        private bool _isIncomeChecked = true;
        /// <summary>
        /// 收入ToggleButton是否按下
        /// </summary>
        public bool IsIncomeChecked
        {
            get { return _isIncomeChecked; }
            set
            {
                _isIncomeChecked = value;
                RaisePropertyChanged("IsIncomeChecked");
            }
        }

        private bool _isExpandChecked = false;
        /// <summary>
        /// 支出ToggleButton是否按下
        /// </summary>
        public bool IsExpandChecked
        {
            get { return _isExpandChecked; }
            set
            {
                _isExpandChecked = value;
                RaisePropertyChanged("IsExpandChecked");
            }
        }

        private DelegateCommand _incomeButtonPressed;
        /// <summary>
        /// 收入ToggleButton按下操作
        /// </summary>
        public DelegateCommand IncomeButtonPressed
        {
            get
            {
                if (_incomeButtonPressed == null)
                {
                    _incomeButtonPressed = new DelegateCommand(new Action<object>(
                        o =>
                        {
                            ButtonPressed("Income");
                        }));
                }
                return _incomeButtonPressed;
            }
            set
            {
                _incomeButtonPressed = value;
                RaisePropertyChanged("IncomeButtonPressed");
            }
        }

        private DelegateCommand _expandButtonPressed;
        /// <summary>
        /// 支出ToggleButton按下操作
        /// </summary>
        public DelegateCommand ExpandButtonPressed
        {
            get
            {
                if (_expandButtonPressed == null)
                {
                    _expandButtonPressed = new DelegateCommand(new Action<object>(
                        o =>
                        {
                            ButtonPressed("Expand");
                        }));
                }
                return _expandButtonPressed;
            }
            set
            {
                _expandButtonPressed = value;
                RaisePropertyChanged("ExpandButtonPressed");
            }
        }


        private void ButtonPressed(string toggleButtonName)
        {
            //当ToggleButton按下时，先改变IsChecked状态，再触发点击事件
            //所以为了达到不能弹出的效果，必须在点击事件中把弹起状态改回去
            //即当检测到IsChecked是false时，代表发生了弹起事件，此时要把状态改回去，并且不触发事件
            //只有检测到IsChecked变为true了，代表发生了按下操作，改变ToggleButton状态，并触发事件
            if (toggleButtonName == "Income")
            {
                if (IsIncomeChecked)
                {
                    IsExpandChecked = false;
                    IsIncomeChecked = true;
                    if (IsIncomeEvent != null)
                        IsIncomeEvent(true);
                }
                else
                {
                    IsIncomeChecked = !IsIncomeChecked;
                }
            }
            else if (toggleButtonName == "Expand")
            {
                if (IsExpandChecked)
                {
                    IsExpandChecked = true;
                    IsIncomeChecked = false;
                    if (IsIncomeEvent != null)
                        IsIncomeEvent(false);
                }
                else
                {
                    IsExpandChecked = !IsExpandChecked;
                }
            }
        }
    }
}
