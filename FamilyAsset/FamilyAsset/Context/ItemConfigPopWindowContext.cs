using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using BLL.ItemConfigureProcess;
using Common;

namespace FamilyAsset.Context
{    
    class ItemConfigPopWindowContext : IContext
    {
        public IItemConfigureProcess Bussiness { get; set; }
        public OperationType OpType { get; set; }
        public ItemType ItemType { get; set; }

        private Model.JZItemOne _itemOne = null;

        public Model.JZItemOne ItemOne
        {
            get { return _itemOne; }
            set { _itemOne = value; }
        }

        private Model.JZItemTwo _itemTwo = null;

        public Model.JZItemTwo ItemTwo
        {
            get { return _itemTwo; }
            set { _itemTwo = value; }
        }

        private Model.Phrase _phrase = null;

        public Model.Phrase Phrase
        {
            get { return _phrase; }
            set { _phrase = value; }
        }
    }
}
