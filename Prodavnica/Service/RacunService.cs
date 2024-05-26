using Common;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class RacunService : IRacunService
    {

        private static List<string> loggedInUsers = new List<string>();

        public RacunService()
        {
            // ovde ce ici logger kad se bude implementirao
        }
        public bool CreateRacun()
        {
            throw new NotImplementedException();
        }

        public bool DeleteRacun(string name)
        {
            using (var db = new RacunDbContext())
            {
                Racun r = db.Racuni.Find(name);

                if(r == null)
                {
                    //logger ide
                    Console.WriteLine("ne postoji");
                    return false;
                }

                db.Racuni.Remove(r);
                db.SaveChanges();

                //logger

                return true;
            }
         }

        public void DoubleRacun(Racun racun)
        {
            List<Racun> racuni= GetAllRacun();

            Racun toClone = racuni.Find(r => r.RacunID == racun.RacunID);
            Racun clone = (Racun)toClone.Clone();

            using(var db = new RacunDbContext())
            {
                db.Proizvodi.Attach(clone.Proizvod);
               // clone.RacunID += " (Copy)";
                db.Racuni.Add(clone);
                db.SaveChanges();


                //ovde logovanje
            }

        }

        public bool EditRacun()
        {
            throw new NotImplementedException();
        }

        public List<Racun> GetAllRacun()
        {
            using(var db = new RacunDbContext())
            {
                //logovanje ovde
                return db.Racuni.Include("Proizvod").ToList();
            }
        }

        public LogInInfo LogIn(string username, string password)
        {
            using(var db = new RacunDbContext())
            {
                IUser user = db.RegistrovaniKupci.FirstOrDefault(rk => rk.Username.Equals(username));
                
                
                // if not found in users, look in admins
                if (user == null)
                    user = db.Admini.FirstOrDefault(a => a.Username.Equals(username));

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

        public bool SearchRacun()
        {
            throw new NotImplementedException();
        }
    }
}
