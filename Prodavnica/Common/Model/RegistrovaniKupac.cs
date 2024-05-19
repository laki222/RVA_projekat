using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
   public class RegistrovaniKupac : IUser
    {
        private float popustPriKupovini;
        private string ime;
        private string prezime;
        private string username;
        private string password;



        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
