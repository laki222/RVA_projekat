using Common;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;



namespace Service
{
    class BillService : IBillService
    {

        private static List<string> loggedInUsers = new List<string>();
        private int _currentBillId;

        


        public BillService()
        {
            // ovde ce ici logger kad se bude implementirao
        }

        public Bill CreateBill()
        {
            using (var db = new BillDbContext())
            {
                // Kreiraj novi račun i sačuvaj ga u bazi podataka
                var newBill = new Bill();
                db.Bills.Add(newBill);
                db.SaveChanges();
               
                // Vraćamo ID novog računa
                return newBill;
            }
        }
        public void AddProductToBill(string name, string manufacturer)
        {
            using (var db = new BillDbContext())
            {
                var bill = db.Bills.Find(_currentBillId);
                if (bill == null) return;

                var product = db.Products.FirstOrDefault(p => p.Name == name && p.Manufacturer == manufacturer);
                if (product == null) return;

                db.BillProducts.Add(new BillProducts
                {
                    BillId = _currentBillId,
                    ProductName = product.Name,
                    Price = product.Price
                });
                db.SaveChanges();
            }
        }


        public bool CreateUser(string username, string password, string firstName, string lastName)
        {
            using (var db = new BillDbContext())
            {
                if (db.RegisteredCustomers.Find(username) != null)
                    return false;

                RegisteredCustomer m = new RegisteredCustomer() { Username = username, Password = password, FirstName = firstName, LastName = lastName };
                db.RegisteredCustomers.Add(m);
                db.SaveChanges();
            }

            //loger
            return true;

        }

        public bool DeleteBill(string name)
        {
            using (var db = new BillDbContext())
            {
                var bill = db.Bills.Include(b => b.BillProducts).FirstOrDefault(b => b.BillID == _currentBillId);

                if (bill != null)
                {
                    db.BillProducts.RemoveRange(bill.BillProducts);
                    db.Bills.Remove(bill);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
         }

        public void DoubleBill(Bill racun)
        {
            List<Bill> racuni= GetAllBills();

            Bill toClone = racuni.Find(r => r.BillID == racun.BillID);
            Bill clone = (Bill)toClone.Clone();

            using(var db = new BillDbContext())
            {
                db.Products.Attach(clone.Product);
               // clone.RacunID += " (Copy)";
                db.Bills.Add(clone);
                db.SaveChanges();


                //ovde logovanje
            }

        }

        public bool EditBill()
        {
            using (var db = new BillDbContext())
            {
                Bill bill=SearchBill();
                if (bill != null)
                {
                    bill.CreatedDate = DateTime.Now;
                    // Update other properties as needed

                    db.SaveChanges();
                    return true;
                }
                return false;

            }
        
        }
        public void EditUserInfo(string username, string firstName, string lastName)
        {
            IUser user = null;

            using (var db = new BillDbContext())
            {
                user = db.RegisteredCustomers.FirstOrDefault(m => m.Username == username);

                if (user == null)
                    user = db.Admins.FirstOrDefault(m => m.Username == username);

                if (user == null)
                    return;

                user.FirstName = firstName;
                user.LastName = lastName;

                //logger.Log("Account info of " + username + " edited.", LogLevel.Debug);

                db.SaveChanges();
            }
        }

        public List<Bill> GetAllBills()
        {
            using(var db = new BillDbContext())
            {
                //logovanje ovde
                return db.Bills.Include("Proizvod").ToList();
            }
        }

        public RegisteredCustomer GetUserInfo(string username)
        {
            IUser user = null;

            using (var db = new BillDbContext())
            {
                user = db.RegisteredCustomers.FirstOrDefault(m => m.Username == username);

                if (user == null)
                    user = db.Admins.FirstOrDefault(m => m.Username == username);
            }

            if (user == null)
                return null;

            //logger.Log("User info of " + username + " querried.", LogLevel.Debug);

            return new RegisteredCustomer() { Username = user.Username, FirstName = user.FirstName, LastName = user.LastName };
        }

        public LogInInfo LogIn(string username, string password)
        {
            using(var db = new BillDbContext())
            {
                IUser user = db.RegisteredCustomers.FirstOrDefault(rk => rk.Username.Equals(username));
                
                
                // if not found in users, look in admins
                if (user == null)
                    user = db.Admins.FirstOrDefault(a => a.Username.Equals(username));

                // if still null, wrong username
                if (user == null)
                    return LogInInfo.WrongUserOrPass;

                // already logged in
                if (loggedInUsers.Contains(username))
                    return LogInInfo.AlreadyConnected;

                if (user.Password == password)
                {
                    //ovde treba logovati
                    loggedInUsers.Add(username);

                    return LogInInfo.Sucess;
                }

                // wrong password
               // ovde ide logovanje
                return LogInInfo.WrongUserOrPass;
            }
        }

        public void LogOut(string username)
        {
            throw new NotImplementedException();
        }

        public Bill SearchBill()
        {
            using (var db = new BillDbContext())
            {
                var bill = db.Bills.Include(b => b.BillProducts).FirstOrDefault(b => b.BillID == _currentBillId);

                if (bill != null)
                {
                    return bill;
                }
                return null;
            }
        }    
        }
}
