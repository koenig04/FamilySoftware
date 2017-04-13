using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using BLL;
using Common;
using FamilyAsset.Context;
using FamilyAsset.UICore;

namespace FamilyAsset.PopupWindow.SysConfigure
{
    class ItemModifyPopWindowViewModel : ViewModelBase
    {
        private string _itemModifyTitle;
        /// <summary>
        /// 弹窗左上角的标题栏
        /// </summary>
        public string ItemModifyTitle
        {
            get { return _itemModifyTitle; }
            set
            {
                _itemModifyTitle = value;
                RaisePropertyChanged("ItemModifyTitle");
            }
        }

        private PopWindowItemViewModel _item1;
        /// <summary>
        /// 左侧第一栏（一级条目的名称）
        /// </summary>
        public PopWindowItemViewModel Item1
        {
            get { return _item1; }
            set
            {
                _item1 = value;
                RaisePropertyChanged("Item1");
            }
        }

        private PopWindowItemViewModel _item2;
        /// <summary>
        /// 左侧第二栏（二级条目的名称）
        /// </summary>
        public PopWindowItemViewModel Item2
        {
            get { return _item2; }
            set
            {
                _item2 = value;
                RaisePropertyChanged("Item2");
            }
        }

        private PopWindowItemViewModel _phrase;
        /// <summary>
        /// 左侧第三栏（常用语的名称）
        /// </summary>
        public PopWindowItemViewModel Phrase
        {
            get { return _phrase; }
            set
            {
                _phrase = value;
                RaisePropertyChanged("Phrase");
            }
        }

        private BitmapImage _img;
        /// <summary>
        /// 该条目对应的图标
        /// </summary>
        public BitmapImage Img
        {
            get { return _img; }
            set
            {
                _img = value;
                RaisePropertyChanged("Img");
            }
        }

        private DelegateCommand _cmdConfirm;
        /// <summary>
        /// 按下确定键对应的操作
        /// </summary>
        public virtual DelegateCommand CmdConfirm
        {
            get { return _cmdConfirm; }
            set
            {
                _cmdConfirm = value;
                RaisePropertyChanged("CmdConfirm");
            }
        }

        private DelegateCommand _cmdCancel;
        /// <summary>
        /// 按下取消键对应的操作（关闭当前窗口）
        /// </summary>
        public DelegateCommand CmdCancel
        {
            get
            {
                if (_cmdCancel == null)
                {
                    _cmdCancel = new DelegateCommand(new Action<object>(
                        o =>
                        {
                            MsgManager.SendMsg<ViewModelCallBackInfo>("CloseWindow", null);
                        }));
                }
                return _cmdCancel;
            }
            set
            {
                _cmdCancel = value;
                RaisePropertyChanged("CmdConfirm");
            }
        }

        private DelegateCommand _loadImg;
        /// <summary>
        /// 按下载入图片对应的操作
        /// 打开文件管理器，选择图片
        /// 选择的图片会显示到窗口上
        /// </summary>
        public DelegateCommand LoadImg
        {
            get
            {
                if (_loadImg == null)
                {
                    _loadImg = new DelegateCommand(new Action<object>(
                        o =>
                        {
                            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
                            ofd.DefaultExt = ".png";
                            ofd.Filter = "Image File|*.png;*.jpg;*.bmp";
                            if (ofd.ShowDialog() == true)
                            {
                                Img = new BitmapImage(new Uri(ofd.FileName.Replace("\\", "/"), UriKind.Absolute));
                                m_iconName = ofd.SafeFileName;
                                m_iconFullPath = ofd.FileName;
                            }
                        }));
                }
                return _loadImg;
            }
            set
            {
                _loadImg = value;
                RaisePropertyChanged("LoadImg");
            }
        }


        private Visibility _confirmVisibility = Visibility.Visible;
        /// <summary>
        /// 确认按钮是否可见（是）
        /// </summary>
        public Visibility ConfirmVisibility
        {
            get { return _confirmVisibility; }
            set
            {
                _confirmVisibility = value;
                RaisePropertyChanged("ConfirmVisibility");
            }
        }

