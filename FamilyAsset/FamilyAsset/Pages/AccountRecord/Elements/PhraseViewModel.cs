using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using FamilyAsset.UICore;

namespace FamilyAsset.Pages.AccountRecord.Elements
{
    class PhraseViewModel : NotificationObject
    {
        public event EventHandler<PhraseSelectedEventArgs> PhraseSelectedEvent;

        private string _phraseName;

        public string PhraseName
        {
            get { return _phraseName; }
            set
            {
                _phraseName = value;
                RaisePropertyChanged("PhraseName");
            }
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged("IsSelected");
                RaisePhraseSelectedEvent(value);
            }
        }

        public PhraseViewModel(string phraseName)
        {
            this.PhraseName = phraseName;
        }

        private void RaisePhraseSelectedEvent(bool isSelected)
        {
            if (PhraseSelectedEvent != null)
            {
                PhraseSelectedEvent(this, new PhraseSelectedEventArgs(isSelected, _phraseName));
            }
        }
    }

    class PhraseSelectedEventArgs : EventArgs
    {
        public bool IsSelected { get; private set; }
        public string PhraseName { get; private set; }

        public PhraseSelectedEventArgs(bool isSelected, string phraseName)
        {
            this.IsSelected = isSelected;
            this.PhraseName = phraseName;
        }
    }
}
