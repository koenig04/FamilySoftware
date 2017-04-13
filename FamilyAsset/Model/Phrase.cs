using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Phrase
    {
        public Phrase()
        { }
        #region Model
        private int _id;
        private string _phraseid;
        private string _phrasecontent;
        private string _itemid;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PhraseID
        {
            set { _phraseid = value; }
            get { return _phraseid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PhraseContent
        {
            set { _phrasecontent = value; }
            get { return _phrasecontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ItemID
        {
            set { _itemid = value; }
            get { return _itemid; }
        }
        #endregion Model

    }
}
