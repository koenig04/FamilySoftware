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
using BLL.ItemConfigureProcess;
using Common;
using FamilyAsset.Context;
using FamilyAsset.PopupWindow.SysConfigure;
using FamilyAsset.UICore;
using Model;

namespace FamilyAsset.Pages.SysConfigure.Element.ItemConfigure
{
    class ItemConfigureViewModel : NotificationObject, IUserControlViewModel
    {
        public event EventHandler<UserControlMessageEventArgs> UserControlMessageEvent;

        private IItemConfigureProcess _bussinessProcess;

        public ItemConfigureViewModel()
        {
            this._bussinessProcess = new ItemConfigureProcessManager();
            this._bussinessProcess.ItemSearchedResultEvent += OnItenSearchedResult;
            this._bussinessProcess.ItemChangedEvent += OnItemChanged;
            this._bussinessProcess.ItemPopWindowEvent += OnPopWindow;
            //Load all the ItemOne
            try
            {
                this._bussinessProcess.HandleItemSelected(new ItemSelectedInfo() { IsIncome = true });
            }
            catch { }
        }

        private void OnPopWindow(object sender, ItemPopWindowInfoArgs e)
        {
            UserControlMessageEvent(null, new UserControlMessageEventArgs()
            {
                Message = "PopItemConfigure",
                Context = new ItemConfigPopWindowContext()
                {
                    Bussiness = _bussinessProcess,
                    OpType = e.OperationType,
                    ItemType = e.ItemType,
                    ItemOne = m_selectedItemOne ?? new Model.JZItemOne() { IncomeOrCost = m_inorout },
                    ItemTwo = m_selectedItemTwo,
                    Phrase = m_selectedPhrase
                }
            });
        }

       

        //Do the del/add/modify operation of item
        private void OnItemChanged(object sender, ItemChangedInfoArgs e)
        {
            switch (e.ItemType)
            {
                case ItemType.ItemOne:
                    OperateItemOneList(e);
                    break;
                case ItemType.ItemTwo:
                    OperateItemTwoList(e);
                    break;
                case ItemType.Phrase:
                    OperatePhraseList(e);
                    break;
            }
        }

        //Do the del/add/modify operation of item one
        private void OperateItemOneList(ItemChangedInfoArgs e)
        {
            JZItemOne model = e.ItemInfo as JZItemOne;
            switch (e.OperationType)
            {
                case OperationType.Add:
                    ItemOnes.Add(new ItemViewModel(
                                ItemType.ItemOne, model.JZItemOneID, model.IconName, model.JZItemOneName));
                    break;
                case OperationType.Delete:
                    ItemOnes.Remove(ItemOnes.Where(a => a.SelectedItem.ItemID == model.JZItemOneID).FirstOrDefault());
                    break;
                case OperationType.Modify:
                    int index = ItemOnes.IndexOf(ItemOnes.Where(a => a.SelectedItem.ItemID == model.JZItemOneID).FirstOrDefault());
                    if (index >= 0)
                        ItemOnes[index] = new ItemViewModel(ItemType.ItemOne, model.JZItemOneID, model.IconName, model.JZItemOneName);
                    break;
            }
        }

        //Do the del/add/modify operation of item two
        private void OperateItemTwoList(ItemChangedInfoArgs e)
        {
            JZItemTwo model = e.ItemInfo as JZItemTwo;
            switch (e.OperationType)
            {
                case OperationType.Add:
                    ItemTwos.Add(new ItemViewModel(
                                ItemType.ItemTwo, model.JZItemTwoID, model.IconName, model.JZItemTwoName));
                    break;
                case OperationType.Delete:
                    ItemTwos.Remove(ItemOnes.Where(a => a.SelectedItem.ItemID == model.JZItemTwoID).FirstOrDefault());
                    break;
                case OperationType.Modify:
                    int index = ItemTwos.IndexOf(ItemOnes.Where(a => a.SelectedItem.ItemID == model.JZItemTwoID).FirstOrDefault());
                    if (index >= 0)
                        ItemTwos[index] = new ItemViewModel(ItemType.ItemTwo, model.JZItemTwoID, model.IconName, model.JZItemTwoName);
                    break;
            }
        }

