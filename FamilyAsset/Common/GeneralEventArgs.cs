using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class StringEventArgs : EventArgs
    {
        public string Content { get; private set; }
        public StringEventArgs(string Content)
        {
            this.Content = Content;
        }
    }

    public class IntEventArgs : EventArgs
    {
        public int Content { get; private set; }
        public IntEventArgs(int Content)
        {
            this.Content = Content;
        }
    }

    public class BoolenEventArgs : EventArgs
    {
        public bool Content { get; private set; }
        public string Msg { get; private set; }
        public BoolenEventArgs(bool Content, string Msg = null)
        {
            this.Content = Content;
            this.Msg = Msg;
        }
    }

    public class HashtableEventArgs : EventArgs
    {
        public Hashtable Content { get; private set; }

        public HashtableEventArgs(Hashtable Content)
        {
            this.Content = Content;
        }
    }
}
