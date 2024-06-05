using Client.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModel
{
    public class NewBillViewModel:ViewModelBase
    {
      
        private string author;
        private string errorMessage;

        #region Properties
    
        public string Author
        {
            get { return author; }
            set
            {
                author = value;
                OnPropertyChanged("Author");
            }
        }

       
        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }
        #endregion

        public Command<Window> NewBillCommand { get; set; }



        public NewBillViewModel()
        {          
            Author = string.Empty;
            ErrorMessage = string.Empty;

            NewBillCommand = new Command<Window>(NewBill);
        }

        private void NewBill(Window window)
        {
            if (!ValidateBill())
                return;

            window.Close();
        }

        private bool ValidateBill()
        {
           

            if (Author == string.Empty)
            {
                ErrorMessage = "Author name cannot be empty.";
                return false;
            }


            ErrorMessage = string.Empty;

            return true;
        }
    }
}
