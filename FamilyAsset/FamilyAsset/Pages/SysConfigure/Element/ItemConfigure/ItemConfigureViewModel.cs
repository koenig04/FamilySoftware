using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
    class ItemConfigureViewModel : UserControlViewModelBase
    {
        private IItemConfigureProcess _bussinessProcess;

        public ItemConfigureViewModel()
        {
            this._bussinessProcess = new ItemConfigureProcessManager();
            this._bussinessProcess.ItemSearchedResultEvent += OnItenSearchedResult;
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
            RaiseUserControlMessageEvent( new UserControlMessageEventArgs()
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
        private void OnItemChanged(ItemChangedInfoArgs e)
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
            switch (e.OperationType)
            {
                case OperationType.Add:
                    ItemViewModel newItem = new ItemViewModel(ItemType.ItemOne, e.ItemInfo, m_inorout);
                    newItem.ItemClickedEvent += OnItemClicked;
                    ItemOnes.Add(newItem);
                    break;
                case OperationType.Delete:
                    ItemOnes.Remove(ItemOnes.Where(a => a.SelectedItem.ItemID == ((ItemConfigureOperationInfo)e.ItemInfo).ItemInfo.ItemID).FirstOrDefault());
                    break;
                case OperationType.Modify:
                    int index = ItemOnes.IndexOf(ItemOnes.Where(a => a.SelectedItem.ItemID == ((ItemConfigureOperationInfo)e.ItemInfo).ItemInfo.ItemID).FirstOrDefault());
                    if (index >= 0)
                        ItemOnes[index] = new ItemViewModel(ItemType.ItemOne, e.ItemInfo, m_inorout);
                    break;
            }
        }

        //Do the del/add/modify operation of item two
        private void OperateItemTwoList(ItemChangedInfoArgs e)
        {
            switch (e.OperationType)
            {
                case OperationType.Add:
                    ItemViewModel newItem = new ItemViewModel(ItemType.ItemTwo, e.ItemInfo, m_inorout);
                    newItem.ItemClickedEvent += OnItemClicked;
                    ItemTwos.Add(newItem);
                    break;
                case OperationType.Delete:
                    ItemTwos.Remove(ItemTwos.Where(a => a.SelectedItem.ItemID == ((ItemConfigureOperationInfo)e.ItemInfo).ItemInfo.ItemID).FirstOrDefault());
                    break;
                case OperationType.Modify:
                    int index = ItemTwos.IndexOf(ItemTwos.Where(a => a.SelectedItem.ItemID == ((ItemConfigureOperationInfo)e.ItemInfo).ItemInfo.ItemID).FirstOrDefault());
                    if (index >= 0)
                        ItemTwos[index] = new ItemViewModel(ItemType.ItemTwo, e.ItemInfo, m_inorout);
                    break;
            }
        }

        //Do the del/add/modify operation of phrase
        private void OperatePhraseList(ItemChangedInfoArgs e)
        {
            switch (e.OperationType)
            {
                case OperationType.Add:
                    ItemViewModel newItem = new ItemViewModel(ItemType.Phrase, e.ItemInfo, m_inorout);
                    newItem.ItemClickedEvent += OnItemClicked;
                    Phrases.Add(newItem);
                    break;
                case OperationType.Delete:
                    Phrases.Remove(Phrases.Where(a => a.SelectedItem.ItemID == ((ItemConfigureOperationInfo)e.ItemInfo).ItemInfo.ItemID).FirstOrDefault());
                    break;
                case OperationType.Modify:
                    int index = Phrases.IndexOf(Phrases.Where(a => a.SelectedItem.ItemID == ((ItemConfigureOperationInfo)e.ItemInfo).ItemInfo.ItemID).FirstOrDefault());
                    if (index >= 0)
                        Phrases[index] = new ItemViewModel(ItemType.Phrase, e.ItemInfo, m_inorout);
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
                            ItemViewModel ivm = new ItemViewModel(ItemType.ItemOne, item, m_inorout);
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
                            ItemViewModel ivm = new ItemViewModel(ItemType.ItemTwo, item, m_inorout);
                            ivm.ItemClickedEvent += OnItemClicked;
                            this.ItemTwos.Add(ivm);
                        }
                    }
                    break;
                case ItemType.Phrase://Phrase list
                    this.Phrases.Clear();
                    if (e.ItemCollection != null && (e.ItemCollection as Hashtable).ContainsKey("Phrase"))
                    {
                        foreach (Phrase item in (e.ItemCollection as Hashtable)["Phrase"] as List<Phrase>)
                        {
                            ItemViewModel ivm = new ItemViewModel(ItemType.Phrase, item, m_inorout);
                            ivm.ItemClickedEvent += OnItemClicked;
                            this.Phrases.Add(ivm);
                        }
                    }
                    break;
            }
        }

        private void OnItemClicked(object sender, ItemClickedEventArgs e)
        {
            switch (e.SelectedItem.ItemType)
            {
                case ItemType.ItemOne:
                    foreach (ItemViewModel item in ItemOnes.Where(a => a.SelectedItem != e.SelectedItem))
                    {
                        item.ConvertToUnPressed();
                    }
                    m_selectedItemOne = new JZItemOne()
                    {
                        IconName = e.SelectedItem.ItemIcon,
                        IconNamePressed = e.SelectedItem.ItemIconPressed,
                        IncomeOrCost = m_inorout,
                        JZItemOneID = e.SelectedItem.ItemID,
                        JZItemOneName = e.SelectedItem.ItemName
                    };
                    m_selectedItemTwo = new JZItemTwo() { JZItemOneID = e.SelectedItem.ItemID };
                    m_selectedPhrase = new Phrase() { ItemID = e.SelectedItem.ItemID };
                    break;
                case ItemType.ItemTwo:
                    foreach (ItemViewModel item in ItemTwos.Where(a => a.SelectedItem != e.SelectedItem))
                    {
                        item.ConvertToUnPressed();
                    }
                    m_selectedItemTwo = new JZItemTwo()
                    {
                        IconName = e.SelectedItem.ItemIcon,
                        IconNamePressed = e.SelectedItem.ItemIconPressed,
                        JZItemOneID = m_selectedItemOne.JZItemOneID,
                        JZItemTwoID = e.SelectedItem.ItemID,
                        JZItemTwoName = e.SelectedItem.ItemName
                    };
                    m_selectedPhrase = new Phrase() { ItemID = e.SelectedItem.ItemID };
                    break;
                case ItemType.Phrase:
                    foreach (ItemViewModel item in Phrases.Where(a => a.SelectedItem != e.SelectedItem))
                    {
                        item.ConvertToUnPressed();
                    }
                    m_selectedPhrase = new Phrase()
                    {
                        ItemID = m_selectedItemTwo == null ? m_selectedItemOne.JZItemOneID : m_selectedItemTwo.JZItemTwoID,
                        PhraseID = e.SelectedItem.ItemID,
                        PhraseContent = e.SelectedItem.ItemName
                    };
                    break;
            }
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
                if (_ItemOnes == null)
                {
                    _ItemOnes = new ObservableCollection<ItemViewModel>();
                }
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
            get
            {
                if (_itemTwos == null)
                {
                    _itemTwos = new ObservableCollection<ItemViewModel>();
                }
                return _itemTwos;
            }
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
            get
            {
                if (_phrases == null)
                {
                    _phrases = new ObservableCollection<ItemViewModel>();
                }
                return _phrases;
            }
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
                ItemOnes.Clear();
                ItemTwos.Clear();
                Phrases.Clear();

                m_inorout = obj;
                this._bussinessProcess.HandleItemSelected(new ItemSelectedInfo()
                {
                    IsIncome = obj
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


        public override void HandleViewModelCallBack(ViewModelCallBackInfo callbackInfo)
        {
            if (callbackInfo.IsSucceed == true)
            {
                ItemConfigCallBackContext context = callbackInfo.Context as ItemConfigCallBackContext;
                OnItemChanged(context.Context);
            }
        }

    }

}
