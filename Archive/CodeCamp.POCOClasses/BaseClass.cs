using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace CodeCamp.CoreClasses
{
    public abstract class BaseClass:INotifyPropertyChanged
    {
 
        protected void OnPropertyChanged(string aProperty)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(aProperty));
            }
        }

        public event PropertyChangedEventHandler  PropertyChanged;

    }
}
