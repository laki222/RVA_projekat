using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class Product
    {
        public string BillId {  get; set; }

        [Key]
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public bool IsDeleted { get; set; }
        public float Price { get; set; }

    }
}
