using Client.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            LoginWindow mainWindow = new LoginWindow(); // Instancirajte glavni prozor
            mainWindow.Show(); // Prikazuje glavni prozor
        }

        private void OnApplicationExit(object sender, ExitEventArgs e)
        {
            Session.Current.Abandon();
        }
    }
}
