using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BLL;
using Common;
using FamilyAsset.Context;
using FamilyAsset.PopupWindow.SysConfigure;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.SysConfigure.Element.ItemConfigure
{
    class ItemConfigureViewModel : NotificationObject, IUserControlViewModel
    {
        public event EventHandler<UserControlMessageEventArgs> UserControlMessageEvent;

        private ObservableCollection<ItemViewModel> _ItemOnes;
        /// <summary>
        /// 一级条目List
        /// </summary>
        public ObservableCollection<ItemViewModel> ItemOnes
        {
            get
            {
                return _ItemOnes;
            }
            set
            {
                _ItemOnes = value;
                RaisePropertyChanged("ItemOnes");
            }
        }

        private ObservableCollection<ItemViewModel> _itemTwos;
        /// <summary>
        /// 二级条目List
        /// </summary>
        public ObservableCollection<ItemViewModel> ItemTwos
        {
            get { return _itemTwos; }
            set
            {
                _itemTwos = value;
                RaisePropertyChanged("ItemTwos");
            }
        }

        private ObservableCollection<ItemViewModel> _phrases;
        /// <summary>
        /// 常用语List
        /// </summary>
        public ObservableCollection<ItemViewModel> Phrases
        {
            get { return _phrases; }
            set
            {
                _phrases = value;
                RaisePropertyChanged("Phrases");
            }
        }

        private ItemConfigureButtonsViewModel _itemOneButtons;
        /// <summary>
        /// 一级条目List下的按钮
        /// </summary>
        public ItemConfigureButtonsViewModel ItemOneButtons
        {
            get
            {
                if (_itemOneButtons == null)
                {
                    _itemOneButtons = new ItemConfigureButtonsViewModel(ItemType.ItemOne);
                    _itemOneButtons.ItemConfigureButtonPressEvent += OnButtonsPressed;
                }
                return _itemOneButtons;
            }
            set
            {
                _itemOneButtons = value;
                RaisePropertyChanged("ItemOneButtons");
            }
        }

        private ItemConfigureButtonsViewModel _itemTwoButtons;
        /// <summary>
        /// 二级条目List下的按钮
        /// </summary>
        public ItemConfigureButtonsViewModel ItemTwoButtons
        {
            get
            {
                if (_itemTwoButtons == null)
                {
                    _itemTwoButtons = new ItemConfigureButtonsViewModel(ItemType.ItemTwo);
                    _itemTwoButtons.ItemConfigureButtonPressEvent += OnButtonsPressed;
                }
                return _itemTwoButtons;
            }
            set
            {
                _itemTwoButtons = value;
                RaisePropertyChanged("ItemTwoButtons");
            }
        }

        private ItemConfigureButtonsViewModel _phraseButtons;
        /// <summary>
        /// 常用语List下的按钮
        /// </summary>
        public ItemConfigureButtonsViewModel PhraseButtons
        {
            get
            {
                if (_phraseButtons == null)
                {
                    _phraseButtons = new ItemConfigureButtonsViewModel(ItemType.Phrase);
                    _phraseButtons.ItemConfigureButtonPressEvent += OnButtonsPressed;
                }
                return _phraseButtons;
            }
            set
            {
                _phraseButtons = value;
                RaisePropertyChanged("PhraseButtons");
            }
        }

        private InOutSwitchViewModel _inOrOut;

        public InOutSwitchViewModel InOrOut
        {
            get
            {
                if (_inOrOut == null)
                {
                    _inOrOut = new InOutSwitchViewModel();
                    _inOrOut.IsIncomeEvent += _inOrOut_IsIncomeEvent;
                }
                return _inOrOut;
            }
            set
            {
                _inOrOut = value;
                RaisePropertyChanged("InOrOut");
            }
        }

        void _inOrOut_IsIncomeEvent(bool obj)
        {
            if (obj != m_inorout)
            {//支出收入切换
                //清除已选择的Item
                m_selectedItemOne = null;
                m_selectedItemTwo = null;
                m_selectedPhrase = null;
                m_inorout = obj;
                this._bussiness.ItemConfigure(OperationType.Search, ItemType.ItemOne, obj);
            }
            
        }


        private Model.JZItemOne m_selectedItemOne;
        private Model.JZItemTwo m_selectedItemTwo;
        private Model.Phrase m_selectedPhrase;
        private bool m_inorout = true;

        void OnItemSelected(object sender, ItemClickedEventArgs e)
        {
            switch (e.SelectedItem.ItemType)
            {
                case ItemType.ItemOne:
                    m_selectedItemOne = new Model.JZItemOne()
                    {
                        JZItemOneID = e.SelectedItem.ItemID,
                        JZItemOneName = e.SelectedItem.ItemName,
                        IconName = e.SelectedItem.ItemIcon
                    };
                    break;
                case ItemType.ItemTwo:
                    m_selectedItemTwo = new Model.JZItemTwo()
                    {
                        JZItemTwoID = e.SelectedItem.ItemID,
                        JZItemTwoName = e.SelectedItem.ItemName,
                        IconName = e.SelectedItem.ItemIcon
                    };
                    break;
                case ItemType.Phrase:
                    m_selectedPhrase = new Model.Phrase()
                    {
                        PhraseID = e.SelectedItem.ItemID,
                        PhraseContent = e.SelectedItem.ItemName
                    };
                    break;
            }
        }


        /// <summary>
        /// 用户按下列表下的按钮，弹出相应的窗口，进行增删改操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnButtonsPressed(object sender, ItemConfigureButtonEventArgs e)
        {
            if (UserControlMessageEvent != null)
            {
                if (checkActionValid(e))
                {
                    UserControlMessageEvent(null, new UserControlMessageEventArgs()
                    {
                        Message = "PopItemConfigure",
                        Context = new ItemConfigPopWindowContext()
                        {
                            Bussiness = _bussiness,
                            OpType = e.Optype,
                            ItemType = e.Itemtype,
                            ItemOne = m_selectedItemOne ?? new Model.JZItemOne() { IncomeOrCost = m_inorout },
                            ItemTwo = m_selectedItemTwo,
                            Phrase = m_selectedPhrase
                        }
                    });
                }
            }
        }

        private bool checkActionValid(ItemConfigureButtonEventArgs e)
        {
            bool res = true;
            if (e.Itemtype == ItemType.ItemOne)
            {
                //未选中时，不响应修改与删除
                if (m_selectedItemOne == null &&
                    (e.Optype == OperationType.Modify || e.Optype == OperationType.Delete))
                {
                    res = false;
                }
            }
            else if (e.Itemtype == ItemType.ItemTwo)
            {
                //未选中一级条目时，不响应添加；未选中二级条目时，不响应修改与删除
                if (m_selectedItemOne == null ||
                (m_selectedItemTwo == null && (e.Optype == OperationType.Modify || e.Optype == OperationType.Delete)))
                {
                    res = false;
                }
            }
            else
            {
                //未选中一级条目且未选中二级条目时，不响应添加；为选中常用语时，不响应修改与删除
                if ((m_selectedItemOne == null || m_selectedItemTwo == null) ||
                    (m_selectedPhrase == null && (e.Optype == OperationType.Modify || e.Optype == OperationType.Delete)))
                {
                    res = false;
                }
            }
            return res;
        }

        private IBussiness _bussiness;

        public ItemConfigureViewModel(ref IBussiness bussiness)
        {
            this._bussiness = bussiness;
            this._bussiness.ItemConfigureEvent += _bussiness_ItemConfigureEvent;
            //Load all the ItemOne
            try
            {
                this._bussiness.ItemConfigure(OperationType.Search, ItemType.ItemOne, true);
            }
            catch { }
        }

        void _bussiness_ItemConfigureEvent(object sender, ItemConfigureEventArgs e)
        {
            //ItemConif主界面只响应查找的返回
            if (e.OptType == OperationType.Search)
            {
                ReLoad(e.ItemType, e.ItemInfo);
            }
        }

        private void ReLoad(ItemType ItemType, object ItemInfo)
        {
            switch (ItemType)
            {
                //载入所有的一级条目
                case ItemType.ItemOne:

                    //清理一级条目List
                    if (ItemOnes != null)
                    {
                        ItemOnes.Clear();
                    }
                    else
                    {
                        ItemOnes = new ObservableCollection<ItemViewModel>();
                    }

                    //一级条目List重新赋值
                    if ((ItemInfo as Hashtable)["One"] != null)
                    {
                        List<ItemViewModel> lstOne = (from i in ((ItemInfo as Hashtable)["One"] as List<Model.JZItemOne>)
                                                      select new ItemViewModel()
                                                      {
                                                          ItemName = i.JZItemOneName,
                                                          ItemImg = new BitmapImage(new Uri(Common.GlobalVariables.iconPath.Replace("\\", "/") + i.IconName, UriKind.Absolute)),
                                                          ItemColor = m_inorout ? Colors.LimeGreen : Colors.Firebrick
                                                      }).ToList<ItemViewModel>();
                        foreach (ItemViewModel item in lstOne)
                        {
                            ItemOnes.Add(item);
                        }
                    }
                    break;

                //载入指定一级条目下辖的二级条目和常用语
                case ItemType.ItemTwo:
                    Hashtable hst = ItemInfo as Hashtable;

                    //清理二级条目和常用语List
                    if (Phrases != null)
                    {
                        Phrases.Clear();
                    }
                    else
                    {
                        Phrases = new ObservableCollection<ItemViewModel>();
                    }

                    if (ItemTwos != null)
                    {
                        ItemTwos.Clear();
                    }
                    else
                    {
                        ItemTwos = new ObservableCollection<ItemViewModel>();
                    }

                    //对二级条目和常用语List重新赋值
                    List<ItemViewModel> lst;
                    if (hst["Two"] != null)
                    {//此一级条目下有二级条目
                        lst = (from i in ((ItemInfo as Hashtable)["Two"] as List<Model.JZItemTwo>)
                               select new ItemViewModel()
                               {
                                   ItemName = i.JZItemTwoName,
                                   ItemImg = new BitmapImage(new Uri(Common.GlobalVariables.iconPath.Replace("\\", "/") + i.IconName, UriKind.Absolute)),
                                   ItemColor = m_inorout ? Colors.LimeGreen : Colors.Firebrick
                               }).ToList<ItemViewModel>();
                        foreach (ItemViewModel item in lst)
                        {
                            ItemTwos.Add(item);
                        }
                    }
                    if (hst["Phrase"] != null)
                    {//此一级条目下有常用语
                        lst = (from i in ((ItemInfo as Hashtable)["Phrase"] as List<Model.Phrase>)
                               select new ItemViewModel()
                               {
                                   ItemName = i.PhraseContent
                               }).ToList<ItemViewModel>();
                        foreach (ItemViewModel item in lst)
                        {
                            Phrases.Add(item);
                        }
                    }
                    break;

                //载入指定二级条目下的常用语
                case Common.ItemType.Phrase:

                    //清理常用语List
                    if (Phrases != null)
                    {
                        Phrases.Clear();
                    }
                    else
                    {
                        Phrases = new ObservableCollection<ItemViewModel>();
                    }

                    //对常用语List重新赋值
                    List<ItemViewModel> lstPhrase = (from i in ((ItemInfo as Hashtable)["Phrase"] as List<Model.Phrase>)
                                                     select new ItemViewModel()
                                                     {
                                                         ItemName = i.PhraseContent
                                                     }).ToList<ItemViewModel>();
                    foreach (ItemViewModel item in lstPhrase)
                    {
                        Phrases.Add(item);
                    }
                    break;
            }
        }

        public void HandleViewModelCallBack(ViewModelCallBackInfo callbackInfo)
        {
            if (callbackInfo.IsSucceed == true)
            {
                ItemConfigCallBackContext context = callbackInfo.Context as ItemConfigCallBackContext;
                switch (context.ItemType)
                {
                    case ItemType.ItemOne:
                        this._bussiness.ItemConfigure(OperationType.Search, ItemType.ItemOne, m_inorout);
                        break;
                    case ItemType.ItemTwo:
                        //this._bussiness.ItemConfigure(OperationType.Search,ItemType.ItemTwo,
                        break;
                }
            }
        }
    }

}
