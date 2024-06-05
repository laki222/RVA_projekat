using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class Bill : ICloneable
    {
        
      
        public DateTime CreatedDate { get; set; }
        public List<BillProducts> BillProducts { get; set; }
        [Key]
        public int BillID { get; set; }

        public Product Product { get; set; }

        public float Price { get; set; }


        public ICloneable Clone()
        {
            Bill r = new Bill();
            r.BillID = BillID;
            r.CreatedDate = CreatedDate;
            r.Product = Product;
            r.BillProducts = BillProducts;
            r.Price = Price;


            return r;
        }
    }
}
