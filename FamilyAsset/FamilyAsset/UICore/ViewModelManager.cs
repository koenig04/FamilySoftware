using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Common;
using FamilyAsset.PopupWindow;
using FamilyAsset.PopupWindow.Message;
using FamilyAsset.PopupWindow.SysConfigure;

namespace FamilyAsset.UICore
{
    class ViewModelManager
    {
        private static List<ViewModelInfo> _viewModelInfoList = new List<ViewModelInfo>();                      //ViewModel信息列表
        private static Stack<ViewModelBase> _viewModelStack = new Stack<ViewModelBase>();

        /// <summary>
        /// ViewModel信息注册
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <typeparam name="TViewModel"></typeparam>
        /// <typeparam name="TMsgRegister"></typeparam>
        /// <param name="componentsDic"></param>
        public static void Register<TViewType, TViewModel, TMsgRegister>(string identityFlag = "")
        {
            ViewModelInfo vmInfo = new ViewModelInfo(
                typeof(TViewType),
                typeof(TViewModel),
                typeof(TMsgRegister),
                identityFlag
                );
            _viewModelInfoList.Add(vmInfo);
        }

        /// <summary>
        /// 根据View获得对应的ViewModel
        /// </summary>
        /// <typeparam name="TView">View</typeparam>
        /// <returns></returns>
        public static object GetViewModel<TView>()
        {
            try
            {
                var vmType = GetViewModelInfo(typeof(TView)).ViewModelType;
                return vmType.Assembly.CreateInstance(vmType.FullName);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 根据View，创建相对应的ViewModel，Message，并完成绑定和各自的初始化
        /// </summary>
        /// <param name="fe">View</param>
        public static void SetViewModel(FrameworkElement fe, IContext context, string identityFlag = "")
        {
            //获得ViewModel信息
            var vmInfo = GetViewModelInfo(fe.GetType(), identityFlag);
            if (vmInfo == null) return;
            //获得ViewModel
            var vm = vmInfo.GetViewModelInstance();
            vm.SetContext(context);
            //把View的线程调度设置给ViewModel
            vm.UIDispatcher = fe.Dispatcher;
            //绑定View与ViewModel
            fe.DataContext = vm;
            //实例化并注册对应的Message
            var msgRegister = vmInfo.GetMsgRegisterInstance();
            if (msgRegister == null) return;
            msgRegister.RegInstance = fe;
            vm.MsgManager = new MessageManager();
            msgRegister.MsgManager = vm.MsgManager;
            msgRegister.Register();
            //将新生成的ViewModel压入堆栈
            _viewModelStack.Push(vm);
        }

        private static ViewModelInfo GetViewModelInfo(Type viewType, string identityFlag = "")
        {
            try
            {
                return _viewModelInfoList
                    .Where(p => p.ViewType == viewType && p.IdentityFlag == identityFlag)
                    .First();
            }
            catch
            {
                return null;
            }
        }

        public static void RegisterViewModel(FrameworkElement view, ViewModelBase viewmodel, MessageRegisterBase msgRegister = null)
        {
            if (view == null || viewmodel == null) return;
            //设定数据环境
            view.DataContext = viewmodel;
            //设置ViewModel的Dispatcher
            viewmodel.UIDispatcher = view.Dispatcher;

            //无需注册消息则直接返回
            if (msgRegister == null) return;
            if (msgRegister.RegInstance == null)
                msgRegister.RegInstance = view;

            viewmodel.MsgManager = msgRegister.MsgManager;

            var win = view as Window;
            if (win != null)
                win.Closed += msgRegister.MsgManager.WindowClose;
            //注册消息
            msgRegister.Register();
        }

        public static void CloseWindow(ViewModelCallBackInfo Info)
        {
            //弹出最顶端的ViewModel
            _viewModelStack.Pop();
            //向新的最顶端ViewModel发送回调信息
            _viewModelStack.Peek().ViewModelCallBack(Info);
        }

        public static void RegisteAll()
        {
            Register<MainWindow, MainViewModel, MainMessage>();
            Register<ItemModifyPopWindow, ItemModifyViewModel_Item1, ItemConfigPopWindowMessage>(ItemType.ItemOne.ToString());
            Register<ItemModifyPopWindow, ItemModifyViewModel_Item2, ItemConfigPopWindowMessage>(ItemType.ItemTwo.ToString());
            Register<ItemModifyPopWindow, ItemModifyViewModel_Phrase, ItemConfigPopWindowMessage>(ItemType.Phrase.ToString());
            Register<GeneralPopWindow, GeneralPopWindowViewModel, MessageBase>();
        }
    }
}