        //Do the del/add/modify operation of phrase
        private void OperatePhraseList(ItemChangedInfoArgs e)
        {
            Phrase model = e.ItemInfo as Phrase;
            switch (e.OperationType)
            {
                case OperationType.Add:
                    Phrases.Add(new ItemViewModel(
                                ItemType.Phrase, model.PhraseID, string.Empty, model.PhraseContent));
                    break;
                case OperationType.Delete:
                    Phrases.Remove(ItemOnes.Where(a => a.SelectedItem.ItemID == model.PhraseID).FirstOrDefault());
                    break;
                case OperationType.Modify:
                    int index = Phrases.IndexOf(ItemOnes.Where(a => a.SelectedItem.ItemID == model.PhraseID).FirstOrDefault());
                    if (index >= 0)
                        Phrases[index] = new ItemViewModel(ItemType.Phrase, model.PhraseID, string.Empty, model.PhraseContent);
                    break;
            }
        }

        //Display the searched list
        private void OnItenSearchedResult(object sender, ItemSearchedCollectionArgs e)
        {
            switch (e.ItemType)
            {
                case ItemType.ItemOne://Item One list
                    this.ItemOnes.Clear();
                    if (e.ItemCollection != null && (e.ItemCollection as Hashtable).ContainsKey("One"))
                    {
                        foreach (JZItemOne item in (e.ItemCollection as Hashtable)["One"] as List<JZItemOne>)
                        {
                            ItemViewModel ivm = new ItemViewModel(
                                ItemType.ItemOne, item.JZItemOneID, item.IconName, item.JZItemOneName);
                            ivm.ItemClickedEvent += OnItemClicked;
                            this.ItemOnes.Add(ivm);
                        }
                    }
                    break;
                case ItemType.ItemTwo://Item Two list
                    this.ItemTwos.Clear();
                    if (e.ItemCollection != null && (e.ItemCollection as Hashtable).ContainsKey("Two"))
                    {
                        foreach (JZItemTwo item in (e.ItemCollection as Hashtable)["Two"] as List<JZItemTwo>)
                        {
                            ItemViewModel ivm = new ItemViewModel(
                                ItemType.ItemTwo, item.JZItemTwoID, item.IconName, item.JZItemTwoName);
                            ivm.ItemClickedEvent += OnItemClicked;
                            this.ItemTwos.Add(ivm);
                        }
                    }
                    break;
                case ItemType.Phrase://Phrase list
                    this.Phrases.Clear();
                    if (e.ItemCollection != null && (e.ItemCollection as Hashtable).ContainsKey("Phrase"))
                    {
                        foreach (Phrase item in (e.ItemCollection as Hashtable)["Two"] as List<Phrase>)
                        {
                            ItemViewModel ivm = new ItemViewModel(
                                ItemType.Phrase, item.PhraseID, string.Empty, item.PhraseContent);
                            ivm.ItemClickedEvent += OnItemClicked;
                            this.ItemTwos.Add(ivm);
                        }
                    }
                    break;
            }
        }

        private void OnItemClicked(object sender, ItemClickedEventArgs e)
        {
            _bussinessProcess.HandleItemSelected(new ItemSelectedInfo()
            {
                ItemType = e.SelectedItem.ItemType,
                IsIncome = m_inorout,
                ItemIcon = e.SelectedItem.ItemIcon,
                ItemID = e.SelectedItem.ItemID,
                ItemName = e.SelectedItem.ItemName
            });
        }

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
        /// <summary>
        /// 收入和支出切换的控件
        /// </summary>
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
                this._bussinessProcess.HandleItemOperation(new ItemConfigureOperationInfo()
                {
                    OperationType = OperationType.Search,
                    ItemInfo = new ItemSelectedInfo()
                    {
                        IsIncome = obj,
                        ItemType = ItemType.None
                    }
                });
            }
        }

        private Model.JZItemOne m_selectedItemOne;
        private Model.JZItemTwo m_selectedItemTwo;
        private Model.Phrase m_selectedPhrase;
        private bool m_inorout = true;        


        /// <summary>
        /// 用户按下列表下的按钮，弹出相应的窗口，进行增删改操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnButtonsPressed(object sender, ItemConfigureButtonEventArgs e)
        {
            _bussinessProcess.HandleItemValidOperation(new ItemConfigureOperationValidInfo()
            {
                Itemtype = e.Itemtype,
                Optype = e.Optype
            });
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
