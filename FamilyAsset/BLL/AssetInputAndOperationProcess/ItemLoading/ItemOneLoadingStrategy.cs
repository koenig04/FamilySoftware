using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ItemConfigureProcess;

namespace BLL.AssetInputAndOperationProcess.ItemLoading
{
    class ItemOneLoadingStrategy : ItemLoadingStrategySuper
    {
        public ItemOneLoadingStrategy(ItemProcess ItemProcess)
            : base(ItemProcess)
        {

        }

        public override void LoadItems(ItemSelectedInfo info)
        {
            List<Model.JZItemOne> lstItemOne;
            Hashtable hst=new Hashtable();
            _itemProcess.LoadItemOne(info.IsIncome, out lstItemOne);
            hst.Add("One",lstItemOne);
            RaiseLoadedItemsEvent(new Common.HashtableEventArgs(hst));
        }
    }
}
