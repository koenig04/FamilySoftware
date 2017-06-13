using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ItemConfigureProcess;

namespace BLL.AssetInputAndOperationProcess.ItemLoading
{
    class ItemTwoAndPhrasesLoadingStrategy : ItemLoadingStrategySuper
    {
        public ItemTwoAndPhrasesLoadingStrategy(ItemProcess ItemProcess)
            : base(ItemProcess)
        {

        }

        public override void LoadItems(ItemSelectedInfo info)
        {
            List<Model.JZItemTwo> lstTwo;
            List<Model.Phrase> lstPhrase;
            Hashtable hst = new Hashtable();

            _itemProcess.LoadItemTwoAndPhrase(info.ItemID, out lstTwo, out lstPhrase);

            hst.Add("Two", lstTwo);
            hst.Add("Phrase", lstPhrase);

            RaiseLoadedItemsEvent(new ItemSearchedCollectionArgs() { ItemType = info.ItemType, ItemCollection = hst });
        }
    }
}
