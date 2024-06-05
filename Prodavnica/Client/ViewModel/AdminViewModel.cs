using Client.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Client.ViewModel
{
    class AdminViewModel : ViewModelBase
    {
        public string UsernameTextBox { get; set; }
        public string FirstNameTextBox { get; set; }
        public string LastNameTextBox { get; set; }

        public Command<object> CreateUserCommand { get; set; }

        private string errorText;



        public AdminViewModel()
        {
            UsernameTextBox = string.Empty;
            FirstNameTextBox = string.Empty;
            LastNameTextBox = string.Empty;
            CreateUserCommand = new Command<object>(CreateUser);
        }

        public string ErrorText
        {
            get { return errorText; }
            set
            {
                errorText = value;
                OnPropertyChanged("ErrorText");
            }
        }

        private void CreateUser(object param)
        {
            PasswordBox pw = param as PasswordBox;

            if (!ValidateUserData(pw.Password == null ? string.Empty : pw.Password))
                return;

            if (!Session.Current.BillProxy.CreateUser(UsernameTextBox, pw.Password, FirstNameTextBox, LastNameTextBox))
            {
                ErrorText = "User already exists.";
                return;
            }

            ClientLogger.Log("Admin has created a new user with username " + UsernameTextBox, Common.LogLevel.Debug);

            UsernameTextBox = string.Empty;
            FirstNameTextBox = string.Empty;
            LastNameTextBox = string.Empty;
            pw.Password = string.Empty;
            ErrorText = string.Empty;
            OnPropertyChanged("UsernameTextBox");
            OnPropertyChanged("FirstNameTextBox");
            OnPropertyChanged("LastNameTextBox");
        }

        private bool ValidateUserData(string password)
        {
            if (UsernameTextBox == string.Empty)
            {
                ErrorText = "Username can't be empty.";
                return false;
            }

            if (password == string.Empty)
            {
                ErrorText = "Password can't be empty.";
                return false;
            }

            if (!Regex.IsMatch(FirstNameTextBox, @"^[a-zA-Z]+$"))
            {
                ErrorText = "First name must contain only letters.";
                return false;
            }

            if (!Regex.IsMatch(LastNameTextBox, @"^[a-zA-Z]+$"))
            {
                ErrorText = "Last name must contain only letters.";
                return false;
            }

            return true;
        }
    }
}
