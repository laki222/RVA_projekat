using Client.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


		public string ErrorText
		{
			get { return errorText; }
			set
			{
				errorText = value;
				OnPropertyChanged("ErrorText");
			}
		}

		private void Login(object parameter)
		{
			PasswordBox pass = parameter as PasswordBox;


		}


	}
}
