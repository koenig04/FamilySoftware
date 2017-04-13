using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace FamilyAsset.Context
{
    class ItemConfigCallBackContext : IContext
    {
        public ItemType ItemType { get; private set; }

        public ItemConfigCallBackContext(ItemType ItemType)
        {
            this.ItemType = ItemType;
        }
    }
}
