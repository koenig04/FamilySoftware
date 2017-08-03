using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model;

namespace BLL
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
        public string ItemIconPressed { get; set; }
        public bool IsIncome { get; set; }
        public string ParentID { get; set; }

        public static implicit operator ItemSelectedInfo(Model.JZItemOne model)
        {
            ItemSelectedInfo info = new ItemSelectedInfo()
            {
                ItemType = ItemType.ItemOne,
                IsIncome = model.IncomeOrCost,
                ItemIcon = model.IconName,
                ItemIconPressed = model.IconNamePressed,
                ItemID = model.JZItemOneID,
                ItemName = model.JZItemOneName
            };
            return info;
        }

        public static implicit operator ItemSelectedInfo(Model.JZItemTwo model)
        {
            ItemSelectedInfo info = new ItemSelectedInfo()
            {
                ItemType = ItemType.ItemTwo,
                ItemIcon = model.IconName,
                ItemIconPressed = model.IconNamePressed,
                ItemID = model.JZItemTwoID,
                ItemName = model.JZItemTwoName,
                ParentID = model.JZItemOneID
            };
            return info;
        }

        public static implicit operator ItemSelectedInfo(Model.Phrase model)
        {
            ItemSelectedInfo info = new ItemSelectedInfo()
            {
                ItemType = ItemType.Phrase,
                ItemID = model.PhraseID,
                ItemName = model.PhraseContent,
                ParentID = model.ItemID
            };
            return info;
        }
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
            item.IconNamePressed = info.ItemInfo.ItemIconPressed;
            return item;
        }

        public static implicit operator JZItemTwo(ItemConfigureOperationInfo info)
        {
            JZItemTwo item = new JZItemTwo();
            item.JZItemTwoID = info.ItemInfo.ItemID;
            item.JZItemTwoName = info.ItemInfo.ItemName;
            item.IconName = info.ItemInfo.ItemIcon;
            item.IconNamePressed = info.ItemInfo.ItemIconPressed;
            item.JZItemOneID = info.ItemInfo.ParentID;
            return item;
        }

        //public static explicit operator JZItemTwo(ItemConfigureOperationInfo info)
        //{
        //    JZItemTwo item = new JZItemTwo();
        //    item.JZItemTwoID = info.ItemInfo.ItemID;
        //    item.JZItemTwoName = info.ItemInfo.ItemName;
        //    item.IconName = info.ItemInfo.ItemIcon;
        //    item.IconNamePressed = info.ItemInfo.ItemIconPressed;
        //    item.JZItemOneID = info.ItemInfo.ParentID;
        //    return item;
        //}

        public static implicit operator Phrase(ItemConfigureOperationInfo info)
        {
            Phrase item = new Phrase();
            item.PhraseID = info.ItemInfo.ItemID;
            item.PhraseContent = info.ItemInfo.ItemName;
            item.ItemID = info.ItemInfo.ParentID;
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
        public bool IsSucceed { get; set; }
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
