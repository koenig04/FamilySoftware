using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model;

namespace BLL.ItemConfigureProcess
{
    public class ItemConfigureOperationValidInfo
    {
        public OperationType Optype { get; set; }
        public ItemType Itemtype { get; set; }
    }

    public class ItemSelectedInfo
    {
        public ItemType ItemType { get; set; }
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemIcon { get; set; }
        public bool IsIncome { get; set; }
    }

    public class CallBackInfo
    {
        public FunctionType FuncType { get; set; }
        public bool IsSucceed { get; set; }
        public IContext Context { get; set; }
    }

    public class ItemConfigureOperationInfo
    {
        public ItemSelectedInfo ItemInfo { get; set; }
        public OperationType OperationType { get; set; }

        public static implicit operator JZItemOne(ItemConfigureOperationInfo info)
        {
            JZItemOne item = new JZItemOne();
            item.JZItemOneID = info.ItemInfo.ItemID;
            item.JZItemOneName = info.ItemInfo.ItemName;
            item.IconName = info.ItemInfo.ItemIcon;
            item.IncomeOrCost = info.ItemInfo.IsIncome;
            return item;
        }

        public static implicit operator JZItemTwo(ItemConfigureOperationInfo info)
        {
            JZItemTwo item = new JZItemTwo();
            item.JZItemTwoID = info.ItemInfo.ItemID;
            item.JZItemTwoName = info.ItemInfo.ItemName;
            item.IconName = info.ItemInfo.ItemIcon;
            return item;
        }

        public static implicit operator Phrase(ItemConfigureOperationInfo info)
        {
            Phrase item = new Phrase();
            item.PhraseID = info.ItemInfo.ItemID;
            item.PhraseContent = info.ItemInfo.ItemName;
            return item;
        }

    }

    public class ItemPopWindowInfoArgs : EventArgs
    {
        public ItemType ItemType { get; set; }
        public OperationType OperationType { get; set; }
        public JZItemOne ItemOne { get; set; }
        public JZItemTwo ItemTwo { get; set; }
        public Phrase Phrase { get; set; }
    }

    public class ItemChangedInfoArgs : EventArgs
    {
        public OperationType OperationType { get; set; }
        public ItemType ItemType { get; set; }
        public object ItemInfo { get; set; }
    }

    public class ItemSearchedCollectionArgs : EventArgs
    {
        public ItemType ItemType { get; set; }
        public Hashtable ItemCollection { get; set; }
    }
}
