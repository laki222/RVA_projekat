using Client.Class;
using Client.View;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Client.ViewModel
{
    public class HomeViewModel:ViewModelBase
    {
        public ICollectionView BillList { get; set; }

        private List<Product> _products;

        public string PriceTextBox { get; set; }
        public string ProductTextBox { get; set; }

        // Commands
        public Command NewBillCommand { get; set; }

        public Command AddProductCommand { get; set; }

        public Command EditBillCommand { get; set; }
        public Command<Bill> DuplicateCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Command RefreshCommand { get; set; }
        public Command SearchCommand { get; set; }

        public Command LogoutCommand {  get; set; }


        // Undo/redo
        public Command UndoCommand { get; set; }
        public Command RedoCommand { get; set; }
        private ActionHistory history;

        private Bill selectedBill;
        private List<Bill> bills; // for display
        private List<Bill> localBillDB;// for tracking changes

        private string loggedUsername;
        private bool isAdmin;
       
        public HomeViewModel()
        {
            history = new ActionHistory();
            AddProductCommand = new Command(AddProduct);
            
            NewBillCommand = new Command(NewBill);
            EditBillCommand = new Command(EditBill);
            DuplicateCommand = new Command<Bill>(DuplicateBill, CanDuplicate);
            DeleteCommand = new Command(DeleteBill);
            RefreshCommand = new Command(RefreshList);
            LogoutCommand = new Command(Logout);



            SearchCommand = new Command(FilterBills);
            UndoCommand = new Command(Undo, history.CanUndo);
            RedoCommand = new Command(Redo, history.CanRedo);


           



            bills = Session.Current.BillProxy.GetAllBills();
                localBillDB = new List<Bill>(bills);

                CollectionViewSource itemSourceList = new CollectionViewSource() { Source = bills };
                BillList = itemSourceList.View;
            
        }
        public void CheckIfUserIsAdmin()
        {
            string loggedUsername = Session.Current.LoggedInUser;
            IsAdmin = Session.Current.BillProxy.CheckIfAdmin(loggedUsername);
        }

        public bool IsAdmin
        {
            get { return isAdmin; }
            set
            {
                isAdmin = value;
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        public Bill SelectedBill
        {
            get { return selectedBill; }
            set
            {
                selectedBill = value;
                DuplicateCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("SelectedBill");
                UpdateProducts();
            }
        }
        public List<Product> Products
        {
            get { return _products; }
            private set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }
        private void UpdateProducts()
        {
            Products = Session.Current.BillProxy.GetAllProductById(selectedBill.BillID);

        }

        private void AddProduct()
        {
            var win = new View.AddProductToBillView();
            AddProductToBillViewModel vm = (AddProductToBillViewModel)win.DataContext;
            
            win.ShowDialog();
            Product p = Session.Current.BillProxy.CreateProduct(selectedBill.BillID.ToString(),vm.Name, vm.Manufacturer, vm.Price);
            if (p!=null)
                ClientLogger.Log($"Product {vm.Name} successfully created.", Common.LogLevel.Info);
            else
                ClientLogger.Log($"Product {vm.Name} could not be created.", Common.LogLevel.Error);

            Session.Current.BillProxy.AddProductToBill(selectedBill.BillID, vm.Name, vm.Manufacturer);


            
            RefreshList();

        }

        private void Logout()
        {
            Session.Current.Abandon();
            Application.Current.Dispatcher.Invoke(() =>
            {
                var mainWindow = Application.Current.MainWindow;
                var loginWindow = new LoginWindow();
                loginWindow.Show();
                mainWindow.Close();
            });

            Session.Current.LoggedInUser = null;
        }


        private void NewBill()
        {
           
            Bill bill = Session.Current.BillProxy.CreateBill(Session.Current.LoggedInUser);
            if (bill!=null)
                ClientLogger.Log($"Bill {bill.BillID} successfully created.", Common.LogLevel.Info);
            else
                ClientLogger.Log($"Bill {bill.BillID} could not be created.", Common.LogLevel.Error);



            Action redo = () =>
            {
                

                NewBillCommand.RaiseCanExecuteChanged();
               
                BillList.Refresh();
            };
            Action undo = () =>
            {


                DuplicateCommand.RaiseCanExecuteChanged();
               
                BillList.Refresh();
            };

            history.AddAndExecute(new RevertableCommand(redo, undo));
            UndoCommand.RaiseCanExecuteChanged();
            RedoCommand.RaiseCanExecuteChanged();

            
            RefreshList();
        }

        private void EditBill()
        {
            var win = new View.NewBillWindow();
            NewBillViewModel vm = (NewBillViewModel)win.DataContext;
            vm.Author = selectedBill.Creator;

            // Otvara se prozor
            win.ShowDialog();

            if (Session.Current.BillProxy.EditBill(vm.Author,selectedBill.BillID))
                ClientLogger.Log($"Bill {vm.Author} successfully edited.", Common.LogLevel.Info);
            else
                ClientLogger.Log($"Bill {vm.Author} could not be edited.", Common.LogLevel.Error);

            RefreshList();
        }

        private void DuplicateBill(Bill b)
        {
            ClientLogger.Log($"Bill {b.BillID} duplicated.", Common.LogLevel.Info);
            Session.Current.BillProxy.DoubleBill(b);
            RefreshList();
        }

        private void DeleteBill()
        {
            if (Session.Current.BillProxy.DeleteBill(selectedBill.BillID))
                ClientLogger.Log($"Bill {selectedBill.BillID} deleted successfully.", Common.LogLevel.Info);
            else
                ClientLogger.Log($"Bill {selectedBill.BillID} could not be deleted.", Common.LogLevel.Error);

            RefreshList();
        }

        private bool CanDuplicate()
        {
            return selectedBill != null;
        }

        private void RefreshList()
        {
            bills.Clear();

           
            foreach (Bill b in Session.Current.BillProxy.GetAllBills())
            {
                if (bills.ToList().Find(p => p.BillID == b.BillID) == null)
                    bills.Add(b);
            }

            BillList.Refresh();
        }

       

       
        private void FilterBills()
        {
            BillList.Filter = new Predicate<object>(b =>
                ((Bill)b).BillID.ToString().Contains(PriceTextBox == null ? "" : PriceTextBox) &&
                ((Bill)b).Creator.Contains(ProductTextBox == null ? "" : ProductTextBox)
            );
        }

        private void Undo()
        {
            history.Undo();
            UndoCommand.RaiseCanExecuteChanged();
            RedoCommand.RaiseCanExecuteChanged();
        }

        private void Redo()
        {
            history.Redo();
            UndoCommand.RaiseCanExecuteChanged();
            RedoCommand.RaiseCanExecuteChanged();
        }
    }
}
