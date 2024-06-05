using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
   public class RegisteredCustomer : IUser
    {


        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public float Discount { get; set; }

        public string IsDeleted { get; set; }



    }
}
