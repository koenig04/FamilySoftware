using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ActionDictionary;

namespace BLL
{
    public class BussinessManager : IBussiness
    {
        /// <summary>
        /// ItemConfig对应的业务逻辑类
        /// </summary>
        private ItemProcess m_itemProcess;
        /// <summary>
        /// Action字典
        /// 存放了所有的操作，使用查表的方法来调用相应的方法
        /// </summary>
        private ActionDictionaryProcess m_actionDic;

        /// <summary>
        /// ItemConfig操作的结果回调事件
        /// </summary>
        public event EventHandler<ItemConfigureEventArgs> ItemConfigureEvent;

        public BussinessManager()
        {
            m_itemProcess = new ItemProcess();

            m_actionDic = new ActionDictionaryProcess(ref m_itemProcess);
            m_actionDic.ItemConfigEvent += m_actionDic_ItemConfigEvent;
        }

        void m_actionDic_ItemConfigEvent(object sender, ItemConfigureEventArgs e)
        {
            if (ItemConfigureEvent != null)
            {
                ItemConfigureEvent(null, e);
            }
        }

        /// <summary>
        /// ItemConfig操作
        /// </summary>
        /// <param name="OpType"></param>
        /// <param name="ItemType"></param>
        /// <param name="ItemInfo"></param>
        public void ItemConfigure(Common.OperationType OpType, Common.ItemType ItemType, object ItemInfo, string IconFullPath = "")
        {
            m_actionDic.ItemProcess(OpType, ItemType, ItemInfo);
        }




    }
}
