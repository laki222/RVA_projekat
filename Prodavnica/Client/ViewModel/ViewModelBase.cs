using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected static Logger ClientLogger;



        public ViewModelBase()
        {
            if (ClientLogger == null)
            {
                ClientLogger = new Logger();
                ClientLogger.AddTarget(new LoggerFileTarget("ClientActionsLog.txt"));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
