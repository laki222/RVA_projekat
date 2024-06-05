using Client.Class;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModel
{
    public class AddProductToBillViewModel:ViewModelBase
    {
        private List<Product> selectedProducts;
       
        public List<Product> SelectedProducts
        {
            get { return selectedProducts; }
            set
            {
                selectedProducts = value;
                OnPropertyChanged(nameof(SelectedProducts));
            }
        }
        public Command<Window> NewProdcutToBillCommand { get; set; }
        public AddProductToBillViewModel()
        {
            SelectedProducts = new List<Product>();
            NewProdcutToBillCommand = new Command<Window>(NewProductToBill);
        }

        private void NewProductToBill(Window window)
        {
            foreach (Product product in SelectedProducts) {
                Session.Current.BillProxy.AddProductToBill(product.Name, product.Manufacturer);
            }
           
            window.Close();
        }



    }
}
