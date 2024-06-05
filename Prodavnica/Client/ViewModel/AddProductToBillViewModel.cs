using Client.Class;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModel
{
    public class AddProductToBillViewModel:ViewModelBase
    {
        private string name;
        private string manufacturer;
        private string price;
        private string errorMessage;

        #region Properties
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Manufacturer
        {
            get { return manufacturer; }
            set
            {
                manufacturer = value;
                OnPropertyChanged("Manufacturer");
            }
        }

        public string Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
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

        public Command<Window> AddProductCommand { get; set; }



        public AddProductToBillViewModel()
        {
            Name = string.Empty;
            Manufacturer = string.Empty;
            Price = string.Empty;
            ErrorMessage = string.Empty;

            AddProductCommand = new Command<Window>(NewProduct);
        }

        private void NewProduct(Window window)
        {
            if (!ValidateProduct())
                return;

            window.Close();
        }

        private bool ValidateProduct()
        {
            if (Name == string.Empty)
            {
                ErrorMessage = "Name cannot be empty.";
                return false;
            }

            if (Manufacturer == string.Empty)
            {
                ErrorMessage = "Manufacturer name cannot be empty.";
                return false;
            }

            if (int.Parse(Price)<=0)
            {
                ErrorMessage = "Price must be greater then 0.";
                return false;
            }

            ErrorMessage = string.Empty;

            return true;
        }



    }
}
