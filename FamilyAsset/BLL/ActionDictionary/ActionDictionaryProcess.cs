using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DAL;

namespace BLL.ActionDictionary
{
    class ActionDictionaryProcess
    {
        public event EventHandler<ItemConfigureEventArgs> ItemConfigEvent;

        private Dictionary<ItemConfigActionKey, Action<object>> m_itemconfigDic;
        private ItemProcess m_itemProcess;

        public ActionDictionaryProcess(ref ItemProcess itemProcess)
        {
            m_itemProcess = itemProcess;
            m_itemconfigDic = new ItemConfigDictionary().Init(m_itemProcess,
                new Action<ItemConfigureEventArgs>(
                e =>
                {
                    RaiseEvent(e);
                }));
        }

        private void RaiseEvent(ItemConfigureEventArgs e)
        {
            if (ItemConfigEvent != null)
            {
                ItemConfigEvent(null, e);
            }
        }

        public void ItemProcess(OperationType opType, ItemType itemType, object ItemInfo)
        {
            Action<object> action = (from d in m_itemconfigDic.AsEnumerable()
                                     where d.Key.OpType == opType && d.Key.ItemType == itemType
                                     select d.Value).FirstOrDefault();
            if (action != null)
            {
                action(ItemInfo);
            }
        }



        public class ItemConfigActionKey
        {
            public OperationType OpType { get; private set; }
            public ItemType ItemType { get; private set; }

            public ItemConfigActionKey(OperationType OpType, ItemType ItemType)
            {
                this.OpType = OpType;
                this.ItemType = ItemType;
            }
        }


    }
}
