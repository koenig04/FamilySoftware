using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace BLL.ActionDictionary
{
    class ItemConfigDictionary
    {
        //public Dictionary<BLL.ActionDictionary.ActionDictionaryProcess.ItemConfigActionKey, Action<object>> Init
        //    (ItemProcess itemProcess, Action<ItemConfigureEventArgs> action)
        //{
        //    Dictionary<BLL.ActionDictionary.ActionDictionaryProcess.ItemConfigActionKey, Action<object>> dic =
        //        new Dictionary<ActionDictionaryProcess.ItemConfigActionKey, Action<object>>();

        //    #region Add

        //    //Add ItemOne
        //    dic.Add(new BLL.ActionDictionary.ActionDictionaryProcess.ItemConfigActionKey(
        //        OperationType.Add, ItemType.ItemOne),
        //        new Action<object>(
        //            o =>
        //            {
        //                bool res = itemProcess.AddItemOne(o as Model.JZItemOne);
        //                action(new ItemConfigureEventArgs()
        //                {
        //                    OptType = OperationType.Add,
        //                    ItemType = ItemType.ItemOne,
        //                    ItemInfo = res
        //                });
        //            }));

        //    //Add ItemTwo
        //    dic.Add(new BLL.ActionDictionary.ActionDictionaryProcess.ItemConfigActionKey(
        //        OperationType.Add, ItemType.ItemTwo),
        //        new Action<object>(
        //            o =>
        //            {
        //                bool res = itemProcess.AddItemTwo(o as Model.JZItemTwo);
        //                action(new ItemConfigureEventArgs()
        //                {
        //                    OptType = OperationType.Add,
        //                    ItemType = ItemType.ItemTwo,
        //                    ItemInfo = res
        //                });
        //            }));

        //    //Add Phrase
        //    dic.Add(new BLL.ActionDictionary.ActionDictionaryProcess.ItemConfigActionKey(
        //        OperationType.Add, ItemType.Phrase),
        //       new Action<object>(
        //           o =>
        //           {
        //               bool res = itemProcess.AddPhrase(o as Model.Phrase);
        //               action(new ItemConfigureEventArgs()
        //               {
        //                   OptType = OperationType.Add,
        //                   ItemType = ItemType.Phrase,
        //                   ItemInfo = res
        //               });
        //           }));

        //    #endregion

        //    #region Search
        //    //Search all the ItemOne
        //    dic.Add(new BLL.ActionDictionary.ActionDictionaryProcess.ItemConfigActionKey(
        //        OperationType.Search, ItemType.ItemOne),
        //    new Action<object>(
        //        o =>
        //        {
        //            Hashtable hst = new Hashtable();
        //            List<Model.JZItemOne> lstOne;
        //            itemProcess.LoadItemOne((bool)o, out lstOne);
        //            hst.Add("One", lstOne);
        //            action(new ItemConfigureEventArgs()
        //            {
        //                OptType = OperationType.Search,
        //                ItemType = ItemType.ItemOne,
        //                ItemInfo = hst
        //            });
        //        }
        //    ));

        //    //Search ItemTwo and Phrases of ItemOne
        //    dic.Add(new BLL.ActionDictionary.ActionDictionaryProcess.ItemConfigActionKey(
        //        OperationType.Search, ItemType.ItemTwo),
        //    new Action<object>(
        //        o =>
        //        {
        //            Hashtable hst = new Hashtable();
        //            List<Model.JZItemTwo> lstTwo;
        //            List<Model.Phrase> lstPhrase;
        //            itemProcess.LoadItemTwoAndPhrase((o as Model.JZItemOne).JZItemOneID, out lstTwo, out lstPhrase);
        //            hst.Add("Two", lstTwo);
        //            hst.Add("Phrase", lstPhrase);
        //            action(new ItemConfigureEventArgs()
        //            {
        //                OptType = OperationType.Search,
        //                ItemType = ItemType.ItemTwo,
        //                ItemInfo = hst
        //            });
        //        }
        //    ));

        //    //Search Phrase of ItemTwo
        //    dic.Add(new BLL.ActionDictionary.ActionDictionaryProcess.ItemConfigActionKey(
        //       OperationType.Search, ItemType.Phrase),
        //    new Action<object>(
        //        o =>
        //        {
        //            Hashtable hst = new Hashtable();
        //            List<Model.Phrase> lstPhrase;
        //            itemProcess.LoadPhrase((o as Model.JZItemTwo).JZItemTwoID, out lstPhrase);
        //            hst.Add("Phrase", lstPhrase);
        //            action(new ItemConfigureEventArgs()
        //            {
        //                OptType = OperationType.Search,
        //                ItemType = ItemType.Phrase,
        //                ItemInfo = hst
        //            });
        //        }
        //   ));

        //    #endregion

        //    #region Modify

        //    //Update ItemOne
        //    dic.Add(new BLL.ActionDictionary.ActionDictionaryProcess.ItemConfigActionKey(
        //        OperationType.Modify, ItemType.ItemOne),
        //        new Action<object>(
        //            o =>
        //            {
        //                bool res = itemProcess.UpdateItemOne(o as Model.JZItemOne);
        //                action(new ItemConfigureEventArgs()
        //                {
        //                    OptType = OperationType.Modify,
        //                    ItemType = ItemType.ItemOne,
        //                    ItemInfo = res
        //                });
        //            }));

        //    //Update ItemTwo
        //    dic.Add(new BLL.ActionDictionary.ActionDictionaryProcess.ItemConfigActionKey(
        //        OperationType.Modify, ItemType.ItemTwo),
        //        new Action<object>(
        //            o =>
        //            {
        //                bool res = itemProcess.UpdateItemTwo(o as Model.JZItemTwo);
        //                action(new ItemConfigureEventArgs()
        //                {
        //                    OptType = OperationType.Modify,
        //                    ItemType = ItemType.ItemTwo,
        //                    ItemInfo = res
        //                });
        //            }));

        //    //UpdatePhrase
        //    dic.Add(new BLL.ActionDictionary.ActionDictionaryProcess.ItemConfigActionKey(
        //        OperationType.Modify, ItemType.Phrase),
        //        new Action<object>(
        //            o =>
        //            {
        //                bool res = itemProcess.UpdatePhrase(o as Model.Phrase);
        //                action(new ItemConfigureEventArgs()
        //                {
        //                    OptType = OperationType.Modify,
        //                    ItemType = ItemType.Phrase,
        //                    ItemInfo = res
        //                });
        //            }));

        //    #endregion

        //    #region Delete

        //    //Delete ItemOne
        //    dic.Add(new BLL.ActionDictionary.ActionDictionaryProcess.ItemConfigActionKey(
        //        OperationType.Delete, ItemType.ItemOne),
        //        new Action<object>(
        //            o =>
        //            {
        //                bool res = itemProcess.DelItemOne(o as string);
        //                action(new ItemConfigureEventArgs()
        //                {
        //                    OptType = OperationType.Delete,
        //                    ItemType = ItemType.ItemOne,
        //                    ItemInfo = res
        //                });
        //            }));

        //    //Delete ItemTwo
        //    dic.Add(new BLL.ActionDictionary.ActionDictionaryProcess.ItemConfigActionKey(
        //        OperationType.Delete, ItemType.ItemTwo),
        //        new Action<object>(
        //            o =>
        //            {
        //                bool res = itemProcess.DelItemTwo(o as string);
        //                action(new ItemConfigureEventArgs()
        //                {
        //                    OptType = OperationType.Delete,
        //                    ItemType = ItemType.ItemTwo,
        //                    ItemInfo = res
        //                });
        //            }));

        //    //Delete Phrase
        //    dic.Add(new BLL.ActionDictionary.ActionDictionaryProcess.ItemConfigActionKey(
        //        OperationType.Delete, ItemType.Phrase),
        //        new Action<object>(
        //            o =>
        //            {
        //                bool res = itemProcess.DelPhrase(o as string);
        //                action(new ItemConfigureEventArgs()
        //                {
        //                    OptType = OperationType.Delete,
        //                    ItemType = ItemType.Phrase,
        //                    ItemInfo = res
        //                });
        //            }));

        //    #endregion

        //    return dic;
        //}
    }
}
