using Client.Class;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class AccountViewModel: ViewModelBase
    {
        public string FirstNameTextBox { get; set; }
        public string LastNameTextBox { get; set; }

        public Command SaveCommand { get; set; }



        public AccountViewModel()
        {
            RegisteredCustomer ui = Session.Current.BillProxy.GetUserInfo(Session.Current.LoggedInUser);
            FirstNameTextBox = ui.FirstName;
            LastNameTextBox = ui.LastName;

            SaveCommand = new Command(SaveAccountChanges);
        }

        private void SaveAccountChanges()
        {
            Session.Current.BillProxy.EditUserInfo(Session.Current.LoggedInUser, FirstNameTextBox, LastNameTextBox);
            ClientLogger.Log($"Account {Session.Current.LoggedInUser} successfully edited.", Common.LogLevel.Info);
        }


    }
}