        private Visibility _cancelVisibility = Visibility.Visible;
        /// <summary>
        /// 取消按钮是否可见（是）
        /// </summary>
        public Visibility CancelVisibility
        {
            get { return _cancelVisibility; }
            set
            {
                _cancelVisibility = value;
                RaisePropertyChanged("CancelVisibility");
            }
        }

        private bool _isLoadImgAvailable;
        /// <summary>
        /// 图像载入按钮是否可用
        /// </summary>
        public bool IsLoadImgAvailable
        {
            get { return _isLoadImgAvailable; }
            set
            {
                _isLoadImgAvailable = value;
                RaisePropertyChanged("IsLoadImgAvailable");
            }
        }


        //业务处理类
        protected IBussiness m_bussiness;
        //一级条目对应的数据Model
        protected Model.JZItemOne m_item1;
        //二级条目对应的数据Model
        protected Model.JZItemTwo m_item2;
        //常用语对应的数据Model
        protected Model.Phrase m_phrase;
        //操作类型（增、删、改、查）
        protected Common.OperationType m_opType;
        //图标文件名称
        protected string m_iconName;
        //图标完整路径
        private string m_iconFullPath;

        public override void SetContext(IContext Context)
        {
            ItemConfigPopWindowContext context = Context as ItemConfigPopWindowContext;
            this.m_bussiness = context.Bussiness;
            this.m_opType = context.OpType;
            this.m_item1 = context.ItemOne;
            this.m_item2 = context.ItemTwo;
            this.m_phrase = context.Phrase;

            //设置标题栏和是否可以载入图标
            switch (context.OpType)
            {
                case Common.OperationType.Add:
                    ItemModifyTitle = "添加";
                    IsLoadImgAvailable = true;
                    break;
                case Common.OperationType.Delete:
                    ItemModifyTitle = "删除";
                    IsLoadImgAvailable = false;
                    break;
                case Common.OperationType.Modify:
                    ItemModifyTitle = "修改";
                    IsLoadImgAvailable = true;
                    break;
                case Common.OperationType.Search:
                    ItemModifyTitle = "查找";
                    break;
            }

            //响应Item的增删改
            this.m_bussiness.ItemConfigureEvent -= m_bussiness_ItemConfigureEvent;
            this.m_bussiness.ItemConfigureEvent += m_bussiness_ItemConfigureEvent;
        }

        void m_bussiness_ItemConfigureEvent(object sender, ItemConfigureEventArgs e)
        {
            if (e.OptType != Common.OperationType.Search)
            {
                string msg = string.Empty;
                msg += CommonEnumChsConverter.Instance.OperationTypeConvert(e.OptType);
                msg += CommonEnumChsConverter.Instance.ItemTypeConvert(e.ItemType);
                msg += (bool)e.ItemInfo ? "成功" : "失败";
                MsgManager.SendMsg<GeneralPopWindowContext>("ShowResult", new GeneralPopWindowContext() { Msg = msg });
            }
        }

        protected virtual void UpdateItemInfo()
        {
            //m_item1 = new Model.JZItemOne();
            //m_item2 = new Model.JZItemTwo();
            //m_phrase = new Model.Phrase();
            if (m_item1 != null)
                m_item1.JZItemOneName = Item1 != null ? Item1.ItemValue : null;
            if (m_item2 != null)
                m_item2.JZItemTwoName = Item2 != null ? Item2.ItemValue : null;
            if (m_phrase != null)
                m_phrase.PhraseContent = Phrase != null ? Phrase.ItemValue : null;
        }

        public override void ViewModelCallBack(ViewModelCallBackInfo Info)
        {
            if (Info.IsSucceed)
            {
                string dstFile = Common.GlobalVariables.iconPath + m_iconName;
                File.Copy(m_iconFullPath, dstFile, true);               
            }
        }
    }
}
