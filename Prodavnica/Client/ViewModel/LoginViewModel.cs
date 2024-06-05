using Client.Class;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Client.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {

        public string Username { get; set; }
        public Command<object> LoginCommand { get; set; }

        private string errorText;



        public LoginViewModel()
        {
            LoginCommand = new Command<object>(Login);
        }

        #region Properties
        public string ErrorText
        {
            get { return errorText; }
            set
            {
                errorText = value;
                OnPropertyChanged("ErrorText");
            }
        }
        #endregion

        private void Login(object parameter)
        {
            PasswordBox pass = parameter as PasswordBox;

            LogInInfo loginInfo;
            
            loginInfo = Session.Current.BillProxy.LogIn(Username, pass.Password);

            if (loginInfo == LogInInfo.Sucess)
            {
                Session.Current.LoggedInUser = Username;

                MainWindow mw = new MainWindow();
                mw.Show();

                Application.Current.MainWindow.Close();
                Application.Current.MainWindow = mw;

                //ClientLogger.Log("Successfully logged in.", Common.LogLevel.Info);
            }
            else
            {
                if (loginInfo == LogInInfo.WrongUserOrPass)
                    ErrorText = "Username or password is incorrect.";
                else
                    ErrorText = "Account already connected.";

                //ClientLogger.Log("Unsuccessful login attempt.", Common.LogLevel.Error);
            }
        }



    }
}
