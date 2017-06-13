using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ItemConfigureProcess;

namespace BLL.AssetInputAndOperationProcess.ItemLoading
{
    class PhrasesLoadingStrategy : ItemLoadingStrategySuper
    {
        public PhrasesLoadingStrategy(ItemProcess ItemProcess)
            : base(ItemProcess)
        {

        }

        public override void LoadItems(ItemSelectedInfo info)
        {
            List<Model.Phrase> lstPhrase;
            Hashtable hst = new Hashtable();

            _itemProcess.LoadPhrase(info.ItemID, out lstPhrase);

            hst.Add("Phrase", lstPhrase);

            RaiseLoadedItemsEvent(new ItemSearchedCollectionArgs() { ItemType = info.ItemType, ItemCollection = hst });
        }
    }
}
