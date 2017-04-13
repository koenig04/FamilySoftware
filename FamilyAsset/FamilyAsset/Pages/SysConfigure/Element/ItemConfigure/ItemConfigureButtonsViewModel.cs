using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Common;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.SysConfigure.Element.ItemConfigure
{
    class ItemConfigureButtonsViewModel : NotificationObject
    {
        public event EventHandler<ItemConfigureButtonEventArgs> ItemConfigureButtonPressEvent;

        private ItemType m_itemType;

        private DelegateCommand _add;

        public DelegateCommand Add
        {
            get
            {
                if (_add == null)
                {
                    _add = new DelegateCommand(new Action<object>(
                        o =>
                        {
                            RaisEvent(new ItemConfigureButtonEventArgs(OperationType.Add, m_itemType));
                        }));
                }
                return _add;
            }
            set
            {
                _add = value;
                RaisePropertyChanged("Add");
            }
        }

        private DelegateCommand _modify;

        public DelegateCommand Modify
        {
            get
            {
                if (_modify == null)
                {
                    _modify = new DelegateCommand(new Action<object>(
                        o =>
                        {
                            RaisEvent(new ItemConfigureButtonEventArgs(OperationType.Modify, m_itemType));
                        }));
                }
                return _modify;
            }
            set
            {
                _modify = value;
                RaisePropertyChanged("Modify");
            }
        }

        private DelegateCommand _del;

        public DelegateCommand Del
        {
            get
            {
                if (_del == null)
                {
                    _del = new DelegateCommand(new Action<object>(
                        o =>
                        {
                            RaisEvent(new ItemConfigureButtonEventArgs(OperationType.Delete, m_itemType));
                        }));
                }
                return _del;
            }
            set
            {
                _del = value;
                RaisePropertyChanged("Del");
            }
        }

        private BitmapImage _ModifyButton;

        public BitmapImage ModifyButton
        {
            get
            {
                if (_ModifyButton == null)
                {
                    _ModifyButton = new BitmapImage(new Uri(GlobalVariables.systemIconPath.Replace("\\", "/") + "modify.png", UriKind.Absolute));
                }
                return _ModifyButton;
            }
            set
            {
                _ModifyButton = value;
                RaisePropertyChanged("ModifyButton");
            }
        }

        public ItemConfigureButtonsViewModel(ItemType Itemtype)
        {
            this.m_itemType = Itemtype;
        }

        private void RaisEvent(ItemConfigureButtonEventArgs e)
        {
            if (ItemConfigureButtonPressEvent != null)
            {
                ItemConfigureButtonPressEvent(this, e);
            }
        }
    }

    /// <summary>
    /// ItemConfig下方的增删改按钮的触发事件
    /// 因为目前选中的是哪个项目，在上一级控件中已经获得
    /// 所以此处只要能够返回用户点击的是哪个按钮即可
    /// </summary>
    class ItemConfigureButtonEventArgs : EventArgs
    {
        public OperationType Optype { get; private set; }
        public ItemType Itemtype { get; private set; }

        public ItemConfigureButtonEventArgs(OperationType Optype, ItemType Itemtype)
        {
            this.Optype = Optype;
            this.Itemtype = Itemtype;
        }
    }


}
