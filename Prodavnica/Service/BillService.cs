using Client.Class;
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
        private Logger logger = new Logger();




        public BillService()
        {
            logger.AddTarget(new LoggerConsoleTarget());
            logger.AddTarget(new LoggerFileTarget("LogData.txt"));
        }

        public Bill CreateBill(string creator)
        {
            using (var db = new BillDbContext())
            {
                // Kreiraj novi račun i sačuvaj ga u bazi podataka
                var newBill = new Bill() { CreatedDate=DateTime.Now,Creator=creator };
                db.Bills.Add(newBill);
                db.SaveChanges();

                logger.Log("Bill " + newBill.BillID + "  successfully created.", LogLevel.Info);
                return newBill;
            }
        }
        public void AddProductToBill(int id,string name, string manufacturer)
        {
            using (var db = new BillDbContext())
            {
                var bill = db.Bills.Find(id);
                if (bill == null) return;

                var product = db.Products.FirstOrDefault(p => p.Name == name && p.Manufacturer == manufacturer);
                if (product == null) return;

                db.BillProducts.Add(new BillProducts
                {
                    BillId = id,
                    ProductName = product.Name,
                    Price = product.Price
                });
                logger.Log("Product " + name + "  successfully added.", LogLevel.Info);
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

            logger.Log("User " + username + "  successfully created.", LogLevel.Info);
            return true;

        }

        public bool DeleteBill(int billid)
        {
            using (var db = new BillDbContext())
            {
                var bill = db.Bills.Include(b => b.BillProducts).FirstOrDefault(b => b.BillID == billid);

                if (bill != null)
                {
                    db.BillProducts.RemoveRange(bill.BillProducts);
                    db.Bills.Remove(bill);
                    db.SaveChanges();
                    logger.Log("Bill " + billid + "  successfully deleted.", LogLevel.Info);
                    return true;
                }
                logger.Log("Bill " + billid + "  unsuccessfully deleted.", LogLevel.Info);
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
                //db.Products.Attach(clone.Product);
               // clone.RacunID += " (Copy)";
                db.Bills.Add(clone);
                db.SaveChanges();


                logger.Log("Bill " + racun.BillID + "  successfully cloned.", LogLevel.Info);
            }

        }

        public bool EditBill(string creator,int id)
        {
            using (var db = new BillDbContext())
            {
                Bill bill=db.Bills.Find(id);
                if (bill != null)
                {
                    db.Bills.Remove(bill);
                    db.SaveChanges() ;
                    Bill bill1 = new Bill(); 
                    bill1.BillID= id;
                    bill1.CreatedDate = DateTime.Now;
                    bill1.Creator=creator;
                    bill1.BillProducts=bill.BillProducts;
                    bill1.Price=bill.Price;
                    bill1.Product=bill.Product;
                    db.Bills.Add(bill1);
                    db.SaveChanges();
                    logger.Log("Bill " + id + "  successfully edited.", LogLevel.Info);
                    return true;
                }
                logger.Log("Bill " + id + " is not found.", LogLevel.Info);
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

                logger.Log("Account info of " + username + " edited.", LogLevel.Debug);

                db.SaveChanges();
            }
        }

        public List<Bill> GetAllBills()
        {
            using(var db = new BillDbContext())
            {
                //logovanje ovde

                var bill = db.Bills.ToList();

                foreach (var item in bill)
                {
                    var products=GetAllProductById(item.BillID);
                    float temp=0;
                    foreach (var p in products)
                    {
                        temp+= p.Price;
                    }

                    item.Price = temp;

                }
                logger.Log("Bill data querried.", LogLevel.Debug);

                return db.Bills.ToList();
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

            logger.Log("User info of " + username + " querried.", LogLevel.Debug);

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
                    logger.Log("User " + username + " logged in.", LogLevel.Info);
                    loggedInUsers.Add(username);

                    return LogInInfo.Sucess;
                }

                // wrong password
                logger.Log("Wrong password.", LogLevel.Info);
                return LogInInfo.WrongUserOrPass;
            }
        }

        public void LogOut(string username)
        {
            logger.Log("User " + username + " has disconnected.", LogLevel.Info);
        }

        public bool CheckIfAdmin(string username)
        {
            using (var db = new BillDbContext())
            {
                var admins = db.Admins.Include(b => b.Username).FirstOrDefault(b => b.Username == username);

                if (admins != null)
                {
                    logger.Log("User " + username + " is admin.", LogLevel.Info);
                    return true;
                }

                logger.Log("User " + username + " is not admin.", LogLevel.Info);

                return false;
            }


        }


        public Bill SearchBill(int id)
        {
            using (var db = new BillDbContext())
            {
                var bill = db.Bills.Include(b => b.BillProducts).FirstOrDefault(b => b.BillID == id);

                if (bill != null)
                {
                    logger.Log("Bill " + id + " is found.", LogLevel.Info);
                    return bill;
                }

                logger.Log("Bill " + id + " is not found.", LogLevel.Info);
                return null;
            }
        }

        public Product CreateProduct(string billid, string name, string manufacturer, string price)
        {
            using (var db = new BillDbContext())
            {
                
                Product p = new Product() { Name = name,BillId=billid, Manufacturer = manufacturer, Price = float.Parse(price), IsDeleted = false };
                db.Products.Add(p);
                db.SaveChanges();
                logger.Log("Product " + name + " created.", LogLevel.Info);
                return p;
            }
        }

        public List<Product>GetAllProductById(int id)
        {
            using (var db = new BillDbContext())
            {
                var products = db.Products.Where(p => p.BillId == id.ToString()).ToList();
                

                if (products != null)
                {
                    logger.Log("Product with billId:" + id + " exist.", LogLevel.Info);
                    return products;
                }
                logger.Log("Product with billId:" + id + "not exist.", LogLevel.Info);
                return null;
            }
        }

    }
}
