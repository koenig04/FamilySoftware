using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ItemConfigureEventArgs : EventArgs
    {
        public Common.OperationType OptType { get; set; }
        public Common.ItemType ItemType { get; set; }
        public object ItemInfo { get; set; }
    }    
}
